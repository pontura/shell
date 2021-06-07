using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProgressData : MonoBehaviour
{
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
    }
    public List<FeedbackData> GetLastFeedBack()
    {
        return activeSituacion.feedbackData;
    }

}
