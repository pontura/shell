using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSignal : MonoBehaviour
{
    public Text field;
    public Text textField;

    public void Init(string text, string desc, Vector3 pos)
    {
        textField.text = desc;
        transform.position = pos;
        gameObject.SetActive(true);
        field.text = text;
    }
}
