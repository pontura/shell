﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterScreen : ScreenMain
{
    public IntroScreen intro;
    public InputField nombreField;
    public InputField apellidoField;
    public InputField colegioField;
    public Text feedbackField;

    public override void Init()
    {
        feedbackField.text = "";
        nombreField.text = Data.Instance.databaseManager.userData.nombre;
        apellidoField.text = Data.Instance.databaseManager.userData.apellido;
        colegioField.text = Data.Instance.databaseManager.userData.colegio;
        Events.HideOldScreens();
        intro.ShowBackgroundOnly();
    }
    public void OnSubmit()
    {
        if(nombreField.text.Length<3)
        {
            FeedbcakText("Escribe tu nombre real"); return;
        }
        if (apellidoField.text.Length < 3)
        {
            FeedbcakText("Escribe tu apellido real"); return;
        }
        if (colegioField.text.Length < 3)
        {
            FeedbcakText("Escribe tu colegio real"); return;
        }
        DatabaseManager.UsersData data = new DatabaseManager.UsersData();
        data.nombre = nombreField.text;
        data.apellido = apellidoField.text;
        data.colegio = colegioField.text;
        data.score = Data.Instance.progressData.score;
        Data.Instance.databaseManager.SaveScore(data);
    }
    void FeedbcakText(string text)
    {
        feedbackField.text = text;
        CancelInvoke();
        Invoke("Reset", 3);
    }
    void Reset()
    {
        feedbackField.text = "";
    }
}
