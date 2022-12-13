using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Debug = UnityEngine.Debug;

public class CharacterCollision : MonoBehaviour
{
    //public Transform scene;
    //public Transform currentStage;

    public GameObject characterGeometry;
    private SkinnedMeshRenderer characterSkinnedMeshRenderer;

    //public float pushPower = 1.0f;
    public float forceMagnitude = 1.0f;

    private Transform platformOriginParent;
    private bool platformFrameCheck = false;

    private float originMoveSpeed;
    public ActionBasedContinuousMoveProvider moveProvider;

    void Start()
    {
        originMoveSpeed = moveProvider.moveSpeed;
        characterSkinnedMeshRenderer = characterGeometry.GetComponent<SkinnedMeshRenderer>();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Elevator"))
        {
            hit.transform.SendMessage("CharacterLocker", this.gameObject);
        }


        if (hit.gameObject.CompareTag("Seesaw"))
        {
            Debug.Log("Seesaw");
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
        Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 3f, LayerMask.GetMask("Platform", "Elevator"));
        if(hit.collider)
            print(hit.collider);
        if (hit.collider && !originParent)
        {
            platformFrameCheck = true;
            originParent = transform.parent;

            //if(characterSkinnedMeshRenderer.enabled == false)
            //{
            //    moveProvider.moveSpeed = originMoveSpeed * (hit.collider.transform.localScale.x) * 0.6f;
            //}
            //else
            //{
            //    moveProvider.moveSpeed = originMoveSpeed * (hit.collider.transform.localScale.x);
            //}

            transform.SetParent(hit.collider.transform);
        }
        if (!hit.collider && originParent)
        {
            platformFrameCheck = false;

            //if (characterSkinnedMeshRenderer.enabled == false)
            //{
            //    moveProvider.moveSpeed = originMoveSpeed * 0.6f;
            //}
            //else
            //{
            //    moveProvider.moveSpeed = originMoveSpeed;
            //}

            transform.SetParent(originParent);
            originParent = null;
        }

        if (!platformFrameCheck)
        {
            if (characterSkinnedMeshRenderer.enabled == false)
            {
                moveProvider.moveSpeed = originMoveSpeed * 0.6f;
            }
            else if (characterSkinnedMeshRenderer.enabled == true)
            {
                moveProvider.moveSpeed = originMoveSpeed;
            }
        }
        else if (platformFrameCheck)
        {
            if (characterSkinnedMeshRenderer.enabled == false)
            {
                moveProvider.moveSpeed = originMoveSpeed * (hit.collider.transform.localScale.x) * 0.6f;
            }
            else if (characterSkinnedMeshRenderer.enabled == true)
            {
                moveProvider.moveSpeed = originMoveSpeed * (hit.collider.transform.localScale.x);
            }
        }
    }
}
