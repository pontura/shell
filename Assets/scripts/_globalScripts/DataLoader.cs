using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    public string url;
    System.Action OnReady;
    public TextAsset textAsset;

    public void LoadData(System.Action OnReady)
    {
        this.OnReady = OnReady;
        string url_by_lang;

        // Data.Instance.spreadsheetLoader.LoadFromTo(url, OnLoaded);
        Data.Instance.spreadsheetLoader.CreateListFromFile(textAsset.text, OnLoaded);
    }
    public virtual void OnLoaded(List<SpreadsheetLoader.Line> d) {
        OnReady();
    }
    public virtual void Reset() { }

}
