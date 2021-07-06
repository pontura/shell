using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DatabaseManager : MonoBehaviour
{
    string hashPassword = "pontura";
    string url = "http://www.pontura.com/shell/";
    public UsersData userData;
    public HiscoreData hiscore;

    [Serializable]
    public class HiscoreData
    {
        public List<UsersData> all;
    }

    [Serializable]
    public class UsersData
    {
        public int id;
        public int score;
        public string nombre;
        public string apellido;
        public string colegio;
    }
    void Awake()
    {
        GetUserData();
        GetHiscore();
    }
    private void GetUserData()
    {
        if (PlayerPrefs.GetString("nombre") != "")
        {
            userData.id = PlayerPrefs.GetInt("id");
            userData.nombre = PlayerPrefs.GetString("nombre");
            userData.apellido = PlayerPrefs.GetString("apellido");
            userData.colegio = PlayerPrefs.GetString("colegio");
            userData.score = PlayerPrefs.GetInt("score");
        }
    }
    private void SetUserData()
    {
        PlayerPrefs.SetString("nombre", userData.nombre);
        PlayerPrefs.SetString("apellido", userData.apellido);
        PlayerPrefs.SetString("colegio", userData.colegio);
        PlayerPrefs.SetInt("score", userData.score);
        PlayerPrefs.SetInt("id", userData.id);

        GetUserData();
    }
    public void GetHiscore()
    {
        StartCoroutine(LoadJson(url + "getHiscore.php", OnHiscoreDone));
    }
    public void SaveNewScoreToExistingUser()
    {
        userData.score = Data.Instance.progressData.score;
        SaveScore(userData);
    }
    public void SaveNewScore(string name, string lastName, string school)
    {
        UsersData data = new UsersData();
        data.nombre = name;
        data.apellido = lastName;
        data.colegio = school;
        data.score = Data.Instance.progressData.score;
        SaveScore(data);
    }
    public void SaveScore(UsersData userdata)
    {
        userdata.id = userData.id;
        this.userData = userdata;
        string hash = Md5Test.Md5Sum(userdata.nombre + userdata.score + hashPassword);
        string urlReal = url
            + "setUser.php?nombre=" + userdata.nombre
            + "&apellido=" + userdata.apellido
            + "&colegio=" + userdata.colegio
            + "&score=" + userdata.score
            + "&id=" + userdata.id
            + "&hash=" + hash;
        print("save: " + urlReal);
        StartCoroutine(LoadJson(urlReal, OnUserDataSaved));
    }
    void OnUserDataSaved(string result)
    {
        print("result " + result);
        userData.id = int.Parse(result);
        RefreshHiscores();
        SetUserData();
    }
    IEnumerator LoadJson(string url, System.Action<string> OnDone)
    {
        print(url);
        WWW www = new WWW(url);
        yield return www;
        if (www.error == null)
        {
            if (OnDone != null)
                OnDone(www.text);
        }
        else
        {
            Debug.Log("ERROR: " + www.error);
        }
    }
    void OnHiscoreDone(string result)
    {
        hiscore = JsonUtility.FromJson<HiscoreData>(result);
    }
    void RefreshHiscores()
    {
        foreach(UsersData uData in  hiscore.all)
        {
            if(uData.apellido == userData.apellido && uData.nombre == userData.nombre)
            {
                uData.score = userData.score;
            }
        }
        hiscore.all.Sort((p1, p2) => p1.score.CompareTo(p2.score));
        hiscore.all.Reverse();
    }
}
