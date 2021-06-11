using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSignal : MonoBehaviour
{
    public Text field;

    public void Init(string text, Vector3 pos)
    {
        transform.position = pos;
        gameObject.SetActive(true);
        field.text = text;
    }
}
