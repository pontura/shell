using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CongatsScreen : ScreenMain
{
    public Text scoreField;
    public HiscoreScreen hiscoreScreen;
    public IntroScreen intro;

    public override void Init()
    {
        scoreField.text = Data.Instance.progressData.score.ToString();
    }
    public void Next()
    {
        Events.HideOldScreens();
        intro.AllOn();
        Invoke("Delayed", 0.5f);
    }
    void Delayed()
    {
        gameObject.SetActive(false);
        hiscoreScreen.Init();
    }
}
