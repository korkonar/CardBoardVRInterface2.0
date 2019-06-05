using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GazeControl : MonoBehaviour
{
	public Image imgGaze;
	public float totalTime;
	public bool gvrStatus;
	float gvrTimer;
    GameObject but;
    string butName;
    ScrollRectScript menuScroll;
    public bool usingGaze = true;

    // Start is called before the first frame update
    void Start()
    {
        totalTime = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (gvrStatus && usingGaze){
        	gvrTimer += Time.deltaTime;
        	imgGaze.fillAmount = gvrTimer / totalTime;
            if (imgGaze.fillAmount == 1){
                buttonClick();
            }
        }
    }

    void buttonClick() {
        if (butName == "Left") {
            gvrTimer = 0;
            imgGaze.fillAmount = 0;
            menuScroll.ButtonLeftIsPressed();
            return;
        }
        else if (butName == "Right") {
            gvrTimer = 0;
            imgGaze.fillAmount = 0;
            menuScroll.ButtonRightIsPressed();
            return;
        }
        else if(butName == "UseClap" || butName == "UseHeadTilt")
        {
            gvrTimer = 0;
            imgGaze.fillAmount = 0;
            but.GetComponent<Toggle>().isOn = true;
            return;
        }
        but.GetComponent<Button>().onClick.Invoke();
    }

    public void GVROn(GameObject b, string buttonName, ScrollRectScript menu){
        gvrTimer = 0;
        imgGaze.fillAmount = 0;
        but = b;
        butName = buttonName;
        menuScroll = menu;
    	gvrStatus = true;
    }

    public void GVROff(){
    	gvrStatus = false;
    	gvrTimer = 0;
    	imgGaze.fillAmount = 0;
    }
}
