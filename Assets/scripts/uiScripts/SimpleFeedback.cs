using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFeedback : MonoBehaviour
{
    public GameObject ok;
    public GameObject wrong;
    public GameObject neutro;

    public enum states
    {
        OK, 
        WRONG,
        NEUTRO
    }
    private void Start()
    {
        SetOff();
    }
    public void SetState(states STATE, int duration)
    {
        print("SetState" + STATE);
        CancelInvoke();
        SetOff();
        switch (STATE)
        {
            case states.OK:
                ok.gameObject.SetActive(true);
                Events.PlaySound("ui", "Sounds/feedback_ok", false);
                break;
            case states.WRONG:
                wrong.gameObject.SetActive(true);
                Events.PlaySound("ui", "Sounds/feedback_wrong", false);
                break;
            case states.NEUTRO:
                neutro.gameObject.SetActive(true);
                Events.PlaySound("ui", "Sounds/feedback_neutro", false);
                break;
        }
        Invoke("SetOff", duration);
    }
    public void SetOff()
    {
        ok.gameObject.SetActive(false);
        wrong.gameObject.SetActive(false);
        neutro.gameObject.SetActive(false);
    }

}
