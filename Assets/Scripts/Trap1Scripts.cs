using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap1Scripts : MonoBehaviour, IItem
{
    public GameObject trapSpikesPrefab;

    private GameObject insTrapSpikes;
    private Vector3 trapPosition;
    
    void Start()
    {
        insTrapSpikes = Instantiate(trapSpikesPrefab, transform.position, transform.rotation);
        insTrapSpikes.transform.SetParent(this.gameObject.transform);
        insTrapSpikes.transform.localScale = this.gameObject.transform.localScale;

        trapPosition = transform.position;
    }

    void Update()
    {
        if(insTrapSpikes.GetComponent<FloatingSwayMove>().Enabled == true)
        {
            if (trapPosition != transform.position)
            {
                Destroy(insTrapSpikes);
                insTrapSpikes = Instantiate(trapSpikesPrefab, transform.position, transform.rotation);
                insTrapSpikes.transform.SetParent(this.gameObject.transform);
                insTrapSpikes.transform.localScale = this.gameObject.transform.localScale;

                trapPosition = transform.position;
            }
        }
    }


    public void Input(bool value)
    {
        if (insTrapSpikes)
        {
            if (value)
            {
                insTrapSpikes.GetComponent<FloatingSwayMove>().Enabled = false;
            }
            else if (!value)
            {
                insTrapSpikes.GetComponent<FloatingSwayMove>().Enabled = true;
            }
        }
    }
}
