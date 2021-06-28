using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapScreen : ScreenMain
{
    public float posOffset;
    public Image bgImage;
    public GameObject mapAsset;
    public GameObject mapAssetFront;
    public float fadeDuration = 2;
    public float carSpeed = 2;
    public Car car;
    public GameObject[] points;
    public MapSignal mapSignal;
    bool hasStarted;
    public ButtonStandard nextButton;

    private void Start()
    {
        nextButton.Init(0, OnDone, ListManager.EventToListen.RELEASE);
        nextButton.SetText("Resolver");
        nextButton.gameObject.SetActive(false);
        foreach (GameObject go in points)
            go.GetComponent<Image>().enabled = false;
    }
    void OnDone(int id)
    {
        StartCoroutine(Next());
    }
    public override void Init()
    {
        nextButton.gameObject.SetActive(false);
        base.Init();

        if (!hasStarted)
        {
            hasStarted = true;
            StartCoroutine(Transition(true));
        }
        else
        {           
            mapSignal.gameObject.SetActive(false);
            StartCoroutine(Transition(false));
            Data.Instance.contentData.Next();
        }
    }
    IEnumerator Transition(bool isFirst)
    {
        car.SetState(false);
        bgImage.fillOrigin = 0;

        float from = (points[Data.Instance.contentData.id].transform.localPosition.x* -1) - posOffset;
        float posTo = (points[Data.Instance.contentData.id + 1].transform.localPosition.x * -1) - posOffset;
        
        Vector2 pos = new Vector2(from, mapAsset.transform.localPosition.y);
        mapAsset.transform.localPosition = pos;
        mapAssetFront.transform.localPosition = pos;

        float i = 0;

        Events.PlaySound("ui2", "Sounds/fill", false);

        while (i < 1)
        {
            i += Time.deltaTime / fadeDuration;
            bgImage.fillAmount = i;
            yield return new WaitForEndOfFrame();
        }
        if (!isFirst)
        {       
            yield return new WaitForSeconds(1);

            Events.PlaySound("ui", "Sounds/car", false);
            car.SetState(true);
            Events.HideOldScreens();

            while (mapAsset.transform.localPosition.x > posTo)
            {
                pos.x -= Time.deltaTime * carSpeed;
                mapAsset.transform.localPosition = pos;
                mapAssetFront.transform.localPosition = pos;
                yield return new WaitForEndOfFrame();
            }
        }        
        string text = Data.Instance.contentData.content[Data.Instance.contentData.id].situacion;
        mapSignal.Init((Data.Instance.contentData.id + 1) + "/" + points.Length, text, car.transform.position);
        Events.PlaySound("ui", "Sounds/alert", false);
        car.SetState(false);
        nextButton.gameObject.SetActive(true);      
    }
   IEnumerator Next()
    {
        nextButton.gameObject.SetActive(false);
        Events.GotoTo("GameScreen");
        bgImage.fillOrigin = 1;
        float i = 1;
        Events.PlaySound("ui2", "Sounds/fill", false);
        while (i > 0)
        {
            i -= Time.deltaTime / fadeDuration;
            bgImage.fillAmount = i;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(1);
        Events.HideOldScreens();
    }
}
