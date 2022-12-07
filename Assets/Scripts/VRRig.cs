using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VRMap
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffsect;
    public Vector3 trackingRotationOffset;
    public void Map()
    {
        rigTarget.position = vrTarget.TransformPoint(trackingPositionOffsect);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}

public class VRRig : MonoBehaviour
{
    public float turnSmoothness = 5;

    public VRMap head;
    public VRMap leftHand;
    public VRMap rightHand;

    public Transform headConstraint;
    public Vector3 headBodyOffest;

    void Start()
    {
        headBodyOffest = transform.position - headConstraint.position;
    }

    void FixedUpdate()
    {
        transform.position = headConstraint.position + headBodyOffest;
        transform.forward = Vector3.Lerp(transform.forward, 
            Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized, Time.deltaTime * turnSmoothness);

        head.Map();
        leftHand.Map();
        rightHand.Map();
    }
}
