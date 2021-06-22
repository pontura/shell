using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Welcome : ScreenMain
{
    public Animation anim;
    bool isDone;

    public override void Init()
    {
        StartCoroutine(Timer());
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(3);
        Events.HideOldScreens();
    }
    public void GotoNext()
    {
        if (isDone) return;
        isDone = true;
        //anim.Play("off");
        Events.GotoTo("Map");
        Invoke("Reset", 2);
    }
    void Reset()
    {
        gameObject.SetActive(false);
    }
}

