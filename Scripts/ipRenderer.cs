using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ipRenderer : MonoBehaviour
{
    LineRenderer lineRenderer;
    float cizgi_Genisligi = 0.05f;
    [SerializeField]
    Transform baslangicTransform;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.startWidth = cizgi_Genisligi;
    }

    public void ipGosteFNK(Vector3 hedefPos,bool enableRenderer)
    {
        if (enableRenderer)
        {
            if(!lineRenderer.enabled)
            {
                lineRenderer.enabled = true;
            }
            lineRenderer.positionCount = 2;
        } else
        {
            lineRenderer.positionCount = 0;
            if(lineRenderer.enabled)
            {
                lineRenderer.enabled = false;
            }
        }

        if(enableRenderer)
        {
            Vector3 temp = baslangicTransform.position;
            temp.z = -3f;

            baslangicTransform.position = temp;

            temp = hedefPos;
            temp.z = 0f;
            hedefPos = temp;

            lineRenderer.SetPosition(0, baslangicTransform.position);
            lineRenderer.SetPosition(1,hedefPos);
        }
    }
  
}
