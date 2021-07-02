using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public ProgressBarItem[] all;

    void Awake()
    {
        all = GetComponentsInChildren<ProgressBarItem>();
    }
    public void Refresh()
    {
        int id = 0;
        foreach (ProgressBarItem item in all)
        {
            if (Data.Instance.contentData.id < id)
                item.SetState(2);
            else if (Data.Instance.contentData.id == id)
                item.SetState(1);
            else
                item.SetState(0);
            id++;
        }
    }

}
