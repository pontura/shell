using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    public Text field;
    public bool selected;

    public void OnInit(ContentData.SituacionData situacionData)
    {
        field.text = situacionData.accion;
    }
    public void OnClicked()
    {
        selected = !selected;
    }
}
