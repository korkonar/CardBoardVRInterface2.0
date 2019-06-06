using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GazeControl : MonoBehaviour
{
	public Image imgGaze;
	public float totalTime;
	bool gvrStatus;
	float gvrTimer;
    Button but;
    string butName;
    ScrollRectScript menuScroll;

    // Start is called before the first frame update
    void Start()
    {
        totalTime = 1.5;
    }

    // Update is called once per frame
    void Update()
    {
        if (gvrStatus){
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
        if (butName == "Right") {
            gvrTimer = 0;
            imgGaze.fillAmount = 0;
            menuScroll.ButtonRightIsPressed();
            return;
        }
        var pointer = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(but.gameObject, pointer, ExecuteEvents.submitHandler);
    }

    public void GVROn(Button b, string buttonName, ScrollRectScript menu){
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
