using GoogleVR.HelloVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeadTiltSelection : MonoBehaviour
{
    private Vector3 inicialAcc;
    private Vector3 previousAcc;
    private bool actionPerformed;
    bool reseted;
    bool positiveAction;
    static bool enter;
    public Text debug;
    public GameObject selected;

    // Start is called before the first frame update
    void Start()
    {
        previousAcc = Input.acceleration;
        inicialAcc = previousAcc;
        actionPerformed = false;
        enter = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("UIManager").GetComponent<UIManager>().interAction == "Tilt")
        {

            actionPerformed = false;
            //print(Input.acceleration);
            //debug.transform.parent.gameObject.SetActive(true);
            //debug.text = Input.acceleration.ToString();
            if (!reseted && Input.acceleration.z == inicialAcc.z)
            {
                reseted = true;
            }

            if (reseted && Input.acceleration.z - previousAcc.z >= 0.2)
            {

                debug.text = "Positive Selection";
                selected.GetComponent<Button>().onClick.Invoke();
                positiveAction = true;
                actionPerformed = true;
            }
            else if (reseted && Input.acceleration.z - previousAcc.z <= -0.2)
            {
                debug.text = "Negative Selection";
                foreach(GameObject go in GameObject.FindGameObjectsWithTag("Back"))
                {
                    if (go.activeSelf)
                    go.GetComponent<Button>().onClick.Invoke();
                }
                //selected.GetComponent<Button>().onClick.Invoke();
                positiveAction = false;
                actionPerformed = true;
            }

            previousAcc = Input.acceleration;
        }
    }

    //public bool getPAction()
    //{
    //    if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        return true;
    //    }
    //    if (actionPerformed)
    //    {
    //        if (positiveAction)
    //        {
    //            return true;
    //        }
    //    }
    //    return false;
    //}
    //
    //public bool getNAction()
    //{
    //    if (Input.GetKeyDown(KeyCode.S))
    //    {
    //        return true;
    //    }
    //    if (actionPerformed)
    //    {
    //        if (!positiveAction)
    //        {
    //            return true;
    //        }
    //    }
    //    return false;
    //}


    public void setSelectedObject(GameObject obj)
    {
        selected = obj;
        enter = true;
    }

    public void notSelectedObject(GameObject obj)
    {
        StartCoroutine(ExecuteAfterTime(obj));
        enter = false;
        
    }

    IEnumerator ExecuteAfterTime(GameObject obj)
    {
        yield return new WaitForSeconds(0.5f);

        if (selected == obj && !enter)
        {
            selected = null;
        }
    }
}
