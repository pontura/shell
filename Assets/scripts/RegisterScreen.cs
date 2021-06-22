using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterScreen : ScreenMain
{
    public IntroScreen intro;
    public InputField nombreField;
    public InputField apellidoField;
    public InputField colegioField;

    public override void Init()
    {
        nombreField.text = Data.Instance.databaseManager.userData.nombre;
        apellidoField.text = Data.Instance.databaseManager.userData.apellido;
        colegioField.text = Data.Instance.databaseManager.userData.colegio;
        Events.HideOldScreens();
        intro.ShowBackgroundOnly();
    }
    public void OnSubmit()
    {
        DatabaseManager.UsersData data = new DatabaseManager.UsersData();
        data.nombre = nombreField.text;
        data.apellido = apellidoField.text;
        data.colegio = colegioField.text;
        data.score = 100;
        Data.Instance.databaseManager.SaveScore(data);
    }
}
