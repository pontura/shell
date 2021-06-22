using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : ScreenMain
{
    ContentData.Content content;
    public Text  titleField;
    public ListManager listManager;
    public Button NextButton;
    bool done;
    public bool tutorialDone;
    public GameObject tutorial;
    public ButtonStandard tutorialButton;

    private void Start()
    {
        tutorial.SetActive(false);
        tutorialButton.Init(0, OnTutorialClicked, ListManager.EventToListen.RELEASE);
    }
    void OnTutorialClicked(int buttonID)
    {
        tutorial.SetActive(false);
    }
    public override void Init() {
        tutorial.SetActive(false);
        SetNextButton(false);
        listManager.ResetAll();
        done = false;
        content = Data.Instance.contentData.content[Data.Instance.contentData.id];
        titleField.text = content.situacion;

        List<string> texts = new List<string>();
        foreach (ContentData.SituacionData d in content.situaciones)
            texts.Add(d.accion);
        listManager.SetTexts(texts);

        Invoke("DelayedConstructor", 2);

    }
    void DelayedConstructor()
    {
        if(!tutorialDone)
        {
            tutorial.SetActive(true);
            tutorialDone = true;
        }
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
        done = true;
        StartCoroutine(Feedback());
        SetNextButton(false);
    }
    IEnumerator Feedback()
    {
        Data.Instance.progressData.AddNewSituationResult();
        int _id = 0;
        foreach (ActionButton b in listManager.GetAllButtons())
        {
            b.Feedback();
            ContentData.SituacionData situacion = content.situaciones[_id];
            Data.Instance.progressData.AddFeedBack(situacion, b.selected);
            _id++;

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

            yield return new WaitForSeconds(0.25f);
        }
        Events.GotoTo("FeedbackScreen");
    }
    void SetNextButton(bool interactable)
    {
        NextButton.interactable = interactable;
    }
}
