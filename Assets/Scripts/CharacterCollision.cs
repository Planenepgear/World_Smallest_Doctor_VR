using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CharacterCollision : MonoBehaviour
{
    public Transform scene;
    //public float pushPower = 1.0f;
    public float forceMagnitude = 1.0f;

    private Transform platformOriginParent;
    private bool platformFrameCheck = false;
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Elevator"))
        {
            hit.transform.SendMessage("CharacterLocker", this.gameObject);
        }

        if (hit.gameObject.CompareTag("Platform")&& !platformFrameCheck)
        {
            if (hit.transform != transform.parent)
            {
                platformOriginParent = transform.parent;
                transform.SetParent(hit.transform);
             }
            platformFrameCheck = true;
        }

        
      
        if (hit.gameObject.CompareTag("Seesaw"))
        {
            Rigidbody rb = hit.collider.attachedRigidbody;

            if (rb == null || rb.isKinematic)
            {
                return;
            }

            //if (hit.moveDirection.y > -0.3f)
            //{
            //    return;
            //}

            rb.AddForceAtPosition(Vector3.down * forceMagnitude, transform.position, ForceMode.Impulse);
        }
    }
    float timer = 0;
    float checkTime = 0.2f;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer <= checkTime)
            return;
        timer = 0f;
        if (!platformFrameCheck && platformOriginParent != null) {
            transform.SetParent(platformOriginParent);
            platformOriginParent = null;
        }
        platformFrameCheck = false;
    }

}
