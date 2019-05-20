using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectScript : MonoBehaviour
{
    private ScrollRect scrollRect;
    private bool mouseDown, buttonDown, buttonUp;

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
            if (buttonDown)
            {
                ScrollDown();
            }
            else if (buttonUp)
            {
                ScrollUp();
            }
        }
    }

    public void ButtonDownIsPressed()
    {
        mouseDown = true;
        buttonDown = true;
    }

    public void ButtonUpIsPressed()
    {
        mouseDown = true;
        buttonUp = true;
    }

    private void ScrollDown()
    {
        mouseDown = false;
        buttonDown = false;
        scrollRect.verticalNormalizedPosition -= 0.5f;
    }

    private void ScrollUp()
    {
        mouseDown = false;
        buttonUp = false;
        scrollRect.verticalNormalizedPosition += 0.5f;
    }
}
