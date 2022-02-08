using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiscoreScreen : MonoBehaviour
{
    public GameObject panel;
    public HiscoreUI hiscoreUI;

    public void Init()
    {
        return;
        panel.SetActive(true);
        hiscoreUI.Init();
    }
    public void Close()
    {
        panel.SetActive(false);
    }
}
