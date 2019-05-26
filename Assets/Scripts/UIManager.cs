using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class UIManager : MonoBehaviour
{
    private float timeOfAction;
    public string action;
    private bool timer = false;

    private void Update()
    {
        if (timer)
        {
            timeOfAction += Time.deltaTime;
        }
    }

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

    public void EndTimer()
    {
        timer = false;
        WriteString(action, timeOfAction);
    }

    static void WriteString(string action, float timeOfAction)
    {
        string path = "Assets/Resources/log.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(action + " " + timeOfAction);
        writer.Close();

        //Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(path);
        TextAsset asset = Resources.Load<TextAsset>("log");

        //Print the text from the file
        //Debug.Log(asset.text);
    }

}
