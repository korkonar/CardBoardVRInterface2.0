using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //public void DisableFirstToSecond(Animator anim)
    //{
    //    anim.SetBool("FirstToSecond", false);
    //}

    //public void EnableFirstToSecond(Animator anim)
    //{
    //    anim.SetBool("FirstToSecond", true);
    //}

    //public void DisableSecondToThird(Animator anim)
    //{
    //    anim.SetBool("SecondToThird", false);
    //}

    //public void EnableSecondToThird(Animator anim)
    //{
    //    anim.SetBool("SecondToThird", true);
    //}
    public void EnableDisplayed(Animator anim)
    {
        anim.SetBool("isDisplayed", true);
    }

    public void DisableDisplayed(Animator anim)
    {
        anim.SetBool("isDisplayed", false);
    }

    public void EnableOutside(Animator anim)
    {
        anim.SetBool("isOutside", true);
    }

    public void DisableOutside(Animator anim)
    {
        anim.SetBool("isOutside", false);
    }

    public void EnableObject(Animator anim)
    {
        anim.SetBool("disableObject", false);
    }

    public void DisableObject(Animator anim)
    {
        anim.SetBool("disableObject", true);
    }
}
