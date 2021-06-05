using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMain : MonoBehaviour
{
    public virtual void Show()
    {
        gameObject.SetActive(true);
        Init();
    }
    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
    public virtual void Init() { }
}
