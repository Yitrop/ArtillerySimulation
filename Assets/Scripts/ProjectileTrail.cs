using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class ProjectileTrail : MonoBehaviour
{
    public float pointSpacing = 0.1f;

    private LineRenderer lr;
    private List<Vector3> points = new List<Vector3>();
    private Vector3 lastPoint;
    private bool saved = false;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.material = new Material(lr.material);

        lastPoint = transform.position;
        AddPoint();
    }

    void Update()
    {
        if (!TrajectoryManager.Instance.trackingEnabled)
            return;

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

    void OnDisable()
    {
        // чтобы не сохранять дважды
        if (saved) return;
        saved = true;

        // переносим LineRenderer в отдельный объект
        GameObject holder = new GameObject("SavedTrajectory");
        holder.transform.position = Vector3.zero;

        lr.transform.SetParent(holder.transform, true);

        // регистрируем в менеджере
        TrajectoryManager.Instance.RegisterTrajectory(lr);
    }
}
