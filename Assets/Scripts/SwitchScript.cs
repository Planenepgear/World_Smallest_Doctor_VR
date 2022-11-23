using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    public GameObject Handle;
    private HingeJoint thisTingeJoint;
    public GameObject[] items;
    public List<IItem> _items = new List<IItem>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (var i in items)
            _items.Add(i.GetComponentInChildren<IItem>());
        thisTingeJoint = Handle.GetComponent<HingeJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(thisTingeJoint.angle);
        if (_items.Count == 0) return;
        foreach(var item in _items)
        {
            print(gameObject);
            print(thisTingeJoint);
            item.Input(thisTingeJoint.angle > -55f);
        }

    }
}
