using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : ButtonStandard
{
    public Animation anim;


    public override void OnInit() { 
        anim.Play("ready");
    }
}
