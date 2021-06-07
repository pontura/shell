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
        isActive = true;
        buttons = new List<ButtonStandard>();
        this.Onclicked = Onclicked;
        int id = 0;
        int n = arr.Count;
        for (int i = 0; i < n; i++)
        {
            AddButton(id);
            id++;
        }
    }
    void AddButton(int id)
    {
        ButtonStandard newButton = Instantiate(button, container);
        newButton.Init(id, OnEvent, eventToListen);
        buttons.Add(newButton);
    }
    public void SetTexts(List<string> texts)
    {
        int id = 0;
        foreach (ButtonStandard b in buttons)
        {
            if (id >= texts.Count) return;
            string t = texts[id];
            b.SetText(texts[id]);
            id++;
        }
    }
    void ResetButtons()
    {
        foreach (ButtonStandard b in buttons)
            b.SetActive(false);
    }
    void OnEvent(int id)
    {
        if (!isActive) return;
        if (selectionType != SelectionType.MULTIPLE_SELECT)
            ResetButtons();
        if(!buttons[id].selected)
            buttons[id].SetActive(true);
        else
            buttons[id].SetActive(false);

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
