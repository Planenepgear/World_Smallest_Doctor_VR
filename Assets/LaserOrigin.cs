using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserOrigin : MonoBehaviour
{
    List<Vector3> positiones = new List<Vector3>();

    LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {

        lineRenderer = GetComponent<LineRenderer>();
//        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.positionCount = 0;
        positiones.Clear();

        RaycastHit hit;
        Vector3 start = transform.position;
        Vector3 dir = transform.forward;

        positiones.Add(start);

        bool flag = false;
        while (Physics.Raycast(start, dir, out hit))
        {
            if(hit.collider.gameObject.CompareTag("LaserPoint"))
            {
                LaserPoint laserPoint= hit.collider.gameObject.GetComponent<LaserPoint>();
                laserPoint.On();

                positiones.Add(hit.point);
                flag = true;
                break;
            }

            if (hit.collider.gameObject.CompareTag("Mirror"))
            {
                positiones.Add(hit.point);

                start = hit.point;
                dir = Vector3.Reflect(dir, hit.normal);
            }
            else
            {
                positiones.Add(hit.point);
                flag = true;
                break;
            }
        }

        if(!flag)
        {
            positiones.Add(start += dir * 100);
        }

        lineRenderer.positionCount = positiones.Count;

        for (int i = 0; i < positiones.Count; i++)
        {
            
            lineRenderer.SetPosition(i, positiones[i]);
        }

    }
}
