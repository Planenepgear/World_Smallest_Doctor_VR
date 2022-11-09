using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    public GameObject Handle;
    private HingeJoint thisTingeJoint;

    public GameObject[] items;

    // Start is called before the first frame update
    void Start()
    {
        thisTingeJoint = Handle.GetComponent<HingeJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(thisTingeJoint.angle);

        if (thisTingeJoint.angle <= -55f)
        {
            foreach (var item in items)
            {
                if (item.CompareTag("Door1"))
                {
                    item.SetActive(false);
                }
                else if (item.CompareTag("Trap1"))
                {
                    item.SendMessage("StopTrap", true);
                }
            }
        }
        else
        {
            foreach (var item in items)
            {
                if (item.CompareTag("Door1"))
                {
                    item.SetActive(true);
                }
                else if (item.CompareTag("Trap1"))
                {
                    item.SendMessage("StopTrap", false);
                }
            }
        }
    }
}
