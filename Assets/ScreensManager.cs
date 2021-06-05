using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreensManager : MonoBehaviour
{
    public List<ScreenMain> all;
    public ScreenMain initialScreen;

    private void Start()
    {
        foreach (ScreenMain sm in all)
            sm.gameObject.SetActive(false);
        Loop();
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
        initialScreen.Show();
    }
}
