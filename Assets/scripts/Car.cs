using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public RotateAutomatic[] ruedas;

    private void Start()
    {
            
    }
    public void SetState(bool isOn)
    {
        foreach (RotateAutomatic ra in ruedas)
            ra.enabled = isOn;
    }
}
