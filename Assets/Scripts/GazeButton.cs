using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GazeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public GazeControl player;
    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
    	ScrollRectScript menu = null;
    	string buttonName = null;
        buttonName = gameObject.name;
        //Check if it's a left or right button
        foreach (Transform child in gameObject.transform.parent.gameObject.transform) {
      		//child is your child transform
      		//Debug.Log("Cursor Entering " + child.gameObject.name + " GameObject");
      		if (child.gameObject.name == "ScrollRect"){
      			//Debug.Log("find ScrollRect " + gameObject.name);
      			menu = child.gameObject.GetComponent(typeof(ScrollRectScript)) as ScrollRectScript;
      			//menu.ButtonLeftIsPressed();
      		}
    	}
        //gameObject.GetComponent<Button>().onClick.Invoke();
        player.GVROn(gameObject, buttonName, menu);
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Output the following message with the GameObject's name
        Debug.Log("Cursor Exiting " + name + " GameObject");
        player.GVROff();
    }
}
