using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScreen : ScreenMain
{
    public Parallax parallax;
    public Animator anim;

    public void Next()
    {
        anim.Play("off");
        parallax.StopInFade();
        Invoke("GotoNext", 3);
    }
    void GotoNext()
    {
        Events.GotoTo("Welcome");
    }
}
