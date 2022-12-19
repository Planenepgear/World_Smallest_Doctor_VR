using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PosLockPlayer : MonoBehaviour
{
    public Transform center;
    public Transform ceiling;
    public Transform playerCamera;
    public float maxDistance = 10f;
    [SerializeField] private float distance;
    [SerializeField] private float verticalDistance;

    public GameObject globalVolume;
    public float weightChangeSpeed = 0.5f;
    private Volume myVolume;

    void Start()
    {
        distance = 0;

        myVolume = globalVolume.GetComponent<Volume>();
        myVolume.weight = 0;
    }

    void Update()
    {
        distance = (new Vector3(playerCamera.position.x, 0, playerCamera.position.z) - new Vector3(center.position.x, 0, center.position.z)).magnitude;
        verticalDistance = ceiling.position.y - playerCamera.position.y;

        if (distance > maxDistance)
        {
            myVolume.weight = Mathf.Clamp(weightChangeSpeed * (distance - maxDistance), 0, 1);
        }
        else if (verticalDistance < 0.5f)
        {
            myVolume.weight = Mathf.Clamp(weightChangeSpeed * -verticalDistance * 2, 0.3f, 1);
        }
        else
        {
            myVolume.weight = 0;
        }
    }
}
