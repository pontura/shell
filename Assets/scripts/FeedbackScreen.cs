using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackScreen : ScreenMain
{
    public Text field;
    public List<ProgressData.FeedbackData> wrong;
    public List<ProgressData.FeedbackData> innecesary;
    bool done;

    public override void Init()
    {
        done = false;
        wrong.Clear();
        innecesary.Clear();
        field.text = "";
        foreach(ProgressData.FeedbackData data in Data.Instance.progressData.GetLastFeedBack())
            CheckResult(data);

        ProcessFeedback();

        Events.PlaySound("ui", "Sounds/popup", false); 
    }
    void CheckResult(ProgressData.FeedbackData data)
    {
        if (data.result == ProgressData.Result.BIEN && !data.selected)
            wrong.Add(data);
        else if(data.result == ProgressData.Result.MAL && data.selected)
            wrong.Add(data);
        else if (data.result == ProgressData.Result.NEUTRO && data.selected)
            innecesary.Add(data);
    }
    void ProcessFeedback()
    {
        if (wrong.Count > 0)
        {
            field.text += "<b>Acordate:</b> \n";
            foreach (ProgressData.FeedbackData data in wrong)
                field.text += "- " + data.feedback + "\n\n";
        }

        if (innecesary.Count > 0)
        {
            field.text += "<b>Es innecesario:</b> \n";
            foreach (ProgressData.FeedbackData data in innecesary)
                field.text += "- " + data.accion + "\n";
        }
    }
    public void Next()
    {
        if (done) return;
        done = true;
        if(Data.Instance.contentData.id >= Data.Instance.contentData.content.Count-1)
            Events.GotoTo("Register");
       else
            Events.GotoTo("Map");
        GetComponent<Animation>().Play("off");
        Invoke("Reset", 0.5f);

        Events.PlaySound("ui", "Sounds/popupClose", false); 
    }
    private void Reset()
    {
        Hide();
    }
}
