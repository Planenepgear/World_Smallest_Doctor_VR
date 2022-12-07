using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosLockScene : MonoBehaviour
{
    [SerializeField] float minX = -10.0f;
    [SerializeField] float maxX = 10.0f;

    [SerializeField] float minY = -10.0f;
    [SerializeField] float maxY = 10.0f;

    [SerializeField] float minZ = -10.0f;
    [SerializeField] float maxZ = 10.0f;

    private float x;
    private float y;
    private float z;

    private void Start()
    {
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;
    }

    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, x + minX, x + maxX),
            Mathf.Clamp(transform.position.y, y + minY, y + maxY), Mathf.Clamp(transform.position.z, z + minZ, z + maxZ));
    }
}
