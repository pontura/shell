using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : ScreenMain
{
    public int id;
    ContentData.Content content;
    public Text  titleField;
    public ListManager listManager;
    public Button NextButton;
    bool done;

    public override void Init() {
        SetNextButton(false);
        listManager.ResetAll();
        done = false;
        content = Data.Instance.contentData.content[id];
        titleField.text = content.situacion;

        List<string> texts = new List<string>();
        foreach (ContentData.SituacionData d in content.situaciones)
            texts.Add(d.accion);
        listManager.SetTexts(texts);

        listManager.Init(content.situaciones, OnClicked);
       
    }
    void OnClicked(int id)
    {
        if (done) return;
        foreach (ButtonStandard b in listManager.GetAllButtons())
            if (b.selected)
            {
                SetNextButton(true);
                return;
            }
        SetNextButton(false);
    }
    public void Ready()
    {
        if (done) return;
        listManager.SetActive(false);
        id++;
        done = true;
        StartCoroutine(Feedback());
        SetNextButton(false);
    }
    IEnumerator Feedback()
    {
        Data.Instance.progressData.AddNewSituationResult();
        int _id = 0;
        foreach (ButtonStandard b in listManager.GetAllButtons())
        {
            ContentData.SituacionData situacion = content.situaciones[_id];
            Data.Instance.progressData.AddFeedBack(situacion, b.selected);
            _id++;
            yield return new WaitForSeconds(0.5f);

            if (b.selected)
            {
                if (situacion.value == ProgressData.Result.BIEN)
                    b.GetComponentInChildren<SimpleFeedback>().SetState(SimpleFeedback.states.OK, 1000);
                else if (situacion.value == ProgressData.Result.MAL)
                    b.GetComponentInChildren<SimpleFeedback>().SetState(SimpleFeedback.states.WRONG, 1000);
                else if (situacion.value == ProgressData.Result.NEUTRO)
                    b.GetComponentInChildren<SimpleFeedback>().SetState(SimpleFeedback.states.NEUTRO, 1000);
            }
            else
            {
                if (situacion.value == ProgressData.Result.BIEN)
                    b.GetComponentInChildren<SimpleFeedback>().SetState(SimpleFeedback.states.WRONG, 1000);
            }
        }
        Events.GotoTo("FeedbackScreen");
    }
    void SetNextButton(bool interactable)
    {
        NextButton.interactable = interactable;
    }
}
