using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiscoreLine : MonoBehaviour
{
    public Text puestoField;
    public Text field;
    public Text scoreField;

    public void Init(int id, DatabaseManager.UsersData data)
    {
        puestoField.text = id.ToString();
        field.text = data.nombre + " " + data.apellido + " (" + data.colegio + ")";
        scoreField.text = data.score.ToString();
    }
}
