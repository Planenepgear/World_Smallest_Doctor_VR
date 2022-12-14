using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XROriginMainStart : MonoBehaviour
{
    public Transform startPoint;

    void Start()
    {
        transform.position = startPoint.position;
        transform.rotation = startPoint.rotation;
    }
}
