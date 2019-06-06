using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;

public class UIManager : MonoBehaviour
{
    private float timeOfAction;
    public string interAction;
    private bool timer = false;

    //public JsonData data;
    private string data;

    private string path;

    private void Start()
    {
        interAction = "Stare";
        if (!PlayerPrefs.HasKey("userID"))
        {
            PlayerPrefs.SetInt("userID", 1);
        }
        else
        {
            PlayerPrefs.SetInt("userID", PlayerPrefs.GetInt("userID") + 1);
        }

        //data = new JsonData("User ID", PlayerPrefs.GetInt("userID"));
        data = "User ID:" + PlayerPrefs.GetInt("userID") + "\n";

        path = Path.Combine(Application.persistentDataPath, "data.json");
        Debug.Log(data);
        AppendData();
        //DeserializeData();
    }

    private void Update()
    {
        if (timer)
        {
            timeOfAction += Time.deltaTime;
        }
    }

    //public void setInterAction(string checkedAction)
    //{
    //    interAction = checkedAction;
    //    if (interAction == "Clap")
    //    {
    //        GameObject.Find("Player").GetComponent<GazeControl>().usingGaze = false;
    //    }
    //    else if(interAction == "Tilt")
    //    {
    //        GameObject.Find("Player").GetComponent<GazeControl>().usingGaze = false;
    //    }
    //    else
    //    {
    //        GameObject.Find("Player").GetComponent<GazeControl>().usingGaze = true;
    //    }
    //}

    //public void DisableFirstToSecond(Animator anim)
    //{
    //    anim.SetBool("FirstToSecond", false);
    //}

    //public void EnableFirstToSecond(Animator anim)
    //{
    //    anim.SetBool("FirstToSecond", true);
    //}

    //public void DisableSecondToThird(Animator anim)
    //{
    //    anim.SetBool("SecondToThird", false);
    //}

    //public void EnableSecondToThird(Animator anim)
    //{
    //    anim.SetBool("SecondToThird", true);
    //}
    public void EnableDisplayed(Animator anim)
    {
        anim.SetBool("isDisplayed", true);
    }

    public void DisableDisplayed(Animator anim)
    {
        anim.SetBool("isDisplayed", false);
    }

    public void EnableOutside(Animator anim)
    {
        anim.SetBool("isOutside", true);
    }

    public void DisableOutside(Animator anim)
    {
        anim.SetBool("isOutside", false);
    }

    public void EnableObject(Animator anim)
    {
        anim.SetBool("disableObject", false);
    }

    public void DisableObject(Animator anim)
    {
        anim.SetBool("disableObject", true);
    }

    public void StartTimer()
    {
        timeOfAction = 0.0f;
        timer = true;
    }

    public void EndTimer(string action)
    {
        if (action == interAction)
        {
            timer = false;
            //data = new JsonData(action, timeOfAction);
            data = "Action: " + action + " Time: " + timeOfAction + "\n";
            Debug.Log(data);
            AppendData();
            //DeserializeData();
            //WriteString(action, timeOfAction);
        }
    }

     public void DisableToggles() {
        GameObject.Find("UseClap").SetActive(false);
        GameObject.Find("UseHeadTilt").SetActive(false);
        GameObject.Find("UseWave").SetActive(false);
    }

    public void OnToggle(string type) {
        switch (type) {
            case "Clap":
                if (GameObject.Find("UseClap").GetComponent<Toggle>().isOn) {
                    GameObject.Find("UseWave").GetComponent<Toggle>().isOn = false;
                    GameObject.Find("UseHeadTilt").GetComponent<Toggle>().isOn = false;
                    interAction = type;
                    GameObject.Find("Player").GetComponent<GazeControl>().usingGaze = false;
                } else {
                    interAction = "Stare";
                    GameObject.Find("Player").GetComponent<GazeControl>().usingGaze = true;
                }
                break;
            case "Wave":
                if (GameObject.Find("UseWave").GetComponent<Toggle>().isOn) {
                    GameObject.Find("UseClap").GetComponent<Toggle>().isOn = false;
                    GameObject.Find("UseHeadTilt").GetComponent<Toggle>().isOn = false;
                    interAction = type;
                    GameObject.Find("Player").GetComponent<GazeControl>().usingGaze = true;
                } else {
                    interAction = "Stare";
                    GameObject.Find("Player").GetComponent<GazeControl>().usingGaze = true;
                }
                break;
            case "Tilt":
                if (GameObject.Find("UseHeadTilt").GetComponent<Toggle>().isOn) {
                    GameObject.Find("UseClap").GetComponent<Toggle>().isOn = false;
                    GameObject.Find("UseWave").GetComponent<Toggle>().isOn = false;
                    interAction = type;
                    GameObject.Find("Player").GetComponent<GazeControl>().usingGaze = false;
                } else {
                    interAction = "Stare";
                    GameObject.Find("Player").GetComponent<GazeControl>().usingGaze = true;
                }
                break;
        }
    }

    //static void WriteString(string action, float timeOfAction)
    //{
    //    string path = "Assets/Resources/log.txt";

    //    //Write some text to the test.txt file
    //    StreamWriter writer = new StreamWriter(path, true);
    //    writer.WriteLine(action + " " + timeOfAction);
    //    writer.Close();

    //    //Re-import the file to update the reference in the editor
    //    AssetDatabase.ImportAsset(path);
    //    TextAsset asset = Resources.Load<TextAsset>("log");

    //    //Print the text from the file
    //    //Debug.Log(asset.text);
    //}

    public void AppendData()
    {
        //string jsonDataString = JsonUtility.ToJson(data, true);

        File.AppendAllText(path, data);
    }

    public void DeserializeData()
    {
        string loadedData = File.ReadAllText(path);

        //data = JsonUtility.FromJson<JsonData>(loadedJsonDataString);

        //Debug.Log("action: " + data.action.ToString() + " | time: " + data.time);
        Debug.Log(loadedData);
     }
}

[System.Serializable]
public class JsonData
{
    public string action;
    public float time;

    public JsonData(string action, float time)
    {
        this.action = action;
        this.time = time;
    }

    public JsonData() { }
}
