using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentData : DataLoader
{
    public List<Content> content;
    public int id;

    [System.Serializable]
    public class Content
    {
        public string situacion;
        public List<SituacionData> situaciones;
    }
    [System.Serializable]
    public class SituacionData
    {
        public ProgressData.Result value;
        public string accion;
        public string feedback;
        public string feedbackNo;
    }
    public void Next()
    {
        id++;
    }
    public override void Reset()
    {
        content.Clear();
    }
    public override void OnLoaded(List<SpreadsheetLoader.Line> d)
    {
        OnDataLoaded(content, d);
        base.OnLoaded(d);
    }
    void OnDataLoaded(List<Content> content, List<SpreadsheetLoader.Line> d)
    {
        int colID = 0;
        int rowID = 0;
        Content contentLine = null;
        SituacionData sLine = new SituacionData();
        foreach (SpreadsheetLoader.Line line in d)
        {
            foreach (string value in line.data)
            {
                //print("row: " + rowID + "  colID: " + colID + "  value: " + value);
                if (rowID >= 1)
                {
                    if (colID == 0)
                    {
                        if (value != "")
                        {
                            contentLine = new Content();
                            contentLine.situacion = value;
                            content.Add(contentLine);
                            contentLine.situaciones = new List<SituacionData>();                            
                        }
                        sLine = new SituacionData();
                        contentLine.situaciones.Add(sLine);
                    }
                    else
                    {
                        if (colID == 1 && value != "")
                            sLine.value = ProgressData.Result.BIEN;
                        if (colID == 2 && value != "")
                            sLine.value = ProgressData.Result.NEUTRO;
                        if (colID == 3 && value != "")
                            sLine.value = ProgressData.Result.MAL;

                        if (colID == 4 && value != "")
                            sLine.accion = value;
                        if (colID == 5 && value != "")
                            sLine.feedback = value;
                        if (colID == 6 && value != "")
                            sLine.feedbackNo = value;
                    }
                }
                colID++;
            }
            colID = 0;
            rowID++;
        }
    }
}
