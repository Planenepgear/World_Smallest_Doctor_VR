using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocatorTranform : MonoBehaviour
{
    public Transform trackCamera;
    public Transform scene;
    //public Transform ChapterXrOrigin;

    void Update()
    {
        Vector3 posC = trackCamera.position;
        Vector3 posS = scene.position;
        transform.position = new(posC.x, transform.position.y, posC.z);
        transform.LookAt(new Vector3(posS.x, transform.position.y, posS.z));
        //ChapterXrOrigin.LookAt(new Vector3(transform.position.x, ChapterXrOrigin.position.y, transform.position.z));
    }
}
