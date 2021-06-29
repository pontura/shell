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
    public ButtonStandard buttonOk;

    public override void Init()
    {
        buttonOk.Init(0, Next, ListManager.EventToListen.RELEASE);

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
            field.text += "<b>Recordá:</b>";
            foreach (ProgressData.FeedbackData data in wrong)
                field.text += "\n- " + data.feedback + "\n";
        }

        if (innecesary.Count > 0)
        {
            field.text += "\n<b>No hace falta:</b>";
            foreach (ProgressData.FeedbackData data in innecesary)
                field.text += "\n- " + data.accion;
        }
        if (field.text == "")
        {
            field.text = "\n\n\n<b>¡Perfecto!</b>";
            Data.Instance.progressData.score += 15;
        }
    }
    public void Next(int buttonID)
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
