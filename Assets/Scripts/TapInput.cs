using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapInput : MonoBehaviour
{
    public GameObject yes;

    AudioClip microphoneInput;
    bool microphoneInitialized;
    public float sensitivity;
    public bool flapped;

    private Vector3 previousAccel;
    private int coolDown = 0;
    private int doubleClapCooldown = 0;
    private float i;
    private float prevI = 0;
    private bool spike1 = false;
    private bool spike2 = false;


    void Awake(){
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 60;

        if (Microphone.devices.Length>0){
            microphoneInput = Microphone.Start(Microphone.devices[0],true,999,44100);
            microphoneInitialized = true;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        previousAccel = new Vector3(0,0,0);
    }

    void Update(){
        //get mic volume
        coolDown--;
        if(doubleClapCooldown > 0){
            doubleClapCooldown--;
        }
        if(coolDown > 0){
            int dec = 128;
            float[] waveData = new float[dec];
            int micPosition = Microphone.GetPosition(null)-(dec+1); // null means the first microphone
            microphoneInput.GetData(waveData, micPosition);

            // Getting a peak on the last 128 samples
            float levelMax = 0;
            for (int i = 0; i < dec; i++) {
                float wavePeak = waveData[i] * waveData[i];
                if (levelMax < wavePeak) {
                    levelMax = wavePeak;
                }
            }
            float level = Mathf.Sqrt(Mathf.Sqrt(levelMax));
            i += level;
        }else{
            coolDown = 10;

            int dec = 128;
            float[] waveData = new float[dec];
            int micPosition = Microphone.GetPosition(null)-(dec+1); // null means the first microphone
            microphoneInput.GetData(waveData, micPosition);

            // Getting a peak on the last 128 samples
            float levelMax = 0;
            for (int i = 0; i < dec; i++) {
                float wavePeak = waveData[i] * waveData[i];
                if (levelMax < wavePeak) {
                    levelMax = wavePeak;
                }
            }
            float level = Mathf.Sqrt(Mathf.Sqrt(levelMax));
            i += level;

            i = i/20;
            GameObject.Find("Gyro").GetComponent<UnityEngine.UI.Text>().text = i.ToString();
            
            if(Mathf.Abs(i - prevI) < 0.05f){
                if(spike2){
                    doubleClapCooldown = 60;
                }
                spike1 = false;
                spike2 = false;
                yes.GetComponent<UnityEngine.UI.Text>().text = "";
            }else if(!spike1){
                spike1 = true;
            }else{
                if(doubleClapCooldown > 0){
                    yes.GetComponent<UnityEngine.UI.Text>().text = "double clap";
                }else{
                    spike2 = true;
                    yes.GetComponent<UnityEngine.UI.Text>().text = "clap";
                }
            }

            prevI = i;
            i = 0;
        }

    }
    // Update is called once per frame
    // void FixedUpdate()
    // {
    //     coolDown--;
    //     if(coolDown > 0){
    //         i += (Input.gyro,attitude - previousAccel);
    //         previousAccel = Input.acceleration;
    //     }else{
    //         coolDown = 10;
    //         i = i / 10;
    //         string txt = i.x.ToString("0.0000") + " " + i.y.ToString("0.0000") + " " + i.z.ToString("0.0000");
    //         GameObject.Find("Gyro").GetComponent<UnityEngine.UI.Text>().text = txt;
    //         i = (Input.acceleration - previousAccel);
    //         previousAccel = Input.acceleration;
    //     }
    //     // string txt = i.x.ToString("0.000") + " " + i.y.ToString("0.000") + " " + i.z.ToString("0.000");

    //     // if(i.y >= 0.001){
    //     //     yes.SetActive(true);
    //     //     coolDown = 400;
    //     // }else if(coolDown == 0){
    //     //     yes.SetActive(false);
    //     // }

    //     // if(coolDown > 0){
    //     //     coolDown--;
    //     // }
    // }
}
