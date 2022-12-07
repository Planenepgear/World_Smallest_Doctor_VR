using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFix : MonoBehaviour
{
    public int gravity = -35;

    void Start()
    {
        
    }

    void Update()
    {
        Physics.gravity = new Vector3(0, gravity, 0);  // gravity= -35
    }
}
