using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollision : MonoBehaviour
{
    public Transform scene;

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
    }
}
