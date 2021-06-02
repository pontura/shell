using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiscoreUI : MonoBehaviour
{
    public HiscoreLine line;
    public Transform container;

    void Start()
    {
        LoopTillLoaded();
    }
    void LoopTillLoaded()
    {
        if (Data.Instance.databaseManager.hiscore.all.Length == 0)
            Invoke("LoopTillLoaded", 0.5f);
        else
            LoadHiscores();
    }
    void LoadHiscores()
    {
        int puesto = 1;
        foreach (DatabaseManager.UsersData data in Data.Instance.databaseManager.hiscore.all)
        {
            HiscoreLine hl = Instantiate(line, container);
            hl.transform.localScale = Vector3.one;
            hl.Init(puesto, data);
            puesto++;
        }
    }
}
