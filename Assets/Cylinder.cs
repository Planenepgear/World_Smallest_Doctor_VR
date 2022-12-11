using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour
{
    public GameObject liquid;
    public float current_liquid;
    public float max;

    public bool inCoroutine = false;

    private void Start()
    {
        liquid.transform.localScale = new Vector3(1, current_liquid, 1);
    }
    public float GetCanAddLiquid()
    {
        if (inCoroutine)
            return 0;
        return max - current_liquid > 0 ? max - current_liquid : 0;
    }

    public float GetCanRemoveLiquid()
    {
        return current_liquid;
    }

    public void RemoveLiquid(float v)
    {
        if (0 > current_liquid - v)
        {
            Debug.LogError("Cylinder Game ERROR!");
        }
        StartCoroutine(MoveLiquid(current_liquid - v));
    }
    public void AddLiquid(float v)
    {

        if(max < current_liquid + v)
        {
            Debug.LogError("Cylinder Game ERROR!");
        }

        StartCoroutine(MoveLiquid(current_liquid + v));
    }

    IEnumerator MoveLiquid(float dest)
    {
        inCoroutine = true;
        Debug.Log("move liquid " + current_liquid + " to " + dest);
        float fillSpeed = 1;
        if(dest < current_liquid)
        {
            fillSpeed = -1;
        }

        while (Mathf.Abs(  current_liquid - dest) > 0.1f)
        {
            current_liquid += fillSpeed * Time.deltaTime;
            liquid.transform.localScale = new Vector3(1, current_liquid, 1);
            yield return null;
        }
        current_liquid = dest;
        liquid.transform.localScale = new Vector3(1, current_liquid, 1);

        inCoroutine = false;
    }

}
