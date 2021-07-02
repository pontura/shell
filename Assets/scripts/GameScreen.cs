using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : ScreenMain
{
    ContentData.Content content;
    public Text  titleField;
    public ListManager listManager;
    public ButtonStandard NextButton;
    bool done;
    public bool tutorialDone;
    public GameObject tutorial;
    public ButtonStandard tutorialButton;

    public Image carImage;
    public Sprite[] carSprites;

    public Image situacionImage;
    public Sprite[] situacionesSprites;
    public ProgressBar progressBar;

    private void Start()
    {
        tutorial.SetActive(false);
        tutorialButton.Init(0, OnTutorialClicked, ListManager.EventToListen.RELEASE);
        NextButton.Init(0, Ready, ListManager.EventToListen.RELEASE);
        NextButton.SetText("Listo!");
    }
    void OnTutorialClicked(int buttonID)
    {
        tutorial.SetActive(false);
    }
    public override void Init() {
        progressBar.Refresh();
        int contentID = Data.Instance.contentData.id;
        situacionImage.sprite = situacionesSprites[contentID];
        carImage.sprite = carSprites[contentID];

        tutorial.SetActive(false);
        SetNextButton(false);
        
        listManager.ResetAll();
        done = false;
        content = Data.Instance.contentData.content[contentID];
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
        NextButton.SetActive(true);
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
        NextButton.SetActive(false);
    }
    public void Ready(int id)
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
                else
                    Events.PlaySound("ui", "Sounds/feedback_neutro", false);
            }

            yield return new WaitForSeconds(0.35f);
        }
        Events.GotoTo("FeedbackScreen");
    }
    void SetNextButton(bool interactable)
    {
        NextButton.SetInteractable( interactable );
    }
}
