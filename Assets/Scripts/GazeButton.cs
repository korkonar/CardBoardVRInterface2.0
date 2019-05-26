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
        //Output to console the GameObject's name and the following message
        Debug.Log("Cursor Entering " + gameObject.GetComponent<Button>() + " GameObject");
        //gameObject.GetComponent<Button>().onClick.Invoke();
        player.GVROn(gameObject.GetComponent<Button>());
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Output the following message with the GameObject's name
        Debug.Log("Cursor Exiting " + name + " GameObject");
        player.GVROff();
    }
}
