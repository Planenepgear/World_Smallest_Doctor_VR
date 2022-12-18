using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRestore : MonoBehaviour
{
    private FloorManager floorManager;


    private void Awake()
    {
        floorManager = GetComponentInParent<FloorManager>();
        Debug.Log(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject target = other.gameObject;

        if (!target.CompareTag("Shell"))
        {

            if (target.transform.parent == floorManager.transform)
            {
                LevelObjectInfo origin = floorManager.FindObjectWithID(target.GetInstanceID());

                if (origin != null)
                {
                    origin.rigidbody.velocity = Vector3.zero;
                    origin.rigidbody.angularVelocity = Vector3.zero;
                    target.transform.localPosition = origin.originPosition;
                    target.transform.localRotation = origin.originRotation;
                }
            }
        }
    }

}
