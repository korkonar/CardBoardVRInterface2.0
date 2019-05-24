using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void DisableFirstToSecond(Animator anim)
    {
        anim.SetBool("FirstToSecond", false);
    }

    public void EnableFirstToSecond(Animator anim)
    {
        anim.SetBool("FirstToSecond", true);
    }

    public void DisableSecondToThird(Animator anim)
    {
        anim.SetBool("SecondToThird", false);
    }

    public void EnableSecondToThird(Animator anim)
    {
        anim.SetBool("SecondToThird", true);
    }
}
