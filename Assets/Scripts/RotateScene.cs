using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScene : MonoBehaviour
{
    public float X = 0f;
    public float Y = -10f;
    public float Z = 0f;
    public float speed = 35;

    void Update()
    {
        transform.Rotate(new Vector3(X, Y, Z) * Time.deltaTime * speed);
    }
}
