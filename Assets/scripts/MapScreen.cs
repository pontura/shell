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
    public Car car;
    public GameObject[] points;
    int id = 0;
    public Vector2 carOffset;
    public MapSignal mapSignal;

    private void Start()
    {
        
        foreach (GameObject go in points)
            go.GetComponent<Image>().enabled = false;
    }
    public override void Init()
    {
        mapSignal.gameObject.SetActive(false);
        base.Init();
        StartCoroutine(Transition());
    }
    IEnumerator Transition()
    {
        car.SetState(false);
        bgImage.fillOrigin = 0;

        Vector2 pos = points[id].transform.position; ;
        pos.y += carOffset.y;
        car.transform.position = pos;
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
        car.SetState(true);
        Events.HideOldScreens();
        while (pos.x + carOffset.x < points[id].transform.position.x)
        {
            pos.x += Time.deltaTime * carSpeed;
            car.transform.position = pos;
            yield return new WaitForEndOfFrame();
        }
        mapSignal.Init(id + "/" + points.Length, car.transform.position);        
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
