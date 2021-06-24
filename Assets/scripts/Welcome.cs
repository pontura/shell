using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Welcome : ScreenMain
{
    public Animation anim;
    public ButtonStandard button;
    bool isDone;

    public override void Init()
    {
        StartCoroutine(Timer());
        button.Init(0, GotoNext, ListManager.EventToListen.RELEASE);
        button.SetText("JUGAR!");
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(3);
        Events.HideOldScreens();
    }
    public void GotoNext(int id)
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

