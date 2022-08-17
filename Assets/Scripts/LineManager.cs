using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class LineManager : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    [SerializeField]
    private TextMeshPro _textMeshPro;
    

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();    
    }
    public void DrawLine(Transform anchor)
    {

        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, anchor.transform.position);

        if (_lineRenderer.positionCount > 1)
        {
            Vector3 pointA = _lineRenderer.GetPosition(_lineRenderer.positionCount - 1);
            Vector3 pointB = _lineRenderer.GetPosition(_lineRenderer.positionCount - 2);

            float dist = Vector3.Distance(pointA, pointB);

            TextMeshPro distText = Instantiate(_textMeshPro);

            distText.text = (dist).ToString("F2") + " m";
            //distText.text = "working";

            Vector3 directionVector = (pointB - pointA);

            Vector3 normal = anchor.transform.up;

            Vector3 upd = Vector3.Cross(directionVector, normal).normalized;
            Quaternion rotation = Quaternion.LookRotation(-normal, upd);

            distText.transform.rotation = rotation;

            distText.transform.position = (pointA + directionVector * 0.5f) + upd * 0.008f;
        }
    }
}
