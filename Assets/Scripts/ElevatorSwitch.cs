using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorSwitch : MonoBehaviour
{
    public GameObject elevator;

    // Start is called before the first frame update
    void Start()
    {
        elevator.transform.SetParent(transform.parent);
    }
}
