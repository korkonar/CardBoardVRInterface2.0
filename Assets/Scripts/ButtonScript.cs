using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ButtonScript : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private ScrollRectScript scrollRectScript;
    [SerializeField]
    private bool isRightButton;

    public void OnPointerDown (PointerEventData eventData)
    {
        if (isRightButton)
        {
            scrollRectScript.ButtonRightIsPressed();
        }
        else
        {
            scrollRectScript.ButtonLeftIsPressed();
        }
    }
}
