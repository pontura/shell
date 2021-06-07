using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterScreen : MonoBehaviour
{

    public InputField nombreField;
    public InputField apellidoField;
    public InputField colegioField;
    public InputField scoreField;

    private void Start()
    {
        nombreField.text = Data.Instance.databaseManager.userData.nombre;
        apellidoField.text = Data.Instance.databaseManager.userData.apellido;
        colegioField.text = Data.Instance.databaseManager.userData.colegio;
        scoreField.text = Data.Instance.databaseManager.userData.score.ToString();
    }
    public void OnSubmit()
    {
        DatabaseManager.UsersData data = new DatabaseManager.UsersData();
        data.nombre = nombreField.text;
        data.apellido = apellidoField.text;
        data.colegio = colegioField.text;
        data.score = int.Parse(scoreField.text);
        Data.Instance.databaseManager.SaveScore(data);
    }
}
