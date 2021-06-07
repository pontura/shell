using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreensManager : MonoBehaviour
{
    public List<ScreenMain> all;
    public ScreenMain initialScreen;
    ScreenMain screenActive;

    private void Awake()
    {
        Events.GotoTo += GotoTo;
    }
    private void OnDestroy()
    {
        Events.GotoTo -= GotoTo;
    }
    void GotoTo(string screenName)
    {
        Reset();
        screenActive = GetScreen(screenName);
        screenActive.Show();
    }
    ScreenMain GetScreen(string screenName)
    {
        foreach (ScreenMain sm in all)
            if (sm.name == screenName)
                return sm;
        Debug.LogError("No screen for " + screenName);
        return null;
    }
    private void Start()
    {
        Reset();
        Loop();
    }
    private void Reset()
    {
        foreach (ScreenMain sm in all)
            sm.gameObject.SetActive(false);
    }
    void Loop()
    {
        if (Data.Instance.contentData.content.Count == 0)
            Invoke("Loop", 0.25f);
        else
            Init();
    }
    void Init()
    {
        GotoTo("GameScreen");
    }
}
