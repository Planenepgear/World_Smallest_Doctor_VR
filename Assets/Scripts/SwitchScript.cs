using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    public GameObject Handle;
    private HingeJoint thisTingeJoint;
    public GameObject[] items;
    public List<IItem> _items = new List<IItem>();
    private AudioSource audioSource;
    private bool flag = false;

    private float originXRot;
    private bool isChange = false;

    // Start is called before the first frame update
    void Start()
    {
        originXRot = Handle.transform.localRotation.x;

        foreach (var i in items)
            _items.Add(i.GetComponentInChildren<IItem>());
        thisTingeJoint = Handle.GetComponent<HingeJoint>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(thisTingeJoint.angle);
        if (_items.Count == 0) return;

        //if(originXRot != Handle.transform.localRotation.x)
        //{
        //    isChange = true;
        //}

        foreach(var item in _items)
        {
            //print(gameObject);
            //print(thisTingeJoint);
            //print(Handle.transform.localRotation.x);
            if(Handle.transform.localRotation.x > -0.15f)
            {
                if (!flag)
                {
                    Debug.Log("Switch play" + gameObject);
                    flag = true;

                    if(isChange)
                        audioSource.Play();
                    isChange = true;
                    item.Input(Handle.transform.localRotation.x > -0.15f);
                }
            }
            else
            {
                if (flag)
                {
                    Debug.Log("Switch play" + gameObject);
                    flag = false;

                    if (isChange)
                        audioSource.Play();

                    item.Input(Handle.transform.localRotation.x > -0.15f);
                }
            }

        }

    }
}
