using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCheck : MonoBehaviour
{
    public GameObject hand;
    public GameObject handModel;
    public float minDistance = 0.1f;

    public bool isLeftHand;
    public bool isRightHand;

    // Start is called before the first frame update
    void Start()
    {
        handModel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (hand && isLeftHand)
        {
            if((transform.position - hand.transform.position).sqrMagnitude <= minDistance)
            {
                hand.SetActive(false);
                handModel.SetActive(true);
                //transform.parent.SendMessage("",)
            }
        }
        else if (hand && isRightHand)
        {
            if ((transform.position - hand.transform.position).sqrMagnitude <= minDistance)
            {
                hand.SetActive(false);
                handModel.SetActive(true);
                //transform.parent.SendMessage("",)
            }
        }
    }
}
