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

        data = "User ID:" + PlayerPrefs.GetInt("userID") + "\n";

        path = Path.Combine(Application.persistentDataPath, "data.json");
        Debug.Log(data);
        AppendData();
    }

    private void Update()
    {
        if (timer)
        {
            timeOfAction += Time.deltaTime;
        }
    }

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
        Debug.Log("test");
        GameObject info = GameObject.Find("Norwegian Forest Cat");
        if (action == interAction)
        {
            timer = false;
            data = "Action: " + action + " Time: " + timeOfAction + "\n";
            Debug.Log(data);
            AppendData();
            switch (action)
            {
                case "Clap":
                    info = GameObject.Find("Cape Jackal");
                    break;
                case "Wave":
                    info = GameObject.Find("Persian leopard");
                    break;
                case "Tilt":
                    info = GameObject.Find("Kiang");
                    break;
            }
            info.transform.Find("Video Player").gameObject.SetActive(true);
            info.GetComponent<RawImage>().enabled = true;
            info.transform.Find("Text").gameObject.SetActive(false);
        }
    }

    public void DisableToggles(GameObject parent) {
        //GameObject.Find("UseClap").SetActive(false);
        //GameObject.Find("UseHeadTilt").SetActive(false);
        //GameObject.Find("UseWave").SetActive(false);
        parent.transform.Find("UseClap").gameObject.SetActive(false);
        parent.transform.Find("UseHeadTilt").gameObject.SetActive(false);
        parent.transform.Find("UseWave").gameObject.SetActive(false);
    }

    public void EnableToggles(GameObject parent)
    {
        parent.transform.Find("UseClap").gameObject.SetActive(true);
        parent.transform.Find("UseHeadTilt").gameObject.SetActive(true);
        parent.transform.Find("UseWave").gameObject.SetActive(true);
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
                    GameObject.Find("Player").GetComponent<GazeControl>().usingGaze = false;
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

    public void AppendData()
    {
        File.AppendAllText(path, data);
    }

    public void DeserializeData()
    {
        string loadedData = File.ReadAllText(path);
        Debug.Log(loadedData);
     }
}
