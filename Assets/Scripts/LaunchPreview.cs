using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPreview : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private Vector3 dragStartPoint;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetStartPoint(Vector3 worldPoint)
    {
        dragStartPoint = worldPoint;
        _lineRenderer.SetPosition(0, dragStartPoint);
    }

    public void SetEndPoint(Vector3 worldPoint)
    {
        _lineRenderer.SetPosition(1, worldPoint);
    }
}
