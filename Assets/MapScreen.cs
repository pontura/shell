using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapScreen : ScreenMain
{
    public Image bgImage;
    public float fadeDuration = 2;
    public float carSpeed = 2;
    public GameObject map;
    public GameObject car;
    public GameObject[] points;
    int id = 0;

    public override void Init()
    {
        base.Init();
        StartCoroutine(Transition());
    }
    IEnumerator Transition()
    {
        bgImage.fillOrigin = 0;
        car.transform.localPosition = points[id].transform.localPosition;
        id++;
        float i = 0;
        while(i<1)
        {
            i += Time.deltaTime / fadeDuration;
            bgImage.fillAmount = i;
            yield return new WaitForEndOfFrame();
        }
        map.SetActive(true);
        yield return new WaitForSeconds(1);
        Events.HideOldScreens();
        while (car.transform.localPosition.x < points[id].transform.localPosition.x)
        {
            Vector2 pos = car.transform.localPosition;
            pos.x += Time.deltaTime * carSpeed;
            car.transform.localPosition = pos;
            yield return new WaitForEndOfFrame();
        }
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
