using UnityEngine;
using System.Collections.Generic;

public class ProjectileTrailRecorder : MonoBehaviour
{
    public float pointSpacing = 0.1f;

    private List<Vector3> points = new List<Vector3>();
    private Vector3 lastPoint;

    void Start()
    {
        lastPoint = transform.position;
        points.Add(lastPoint);
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, lastPoint);

        if (dist >= pointSpacing)
        {
            lastPoint = transform.position;
            points.Add(lastPoint);
        }
    }

    void OnDestroy()
    {
        if (points.Count > 1)
            TrajectoryHistoryManager.Instance.AddTrajectory(points);
    }
}
