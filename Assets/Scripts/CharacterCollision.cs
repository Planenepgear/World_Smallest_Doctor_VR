using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CharacterCollision : MonoBehaviour
{
    public Transform scene;
    //public float pushPower = 1.0f;
    public float forceMagnitude = 1.0f;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Elevator"))
        {
            //Debug.Log("Elevator Start");
            hit.transform.SendMessage("CharacterLocker", this.gameObject);
        }
        //else
        //{
        //    transform.SetParent(scene);
        //}

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
}
