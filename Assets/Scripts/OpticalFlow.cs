using UnityEngine;
using UnityEngine.UI;

namespace OpticalFlow {

    public class OpticalFlow : MonoBehaviour {
        public static int[] R = new int[15];
        public static int[] G = new int[15];
        bool left = false;
        bool right = false;
        float cooldown = 1f;
        float lastMove;

        protected enum Pass {
            Flow = 0,
            DownSample = 1,
            BlurH = 2,
            BlurV = 3,
            Visualize = 4
        };

        public RenderTexture Flow { get { return resultBuffer; } }

        [SerializeField] protected Material flowMaterial;
        [SerializeField, Range(0, 6)] int blurIterations = 0, blurDownSample = 0;
        [SerializeField] protected bool debug;
        public GameObject go;
        protected RenderTexture prevFrame, flowBuffer, resultBuffer;

        #region MonoBehaviour functions

        protected void Start() {
            lastMove = -cooldown;
        }

        protected void OnRenderImage(RenderTexture source, RenderTexture destination) {
            Graphics.Blit(resultBuffer, destination, flowMaterial, (int)Pass.Visualize);
        }

        protected void OnDestroy() {
            if (prevFrame != null) {
                prevFrame.Release();
                prevFrame = null;

                flowBuffer.Release();
                flowBuffer = null;

                resultBuffer.Release();
                resultBuffer = null;
            }
        }

        protected void OnGUI() {

            if (!debug || prevFrame == null || flowBuffer == null)
                return;

            const int offset = 10;
            const int width = 176, height = 144;

            GUI.DrawTexture(new Rect(offset, offset + height, width, height), flowBuffer);
            GUI.DrawTexture(new Rect(offset, offset, width, height), prevFrame);
            

            //GUIStyle gs = new GUIStyle();
            //gs.fontSize = 200;
            //GUI.backgroundColor = Color.white;
            //GUI.color = Color.red;
            //GUI.contentColor = Color.red;
            Flowing();
            if (left) {
                GUI.Label(new Rect(250, 20, 600, 600), "LEFT");
                go.transform.position -= (new Vector3(-0.01f, 0, 0));
            }
            if (right) {
                GUI.Label(new Rect(250, 60, 600, 600), "RIGHT");
                go.transform.position -= (new Vector3(0.01f, 0, 0));
            }

        }

        #endregion

        public static void resetMotion() {
            // quick setup for movement
            for (int i = 0; i < R.Length; i++) {
                R[i] = 0;
                G[i] = 0;
            }
        }

        protected void Setup(int width, int height) {
            resetMotion();
            prevFrame = new RenderTexture(width, height, 0);
            prevFrame.format = RenderTextureFormat.ARGBFloat;
            prevFrame.wrapMode = TextureWrapMode.Repeat;
            prevFrame.Create();

            flowBuffer = new RenderTexture(width, height, 0);
            flowBuffer.format = RenderTextureFormat.ARGBFloat;
            flowBuffer.wrapMode = TextureWrapMode.Repeat;
            flowBuffer.Create();

            resultBuffer = new RenderTexture(width >> blurDownSample, height >> blurDownSample, 0);
            resultBuffer.format = RenderTextureFormat.ARGBFloat;
            resultBuffer.wrapMode = TextureWrapMode.Repeat;
            resultBuffer.Create();
        }

        /// <summary>
        /// Returns 0 if not consistent, 1 if consistent left, 2 if consistent right
        /// </summary>
        /// <returns></returns>
        protected void Flowing() {
            int red = 0;
            for (int i = 0; i < R.Length; i++) {
                if (R[i]!=0)
                    if (R[i] > G[i])
                        red++;
            }
            
            int green = 0;
            for (int i = 0; i < G.Length; i++) {
                if (G[i] != 0)
                    if (G[i] > R[i])
                        green++;
            }

            bool usingWave = GameObject.Find("UseMove").GetComponent<Toggle>().isOn;

            if (Time.realtimeSinceStartup - lastMove > cooldown) {
                if (green > 13) {
                    resetMotion();
                    left = true;
                    print("LEFT: ");
                    lastMove = Time.realtimeSinceStartup;
                } else {
                    left = false;
                }
                if (red > 13) {
                    resetMotion();
                    right = true;
                    print("RIGHT: ");
                    lastMove = Time.realtimeSinceStartup;
                } else {
                    right = false;
                }
            }
        }

        public void Calculate(Texture current) {
            if (prevFrame == null) {
                Setup(current.width, current.height);
                Graphics.Blit(current, prevFrame);
            }

            flowMaterial.SetTexture("_PrevTex", prevFrame);
            flowMaterial.SetFloat("_Ratio", 1f * Screen.height / Screen.width);

            Graphics.Blit(current, flowBuffer, flowMaterial, (int)Pass.Flow);
            Graphics.Blit(current, prevFrame);


            // Graphics.Blit(flowBuffer, destination, flowMaterial, (int)Pass.Visualize);

            // Blur and visualize flow
            var downSampled = flowBuffer;// DownSample(flowBuffer, blurDownSample);
            //Blur(downSampled, blurIterations);
            // Graphics.Blit(downSampled, destination, flowMaterial, (int)Pass.Visualize);
            Graphics.Blit(downSampled, resultBuffer);



            // Whole optical flow thingy
            if (Input.acceleration.x < 0.2 || Input.acceleration.y < 0.2) {
                Texture2D texture = new Texture2D(resultBuffer.width, resultBuffer.height, TextureFormat.RGB24, false);

                Rect rectReadPicture = new Rect(0, 0, resultBuffer.width, resultBuffer.height);

                RenderTexture.active = resultBuffer;

                texture.ReadPixels(rectReadPicture, 0, 0);
                texture.Apply();

                RenderTexture.active = null; // added to avoid errors 

                Color32[] temp = texture.GetPixels32();

                for (int i = R.Length - 2; i >= 0; i--) {
                    R[i + 1] = R[i];
                    G[i + 1] = G[i];
                }
                R[0] = 0;
                G[0] = 0;
                for (int i = 0; i < temp.Length; i += 1) {

                    if (temp[i].r*0.3 > temp[i].g && temp[i].r > 30) {
                        R[0]++;
                    } else if (temp[i].g*0.3 > temp[i].r && temp[i].g > 30) {
                        G[0]++;
                    }
                }
                print(R[0] + " vs " + G[0]);


                //RenderTexture.ReleaseTemporary(downSampled);
            }
        }

        RenderTexture DownSample(RenderTexture source, int lod) {
            var dst = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
            source.filterMode = FilterMode.Bilinear;
            Graphics.Blit(source, dst);

            for (var i = 0; i < lod; i++) {
                var tmp = RenderTexture.GetTemporary(dst.width >> 1, dst.height >> 1, 0, dst.format);
                dst.filterMode = FilterMode.Bilinear;
                Graphics.Blit(dst, tmp, flowMaterial, (int)Pass.DownSample);
                RenderTexture.ReleaseTemporary(dst);
                dst = tmp;
            }

            return dst;
        }

        void Blur(RenderTexture source, int iterations) {
            var tmp0 = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
            var tmp1 = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
            var iters = Mathf.Clamp(iterations, 0, 10);

            Graphics.Blit(source, tmp0);
            for (var i = 0; i < iters; i++) {
                for (var pass = 2; pass < 4; pass++) {
                    tmp1.DiscardContents();
                    tmp0.filterMode = FilterMode.Bilinear;
                    Graphics.Blit(tmp0, tmp1, flowMaterial, pass);
                    var tmpSwap = tmp0;
                    tmp0 = tmp1;
                    tmp1 = tmpSwap;
                }
            }
            Graphics.Blit(tmp0, source);

            RenderTexture.ReleaseTemporary(tmp0);
            RenderTexture.ReleaseTemporary(tmp1);
        }

        protected RenderTexture CreateBuffer(int width, int height) {
            var rt = new RenderTexture(width, height, 0);
            rt.format = RenderTextureFormat.ARGBFloat;
            rt.wrapMode = TextureWrapMode.Repeat;
            rt.Create();
            return rt;
        }

    }

}


