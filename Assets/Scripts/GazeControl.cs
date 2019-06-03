﻿using System.Collections;
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

    // Start is called before the first frame update
    void Start()
    {
        totalTime = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (gvrStatus){
        	gvrTimer += Time.deltaTime;
        	imgGaze.fillAmount = gvrTimer / totalTime;
            if (imgGaze.fillAmount == 1){
                but.onClick.Invoke();
            }
        }
    }

    public void GVROn(Button b){
        but = b;
    	gvrStatus = true;
    }

    public void GVROff(){
    	gvrStatus = false;
    	gvrTimer = 0;
    	imgGaze.fillAmount = 0;
    }
}