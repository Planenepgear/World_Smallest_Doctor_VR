using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeesawReset : MonoBehaviour
{
    public GameObject seesawPrefab;
    public Transform seesawPlatform;
    private Vector3 pos;
    private Vector3 platformLocalPos = new(0.82f,0.97f,3.024f);

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        seesawPlatform.localPosition = platformLocalPos;
        seesawPlatform.localRotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        if(pos != transform.position)
        {
            Instantiate(seesawPrefab, transform.position, transform.rotation, transform.parent);
            Destroy(gameObject);

            //seesawPlatform.localRotation = Quaternion.identity;
            pos = transform.position;
        }
    }
}
