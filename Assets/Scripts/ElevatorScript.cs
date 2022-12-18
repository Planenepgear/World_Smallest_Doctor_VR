using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    public Transform midPoint;
    public Transform endPoint;
    public Transform playerObject;
    //public Transform elevatorCenter;
    public GameObject characterObject;
    public Transform playerCamera;
    //public Transform characterModle;

    public GameObject lastElevator;

    public float moveSpeed = 3f;
    public float moveSmoothTime = 3f;
    public bool isLocked = false;
    public bool isFinished = false;

    private Vector3 moveVelocity = Vector3.zero;
    private float distanceMid;
    private float distanceEnd;
    private bool isMidPoint = true;
    private GameObject OriginParent;

    private AudioSource audioSource;
    private void Awake()
    {
        OriginParent = gameObject.transform.parent.gameObject;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //characterController = Character.GetComponent<CharacterController>();
        isLocked = false;
    }

    void Update()
    {
        if (isLocked)
        {
            Invoke(nameof(ElevatorMove), 1.0f);

            if (characterObject)
            {
                PosLock(characterObject);
            }
        }
    }

    private void ElevatorMove()
    {
        audioSource.Play();
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
                playerObject.position = Vector3.MoveTowards(playerObject.position, playerObject.position + new Vector3(0, dis, 0), 5f * Time.deltaTime);
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
                    playerObject.position = Vector3.MoveTowards(playerObject.position, playerObject.position + new Vector3(0, dis, 0), 5f * Time.deltaTime);
                }
                else if (distanceEnd < 1.0f)
                {
                    isLocked = false;
                    isFinished = true;
                    audioSource.Stop();
                    if (characterObject)
                    {
                        //this.transform.SetParent(endPoint.parent);
                        //characterObject.transform.SetParent(endPoint.parent.parent);

                        characterObject = null;
                        OriginParent.SetActive(false);

                        if (lastElevator)
                        {
                            lastElevator.SetActive(false);
                        }
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
            playerObject.position = Vector3.MoveTowards(playerObject.position, playerObject.position + new Vector3(0, dis, 0), 5f * Time.deltaTime);
        }
    }

    public void CharacterLocker(GameObject character)
    {
        if (!isFinished && character)
        {
            characterObject = character;
            isLocked = true;
            //character.transform.SetParent(this.transform);
            //character.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
            //PosLock(character);
        }
        else
        {
            isLocked = false;
            //this.transform.SetParent(endPoint.parent);
            //character.transform.SetParent(endPoint.parent.parent);
            //characterObject = null;
            OriginParent.SetActive(false);
        }
    }

    private void PosLock(GameObject character)
    {
        if (isLocked)
        {
            character.transform.position = new Vector3(Mathf.Clamp(character.transform.position.x, transform.position.x - 0.9f, transform.position.x + 0.9f),
                Mathf.Clamp(character.transform.position.y, transform.position.y - 0.1f, transform.position.y + 0.9f), Mathf.Clamp(character.transform.position.z, transform.position.z - 0.9f, transform.position.z + 0.9f));
        }
    }
}
