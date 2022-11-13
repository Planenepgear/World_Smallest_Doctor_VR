using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FlyCameraLookat : MonoBehaviour
{
    public Transform flyCamera;
    public Transform playerCamera; //3
    public Transform chapterCamera; //1
    public Transform locator;
    public GameObject chapterModle;
    public GameObject chapterLeftHand;
    public GameObject chapterRightHand;

    public GameObject globalVolume;
    public float weightChangeSpeed = 0.25f;
    private Volume myVolume;
    // starting value for the Lerp
    static float t = 0.0f;
    private bool isWeekUp = true;

    public float minDistance = 0.3f;

    //public float flySpeed = 1.0f;
    public float flySmoothTime = 2.5F;
    public float Yoffect = 0f;
    private Vector3 flyVelocity = Vector3.zero;
    private Vector3 centerPosWorld;
    private Vector3 centerPosLocal;

    private float moveSpeedSaver;

    public InputActionProperty LExchange;
    public InputActionProperty RExchange;
    public InputActionProperty XButton;
    public InputActionProperty YButton;

    private bool isExchanging0 = false;
    private bool isExchanging1 = false;
    private bool isExchanging2 = false;

    private void Start()
    {
        chapterCamera.parent.parent.transform.rotation = playerCamera.parent.parent.transform.rotation;

        flyCamera.position = playerCamera.position;
        playerCamera.parent.gameObject.SetActive(true);
        chapterCamera.gameObject.SetActive(false);

        flyCamera.gameObject.SetActive(false);
        chapterCamera.parent.parent.GetComponent<ActionBasedSnapTurnProvider>().enabled = false;

        chapterLeftHand.SetActive(false);
        chapterRightHand.SetActive(false);

        myVolume = globalVolume.GetComponent<Volume>();
        myVolume.weight = 1;
        isWeekUp = true;
        t = 1.0f;
    }

    void Update()
    {
        if (t < 1.0f && !isWeekUp)
        {
            t += Time.deltaTime * weightChangeSpeed;
            myVolume.weight = Mathf.Lerp(0, 1, t);
        }
        else if (t > 0.06f && isWeekUp)
        {
            t -= Time.deltaTime * weightChangeSpeed;
            myVolume.weight = Mathf.Lerp(0, 1, t);
        }

        //Debug.Log(chapterCamera.parent.parent.TransformPoint(chapterCamera.parent.parent.GetComponent<CharacterController>().center));

        if (LExchange.action.ReadValue<float>() == 1 || RExchange.action.ReadValue<float>() == 1 || XButton.action.ReadValue<float>() == 1 || YButton.action.ReadValue<float>() == 1)
        {
            if (isExchanging0 == false && flyCamera.parent == playerCamera.parent)
            {
                //var pos = chapterModle.transform.position;
                //var rot = chapterModle.transform.rotation;
                //chapterCamera.parent.parent.rotation = playerCamera.parent.parent.rotation;
                //chapterModle.transform.position = pos;

                centerPosWorld = chapterCamera.parent.parent.TransformPoint(chapterCamera.parent.parent.GetComponent<CharacterController>().center);
                centerPosLocal = chapterCamera.parent.parent.GetComponent<CharacterController>().center;
                chapterCamera.parent.parent.position = new Vector3(centerPosWorld.x, chapterCamera.parent.parent.position.y + Yoffect, centerPosWorld.z) - new Vector3(centerPosLocal.x, 0, centerPosLocal.z);
                var rot = chapterModle.transform.rotation;
                chapterCamera.parent.parent.rotation = playerCamera.parent.parent.rotation;
                chapterModle.transform.rotation = rot;

                isExchanging0 = true;
                isExchanging1 = true;
                isExchanging2 = false;

                chapterCamera.gameObject.SetActive(true);

                flyCamera.position = playerCamera.position;
                flyCamera.SetParent(chapterCamera.parent);
                playerCamera.parent.gameObject.SetActive(false);
                
                chapterCamera.parent.parent.GetComponent<ActionBasedContinuousMoveProvider>().forwardSource = chapterCamera;

                centerPosWorld = chapterCamera.parent.parent.TransformPoint(chapterCamera.parent.parent.GetComponent<CharacterController>().center);
                chapterModle.transform.position = new Vector3(centerPosWorld.x, chapterModle.transform.position.y + Yoffect, centerPosWorld.z);

                moveSpeedSaver = chapterCamera.parent.parent.GetComponent<ContinuousMoveProviderBase>().moveSpeed;
                chapterCamera.parent.parent.GetComponent<ContinuousMoveProviderBase>().moveSpeed *= 0.6f;

                isWeekUp = false;
                t = 0.0f;
            }
            else if (isExchanging0 == false && flyCamera.parent == chapterCamera.parent)
            {
                centerPosWorld = chapterCamera.parent.parent.TransformPoint(chapterCamera.parent.parent.GetComponent<CharacterController>().center);
                centerPosLocal = chapterCamera.parent.parent.GetComponent<CharacterController>().center;
                chapterCamera.parent.parent.position = new Vector3(centerPosWorld.x, chapterCamera.parent.parent.position.y + Yoffect, centerPosWorld.z) - new Vector3(centerPosLocal.x, 0, centerPosLocal.z);
                var rot = chapterModle.transform.rotation;
                chapterCamera.parent.parent.rotation = playerCamera.parent.parent.rotation;
                chapterModle.transform.rotation = rot;

                isExchanging0 = true;
                isExchanging1 = false;
                isExchanging2 = true;

                playerCamera.parent.gameObject.SetActive(true);
                flyCamera.position = chapterCamera.position;
                flyCamera.SetParent(playerCamera.parent);
                chapterLeftHand.SetActive(false);
                chapterRightHand.SetActive(false);
                chapterCamera.gameObject.SetActive(false);

                chapterCamera.parent.parent.GetComponent<ActionBasedContinuousMoveProvider>().forwardSource = locator;

                centerPosWorld = chapterCamera.parent.parent.TransformPoint(chapterCamera.parent.parent.GetComponent<CharacterController>().center);
                chapterModle.transform.position = new Vector3(centerPosWorld.x, chapterModle.transform.position.y + Yoffect, centerPosWorld.z);

                chapterCamera.parent.parent.GetComponent<ContinuousMoveProviderBase>().moveSpeed = moveSpeedSaver;

                isWeekUp = false;
                t = 0.0f;
            }
        }

        if (isExchanging1 == true)
        {
            Exchange(chapterCamera);
        }
        else if (isExchanging2 == true)
        {
            Exchange(playerCamera);
        }
    }

    private void Exchange(Transform tgt)
    {
        chapterCamera.parent.parent.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
        chapterCamera.parent.parent.GetComponent<ActionBasedSnapTurnProvider>().enabled = false;

        flyCamera.gameObject.SetActive(true);
        flyCamera.LookAt(tgt);
        //Vector3 movement = new(0, 0, flySpeed * Time.deltaTime);
        //flyCamera.Translate(movement);
        flyCamera.position = Vector3.SmoothDamp(flyCamera.position, tgt.position, ref flyVelocity, flySmoothTime);

        float dis = Vector3.Distance(flyCamera.position, tgt.position);

        if (dis <= minDistance)
        {
            if (tgt == chapterCamera)
            {
                //chapterModle.SetActive(false);
                chapterModle.GetComponent<MeshRenderer>().enabled = false;

                chapterCamera.parent.parent.GetComponent<ActionBasedSnapTurnProvider>().enabled = true;
                chapterLeftHand.SetActive(true);
                chapterRightHand.SetActive(true);
            }

            isWeekUp = true;
            t = 1.0f;

            chapterCamera.parent.parent.GetComponent<ActionBasedContinuousMoveProvider>().enabled = true;

            isExchanging0 = false;
            isExchanging1 = false;
            isExchanging2 = false;
            flyCamera.position = tgt.position;

            flyCamera.gameObject.SetActive(false);
            //Invoke(nameof(Wait), 0.5f);
        }
        else if (dis > minDistance)
        {
            if (tgt == playerCamera)
            {
                //chapterModle.SetActive(true);
                chapterModle.GetComponent<MeshRenderer>().enabled = true;

                chapterCamera.parent.parent.GetComponent<ActionBasedSnapTurnProvider>().enabled = false;
            }
        }
    }

    //private void Wait()
    //{
    //    flyCamera.gameObject.SetActive(false);
    //}
}
