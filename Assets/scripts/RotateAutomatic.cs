using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAutomatic : MonoBehaviour
{
    public float speed = 100;
    void Update()
    {
        Vector3 r = transform.localEulerAngles;
        r.z += speed * Time.deltaTime;
        transform.localEulerAngles = r;
    }
}
