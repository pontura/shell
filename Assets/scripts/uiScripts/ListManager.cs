using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListManager : MonoBehaviour
{
    [SerializeField] ButtonStandard button;
    [SerializeField] Transform container;
    List<ButtonStandard> buttons;
    System.Action<int> Onclicked;
    public bool isActive;
    public EventToListen eventToListen;
    public float delay_to_appear = 0;
    List<string> texts;
    bool allButtonsLoaded;

    public enum EventToListen
    {
        RELEASE,
        PRESS
    }

    public SelectionType selectionType;
    public enum SelectionType
    {
        SIMPLE,
        MULTIPLE_SELECT
    }

    public void Init<T>(List<T> arr, System.Action<int> Onclicked)
    {
        allButtonsLoaded = false;
        isActive = true;
        buttons = new List<ButtonStandard>();
        this.Onclicked = Onclicked;
        int id = 0;
        int total = arr.Count;
        StartCoroutine(AddCascade(id, total, delay_to_appear));
    }
    IEnumerator AddCascade(int id, int total, float delay)
    {
        for (int i = 0; i < total; i++)
        {

            AddButton(id);
            id++;
            yield return new WaitForSeconds(delay);
        }
        allButtonsLoaded = true;
    }
    void AddButton(int id)
    {
        Events.PlaySound("ui", "Sounds/tick", false);
        ButtonStandard newButton = Instantiate(button, container);
        newButton.Init(id, OnEvent, eventToListen);

        if(texts != null && texts.Count>=id-1)
            newButton.SetText(texts[id]);

        buttons.Add(newButton);
    }
    public void SetTexts(List<string> texts)
    {
        this.texts = texts;
    }
    void ResetButtons()
    {
        foreach (ButtonStandard b in buttons)
            b.SetActive(false);
    }
    void OnEvent(int id)
    {
        if (!allButtonsLoaded) return;
        if (!isActive) return;
        if (selectionType != SelectionType.MULTIPLE_SELECT)
            ResetButtons();
        if(!buttons[id].selected)
            buttons[id].SetActive(true);
        else
            buttons[id].SetActive(false);

        Events.PlaySound("ui", "Sounds/beep", false);

        Onclicked(id);
    }
    public List<ButtonStandard> GetAllButtons() { return buttons; }

    public void ResetAll()
    {
        Utils.RemoveAllChildsIn(container);
    }
    public void SetActive(bool isActive)
    {
        this.isActive = isActive;
    }
}
