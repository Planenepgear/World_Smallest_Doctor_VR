using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PosLockPlayer : MonoBehaviour
{
    public Transform center;
    public Transform playerCamera;
    public float maxDistance = 10f;
    [SerializeField] private float distance;

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
        distance = (new Vector3(playerCamera.position.x, 0,playerCamera.position.z) - new Vector3(center.position.x, 0, center.position.z)).magnitude;

        if (distance > maxDistance)
        {
            myVolume.weight = Mathf.Clamp(weightChangeSpeed * (distance - maxDistance), 0, 1);
        }
        else
        {
            myVolume.weight = 0;
        }

        //if (characterCamera.activeInHierarchy == false)
        //    characterModel.transform.localPosition = ch.center + new Vector3(0, -0.8f, 0);
    }
}
