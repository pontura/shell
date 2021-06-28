using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProgressData : MonoBehaviour
{
    public int score;

    public enum Result
    {
        BIEN,
        NEUTRO,
        MAL
    }
    public List<Situacions> all;
    [Serializable]
    public class Situacions
    {
        public List<FeedbackData> feedbackData;
    }
    public Situacions activeSituacion;

    [Serializable]
    public class FeedbackData
    {
        public Result result;
        public bool selected;
        public string accion;
        public string feedback;
    }
    void ProcessFeedback(FeedbackData fd)
    {
        switch(fd.result)
        {
            case Result.BIEN:
                if (fd.selected)
                    score += 10;
                else
                    score -= 10;
                break;
            case Result.MAL:
                if (fd.selected)
                    score -= 10;
                else
                    score += 10;
                break;
            case Result.NEUTRO:
                if (fd.selected)
                    score -= 5;
                else
                    score += 2;
                break;
        }
        if (score < 0)  score = 0;
    }
    public void AddNewSituationResult()
    {
        activeSituacion = new Situacions();
        activeSituacion.feedbackData = new List<FeedbackData>();
        all.Add(activeSituacion);
    }
    public void AddFeedBack(ContentData.SituacionData sd, bool selected)
    {
        FeedbackData fd = new FeedbackData();
        fd.result = sd.value;
        fd.selected = selected;
        fd.feedback = sd.feedback;
        fd.accion = sd.accion;
        activeSituacion.feedbackData.Add(fd);
        ProcessFeedback(fd);
    }
    public List<FeedbackData> GetLastFeedBack()
    {
        return activeSituacion.feedbackData;
    }

}
