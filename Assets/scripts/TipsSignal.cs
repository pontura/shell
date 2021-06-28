using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsSignal : MonoBehaviour
{
    public GameObject panel;
    public Text field;
    int id;

    void Start()
    {
        panel.SetActive(false);       
    }
    public void Init()
    {
        CancelInvoke();
        Loop();
    }
    public void SetOff()
    {
        CancelInvoke();
        panel.SetActive(false);
    }

    void Loop()
    {
        Events.PlaySound("ui", "Sounds/feedback_neutro", false);
        panel.SetActive(true);
        Invoke("Reset", 6);
        field.text = Data.Instance.contentData.tips[id];
        id++;
        if (id > Data.Instance.contentData.tips.Length - 1)
            id = 0;
    }
    private void Reset()
    {
        panel.SetActive(false);
        Invoke("Loop", 3);
    }
}
