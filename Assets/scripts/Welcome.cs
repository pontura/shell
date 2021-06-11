using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Welcome : ScreenMain
{
    public Animation anim;

    public override void Init()
    {
        StartCoroutine(Timer());
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(2);
        Events.HideOldScreens();
    }
    public void GotoNext()
    {
        anim.Play("off");
        Events.GotoTo("GameScreen");
        Invoke("Reset", 2);
    }
    void Reset()
    {
        gameObject.SetActive(false);
    }
}

