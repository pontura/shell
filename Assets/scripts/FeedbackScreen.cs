using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackScreen : ScreenMain
{
    public Text field;
    public List<ProgressData.FeedbackData> wrong;
    public List<ProgressData.FeedbackData> innecesary;
    
    public override void Init()
    {
        wrong.Clear();
        innecesary.Clear();
        field.text = "";
        foreach(ProgressData.FeedbackData data in Data.Instance.progressData.GetLastFeedBack())
            CheckResult(data);

        ProcessFeedback();
    }
    void CheckResult(ProgressData.FeedbackData data)
    {
        if (data.result == ProgressData.Result.BIEN && !data.selected)
            wrong.Add(data);
        else if(data.result == ProgressData.Result.MAL && data.selected)
            wrong.Add(data);
        else if (data.result == ProgressData.Result.NEUTRO)
            innecesary.Add(data);
    }
    void ProcessFeedback()
    {
        if (wrong.Count > 0)
        {
            field.text += "<b>Es importante:</b> \n";
            foreach (ProgressData.FeedbackData data in wrong)
                field.text += "- " + data.feedback + "\n\n";
        }

        if (innecesary.Count > 0)
        {
            field.text += "<b>Y es innecesario:</b> \n";
            foreach (ProgressData.FeedbackData data in innecesary)
                field.text += "- " + data.accion + "\n";
        }
    }
    public void Next()
    {
        Events.GotoTo("GameScreen");
    }
}
