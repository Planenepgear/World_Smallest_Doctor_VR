using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

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

    private Transform originParent;
    private void Update()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 3f, LayerMask.GetMask("Platform"));
        if(hit.collider)
            print(hit.collider);
        if (hit.collider && !originParent)
        {
            originParent = transform.parent;
            transform.SetParent(hit.collider.transform);
        }
        if (!hit.collider && originParent)
        {
            transform.SetParent(originParent);
            originParent = null;
        }
        
    }
}
