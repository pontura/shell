using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonStandard : MonoBehaviour
{
    public Text field;
    public Image background;
    public Color selectedColor;
    public Color idleColor;
    public bool selected;

    public void Init(int id, System.Action<int> OnClicked, ListManager.EventToListen eventToListen)
    {
        SetText( id.ToString( ));
        switch (eventToListen)
        {
            case ListManager.EventToListen.RELEASE:
                GetComponent<Button>().onClick.AddListener(() => OnClicked(id));
                break;
            case ListManager.EventToListen.PRESS:
                EventTrigger triggerDown = GetComponent<Button>().gameObject.AddComponent<EventTrigger>();
                var pointerDown = new EventTrigger.Entry();
                pointerDown.eventID = EventTriggerType.PointerDown;
                pointerDown.callback.AddListener((e) => OnClicked(id));
                triggerDown.triggers.Add(pointerDown);
                break;
        }
        OnInit();
    }
    public virtual void OnInit() { }
    public void SetText(string text)
    {
        field.text = text;
    }
    public void SetActive(bool isActive)
    {
        if (isActive)
            background.color = selectedColor;
        else
            background.color = idleColor;
        selected = isActive;
    }
}
