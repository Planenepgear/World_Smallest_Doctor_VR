using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderGame : MonoBehaviour
{

    [SerializeField] Cylinder l;
    [SerializeField] Cylinder m;
    [SerializeField] Cylinder r;

    [SerializeField] float rotateSpeed;

    private bool inCoroutine = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            LiquidMove();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SwapLM();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            SwapMR();
        }
    }

    public void LiquidMove()
    {
        if (inCoroutine || l.inCoroutine || m.inCoroutine || r.inCoroutine)
            return;
        float a = l.GetCanAddLiquid();
        if (a == 0)
            return;
        float b = m.GetCanRemoveLiquid();
        if (b == 0)
            return;
        float v = a > b ? b : a;
        Debug.Log(v);
        
        if (l.max == 5 && l.current_liquid + v == 4.5)
        {
            Debug.Log("GGWP");
        }

        l.AddLiquid(v);
        m.RemoveLiquid(v);

    }

    public void SwapLM()
    {
        if (inCoroutine || l.inCoroutine || m.inCoroutine || r.inCoroutine)
            return;
        StartCoroutine(_SwapLM());

    }

    public void SwapMR()
    {
        if (inCoroutine || l.inCoroutine || m.inCoroutine || r.inCoroutine)
            return;
        StartCoroutine(_SwapMR());

    }

    IEnumerator _SwapLM()
    {
        inCoroutine = true;
        Vector3 lOrigin = l.transform.localPosition;
        Vector3 mOrigin = m.transform.localPosition;
        Vector3 O = (l.transform.localPosition + m.transform.localPosition)/2;
        float R = (l.transform.localPosition - m.transform.localPosition).magnitude / 2;

        float theta = 0;
        while (theta < Mathf.PI)
        {
            theta += rotateSpeed * Time.deltaTime * Mathf.PI / 180f;
            Vector3 dest = R*new Vector3(Mathf.Cos(theta), 0, Mathf.Sin(theta));
            Vector3 moveVec = (O + dest) - l.transform.localPosition;
            m.transform.localPosition -= moveVec;
            l.transform.localPosition += moveVec;
            yield return null;
        }
        l.transform.localPosition = mOrigin;
        m.transform.localPosition = lOrigin;
        Cylinder temp = l;
        l = m;
        m = temp;

        inCoroutine = false;
    }

    IEnumerator _SwapMR()
    {
        inCoroutine = true;
        Vector3 mOrigin = m.transform.localPosition;
        Vector3 rOrigin = r.transform.localPosition;
        Vector3 O = (m.transform.localPosition + r.transform.localPosition) / 2;
        float R = (m.transform.localPosition - r.transform.localPosition).magnitude / 2;

        float theta = 0;
        while (theta < Mathf.PI)
        {
            theta += rotateSpeed * Time.deltaTime * Mathf.PI / 180f;
            Vector3 dest = R * new Vector3(Mathf.Cos(theta), 0, Mathf.Sin(theta));
            Vector3 moveVec = (O + dest) - m.transform.localPosition;
            moveVec.y = 0f;
            r.transform.localPosition -= moveVec;
            m.transform.localPosition += moveVec;
            yield return null;
        }
        m.transform.localPosition = rOrigin;
        r.transform.localPosition = mOrigin;
        Cylinder temp = m;
        m = r;
        r = temp;

        inCoroutine = false;
    }


}
