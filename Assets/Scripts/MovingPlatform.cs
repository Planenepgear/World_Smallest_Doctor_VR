using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour, IItem
{
    [SerializeField] private float moveSpeed;
    private bool isEnabled = false;

    [SerializeField] List<Transform> wayPoints = new List<Transform>();
    private int idx = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (isEnabled)
        {
            Vector3 dest = wayPoints[idx].localPosition;
            if((dest - transform.localPosition).sqrMagnitude< 0.01f)
            {
                ++idx;
                idx %= wayPoints.Count;
            }
            Vector3 moveVec = dest - transform.localPosition;
            moveVec = moveVec.normalized * moveSpeed * Time.deltaTime;
            transform.Translate(moveVec);
        }
    }

    public void Input(bool value)
    {
        isEnabled = !value;
    }
}
