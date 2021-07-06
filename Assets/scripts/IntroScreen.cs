using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScreen : ScreenMain
{
    public Parallax parallax;
    public Animator anim;
    public ButtonStandard readyButton;
    public HiscoreUI hiscoreUI;
    public HiscoreScreen hiscoreScreen;
    public TipsSignal tipsSignal;

    private void Start()
    {
        readyButton.Init(0, OnClicked, ListManager.EventToListen.RELEASE);
        readyButton.SetText("JUGAR");
    }
    public override void Init()
    {
        base.Init();
        hiscoreUI.Init();
        Invoke("TipsOn", 3);
    }
    void TipsOn()
    {
        tipsSignal.Init();
    }
    public void OnClicked( int id )
    {
        anim.Play("off");
        parallax.StopInFade();
        Events.GotoTo("Welcome");
        tipsSignal.SetOff();
    }
    public void ShowBackgroundOnly()
    {
        gameObject.SetActive(true);
        anim.Play("onlyBG");
        parallax.Move();
    }
    public void AllOn()
    {
        gameObject.SetActive(true);
        anim.Play("all");
        parallax.Move();
        hiscoreUI.LoadHiscores();
        Events.ResetApp();
    }
    public void OpenHiscores()
    {
        hiscoreScreen.Init();
    }
}
