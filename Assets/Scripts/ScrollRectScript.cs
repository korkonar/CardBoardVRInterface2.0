using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectScript : MonoBehaviour
{
    private ScrollRect scrollRect;
    private bool mouseDown, buttonRight, buttonLeft;

    // Start is called before the first frame update
    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseDown)
        {
            if (buttonRight)
            {
                ScrollRight();
            }
            else if (buttonLeft)
            {
                ScrollLeft();
            }
        }
    }

    public void ButtonRightIsPressed()
    {
        mouseDown = true;
        buttonRight = true;
    }

    public void ButtonLeftIsPressed()
    {
        mouseDown = true;
        buttonLeft = true;
    }

    private void ScrollRight()
    {
        mouseDown = false;
        buttonRight = false;
        scrollRect.horizontalNormalizedPosition += 0.3f;
    }

    private void ScrollLeft()
    {
        mouseDown = false;
        buttonLeft = false;
        scrollRect.horizontalNormalizedPosition -= 0.3f;
    }
}
