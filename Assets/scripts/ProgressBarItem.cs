using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarItem : MonoBehaviour
{
    public Animation anim;

    public void SetState(int id)
    {
        if (id == 0)
            anim.Play("on");
        else if(id == 1)
            anim.Play("active");
        else
            anim.Play("off");
    }
}
