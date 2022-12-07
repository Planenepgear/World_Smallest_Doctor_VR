using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandCheck : MonoBehaviour
{
    public MainSceneManager levelController;
    public GameObject handRealL;
    public GameObject handRealR;

    public GameObject handModelL;
    public GameObject handModelR;
    public float minDistance = 1f;

    public bool isLeftHand;
    public bool isRightHand;
    private bool isSwitch = false;

    void Start()
    {
        handModelL.SetActive(false);
        handModelR.SetActive(false);

        isLeftHand = false;
        isRightHand = false;
    }

    void Update()
    {
        if (handRealL && handModelL)
        {
            if((handRealL.transform.position - handModelL.transform.position).sqrMagnitude <= minDistance)
            {
                handRealL.SetActive(false);
                handModelL.SetActive(true);
                isLeftHand =  true;
            }
        }
        if ((handRealL.transform.position - handModelL.transform.position).sqrMagnitude > minDistance)
        {
            handRealL.SetActive(true);
            handModelL.SetActive(false);
            isLeftHand = false;
        }

        if (handRealR && handModelR)
        {
            if ((handRealR.transform.position - handModelR.transform.position).sqrMagnitude <= minDistance)
            {
                handRealR.SetActive(false);
                handModelR.SetActive(true);
                isRightHand = true;
            }
        }
        if ((handRealR.transform.position - handModelR.transform.position).sqrMagnitude > minDistance)
        {
            handRealR.SetActive(true);
            handModelR.SetActive(false);
            isRightHand = false;
        }


        if(isLeftHand && isRightHand && !isSwitch)
        {
            Invoke(nameof(GoNext), 1f);
            isSwitch = true;
        }
    }

    private void GoNext()
    {
        levelController.NextScene();
    }
}
