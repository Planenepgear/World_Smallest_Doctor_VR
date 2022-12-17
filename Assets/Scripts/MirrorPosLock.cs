using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorPosLock : MonoBehaviour
{
    private Vector3 trapPosition;

    void Start()
    {
        trapPosition = transform.localPosition;
    }

    void Update()
    {
        if (trapPosition != transform.localPosition)
        {
            transform.localPosition = trapPosition;
            //trapPosition = transform.position;
        }
    }
}
