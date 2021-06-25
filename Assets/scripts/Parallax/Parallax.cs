using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    ParallaxItem[] all;
    public Camera cam;
    public float speed;

    public states state;
    public enum states
    {
        FORWARD,
        STOPPING
    }

    private void Start()
    {
        all = GetComponentsInChildren<ParallaxItem>();
    }
    void Update()
    {
        if (state == states.FORWARD)
        {
            if (speed < 0.7f)
                speed += Time.deltaTime / 2;
            else
                speed = 0.7f;
        }
        else
        {
            speed -= Time.deltaTime / 2; ;
            if (speed < 0) speed = 0;
        }

        foreach (ParallaxItem item in all)
            ProcessParallax(item, item.speed);
    }
    public void StopInFade()
    {
        state = states.STOPPING;
    }
    void ProcessParallax(ParallaxItem item, float _speed)
    {
        Vector3 pos = item.transform.localPosition;

        if (item.transform.localPosition.x < -item.parallaxWidth)
            pos.x = 0;
        else
            pos.x -= speed * _speed * Time.deltaTime;

        item.transform.localPosition = pos;
    }
    public void Move()
    {
        speed = 0;
        state = states.FORWARD;
    }
}
