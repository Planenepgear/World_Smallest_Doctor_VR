using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    public Transform midPoint;
    public Transform endPoint;
    public Transform scene;
    //public Transform elevatorCenter;
    public GameObject characterObject;
    public Transform playerCamera;
    //public Transform characterModle;
    public float moveSpeed = 3f;
    public float moveSmoothTime = 3f;
    public bool isLocked = false;
    public bool isFinished = false;

    private Vector3 moveVelocity = Vector3.zero;
    private float distanceMid;
    private float distanceEnd;
    private bool isMidPoint = true;
    private GameObject OriginParent;

    private void Awake()
    {
        OriginParent = gameObject.transform.parent.gameObject;
    }

    void Start()
    {
        //characterController = Character.GetComponent<CharacterController>();
        isLocked = false;
    }

    void Update()
    {
        if (isLocked)
        {
            Invoke(nameof(ElevatorMove), 1.0f);
        }
    }

    private void ElevatorMove()
    {
        endPoint.parent.gameObject.SetActive(true);
        if (midPoint)
        {
            distanceMid = (transform.position - midPoint.position).sqrMagnitude;
            var dis = transform.position.y - playerCamera.position.y + 5f;
            if (distanceMid > 1 && isMidPoint)
            {
                transform.position = Vector3.MoveTowards(transform.position, midPoint.position, moveSpeed * Time.deltaTime);
                //transform.position = Vector3.SmoothDamp(transform.position, midPoint.position, ref moveVelocity, moveSmoothTime);
                
                //scene.position -= new Vector3(0, dis, 0);
                scene.position = Vector3.MoveTowards(scene.position, scene.position - new Vector3(0, dis, 0), 5f * Time.deltaTime);
            }
            else
            {
                distanceEnd = (transform.position - endPoint.position).sqrMagnitude;
                if (distanceEnd > 0.1f)
                {
                    isMidPoint = false;
                    transform.position = Vector3.MoveTowards(transform.position, endPoint.position, moveSpeed * Time.deltaTime);
                    //transform.position = Vector3.SmoothDamp(transform.position, endPoint.position, ref moveVelocity, moveSmoothTime);
                    
                    //scene.position -= new Vector3(0, dis, 0);
                    scene.position = Vector3.MoveTowards(scene.position, scene.position - new Vector3(0, dis, 0), 5f * Time.deltaTime);
                }
                else if (distanceEnd < 1.0f)
                {
                    isLocked = false;
                    isFinished = true;

                    if (characterObject)
                    {
                        this.transform.SetParent(endPoint.parent);
                        characterObject.transform.SetParent(endPoint.parent.parent);
                        characterObject = null;
                        OriginParent.SetActive(false);
                    }
                }

                //if (distanceEnd < 5f && isFinished == false)
                //{
                //    isFinished = true;
                //    characterObject.transform.SetParent(endPoint.parent.parent);
                //    characterObject = null;
                //}
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint.position, moveSpeed * Time.deltaTime);
            //transform.position = Vector3.SmoothDamp(transform.position, endPoint.position, ref moveVelocity, moveSmoothTime);

            var dis = transform.position.y - playerCamera.position.y + 5f;
            scene.position = Vector3.MoveTowards(scene.position, scene.position - new Vector3(0, dis, 0), 5f * Time.deltaTime);
        }
    }

    public void CharacterLocker(GameObject character)
    {
        if (!isFinished && character)
        {
            characterObject = character;
            isLocked = true;
            character.transform.SetParent(this.transform);
            //character.transform.position = new Vector3(elevatorCenter.position.x, character.transform.localScale.y, elevatorCenter.position.z);
        }
        else
        {
            isLocked = false;
            this.transform.SetParent(endPoint.parent);
            character.transform.SetParent(endPoint.parent.parent);
            //characterObject = null;
            OriginParent.SetActive(false);
        }
    }
}
