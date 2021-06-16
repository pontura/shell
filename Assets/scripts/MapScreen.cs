using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapScreen : ScreenMain
{
    public float posOffset;
    public Image bgImage;
    public GameObject mapAsset;
    public float fadeDuration = 2;
    public float carSpeed = 2;
    public GameObject map;
    public Car car;
    public GameObject[] points;
    int id = 0;
    public MapSignal mapSignal;
    bool hasStarted;

    private void Start()
    {
        
        foreach (GameObject go in points)
            go.GetComponent<Image>().enabled = false;
    }
    public override void Init()
    {
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
        }
    }
    IEnumerator Transition(bool isFirst)
    {
        car.SetState(false);
        bgImage.fillOrigin = 0;

        float from = (points[id].transform.localPosition.x* -1) - posOffset;
        float posTo = (points[id+1].transform.localPosition.x * -1) - posOffset;
        
        Vector2 pos = new Vector2(from, mapAsset.transform.localPosition.y);
        mapAsset.transform.localPosition = pos;

        map.SetActive(true);

        float i = 0;
        while (i < 1)
        {
            i += Time.deltaTime / fadeDuration;
            bgImage.fillAmount = i;
            yield return new WaitForEndOfFrame();
        }

        if (!isFirst)
        {
            id++;            
            yield return new WaitForSeconds(1);
            car.SetState(true);
            Events.HideOldScreens();

            while (mapAsset.transform.localPosition.x > posTo)
            {
                pos.x -= Time.deltaTime * carSpeed;
                mapAsset.transform.localPosition = pos;
                yield return new WaitForEndOfFrame();
            }
        }

        mapSignal.Init((id+1) + "/" + points.Length, car.transform.position);
        car.SetState(false);
        yield return new WaitForSeconds(1.7f);
        Events.GotoTo("GameScreen");
        bgImage.fillOrigin = 1;
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
