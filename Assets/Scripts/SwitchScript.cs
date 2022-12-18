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

    // Start is called before the first frame update
    void Start()
    {
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
                    audioSource.Play();
                    item.Input(Handle.transform.localRotation.x > -0.15f);
                }
            }
            else
            {
                if (flag)
                {
                    Debug.Log("Switch play" + gameObject);
                    flag = false;
                    audioSource.Play();
                    item.Input(Handle.transform.localRotation.x > -0.15f);
                }
            }

        }

    }
}
