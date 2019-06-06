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
    public Text debug2;
    public GameObject selected;

    // Start is called before the first frame update
    void Start()
    {
        previousAcc = Input.acceleration;
        inicialAcc = new Vector3(previousAcc.x, previousAcc.y, 0);
        actionPerformed = false;
        enter = false;
        reseted = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameObject.Find("UIManager").GetComponent<UIManager>().interAction == "Tilt")
        {

            actionPerformed = false;
            //print(Input.acceleration);
            //debug.transform.parent.gameObject.SetActive(true);
            //debug.text = (Input.acceleration.z - previousAcc.z).ToString();
            if (!reseted && (Input.acceleration.z >= inicialAcc.z-0.01f && Input.acceleration.z <= inicialAcc.z + 0.01f))
            {
                reseted = true;
            }
            debug2.text = reseted.ToString();
            if (reseted && (Input.acceleration.z - previousAcc.z >= 0.2 || Input.GetKeyDown(KeyCode.A)))
            {

                debug.text = "Positive Selection";
                selected.GetComponent<Button>().onClick.Invoke();
                if(selected.name == "Right")
                {
                    selected.GetComponent<ButtonScript>().scrollRectScript.ButtonRightIsPressed();
                }else if (selected.name == "Left")
                {
                    selected.GetComponent<ButtonScript>().scrollRectScript.ButtonLeftIsPressed();
                }
                positiveAction = true;
                actionPerformed = true;
                reseted = false;
            }
            else if (reseted && (Input.acceleration.z - previousAcc.z <= -0.3 || Input.GetKeyDown(KeyCode.S)))
            {
                debug.text = "Negative Selection";
                foreach (GameObject go in GameObject.FindGameObjectsWithTag("Back"))
                {
                    if (go.activeInHierarchy)
                    {
                        go.GetComponent<Button>().onClick.Invoke();
                        break;
                    }
                }
                //selected.GetComponent<Button>().onClick.Invoke();
                positiveAction = false;
                actionPerformed = true;
                reseted = false;
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
        yield return new WaitForSeconds(1.0f);

        if (selected == obj && !enter)
        {
            selected = null;
        }
    }
}
