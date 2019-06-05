using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectScript : MonoBehaviour
{
    private ScrollRect scrollRect;
    private bool mouseDown, buttonRight, buttonLeft;
    
    //public float jumpSize;
    public float jumpStep, jumpSize;

    // Start is called before the first frame update
    void Start()
    {
        jumpSize = 210f / (this.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x - 500f);
        jumpStep = jumpSize / 8.0f;
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
        StartCoroutine(ScrollAnimationRight());
        //scrollRect.horizontalNormalizedPosition += 0.3f;
    }

    private void ScrollLeft()
    {
        mouseDown = false;
        buttonLeft = false;
        //scrollRect.horizontalNormalizedPosition -= 0.3f;
        StartCoroutine(ScrollAnimationLeft());
    }

    public IEnumerator ScrollAnimationLeft()
    {
        for (float f = 0.0f; f < jumpSize; f += jumpStep)
        {
            scrollRect.horizontalNormalizedPosition -= jumpStep;
            yield return null;
        }
    }

    public IEnumerator ScrollAnimationRight()
    {
        for (float f = 0.0f; f < jumpSize; f += jumpStep)
        {
            scrollRect.horizontalNormalizedPosition += jumpStep;
            yield return null;
        }
    }
}
