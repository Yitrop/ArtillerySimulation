using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class ProjectileTrail : MonoBehaviour
{
    public float pointSpacing = 0.1f;

    private LineRenderer lr;
    private List<Vector3> points = new List<Vector3>();
    private Vector3 lastPoint;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.material = new Material(lr.material);

        lastPoint = transform.position;
        AddPoint();
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, lastPoint);

        if (dist >= pointSpacing)
        {
            AddPoint();
            lastPoint = transform.position;
        }
    }

    void AddPoint()
    {
        points.Add(transform.position);
        lr.positionCount = points.Count;
        lr.SetPositions(points.ToArray());
    }
}
