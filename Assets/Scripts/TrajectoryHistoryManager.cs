using UnityEngine;
using System.Collections.Generic;

public class TrajectoryHistoryManager : MonoBehaviour
{
    public static TrajectoryHistoryManager Instance;

    public Material trajectoryMaterial;
    public int maxTrajectories = 3;

    private List<LineRenderer> trajectories = new List<LineRenderer>();

    void Awake()
    {
        Instance = this;
    }

    public void AddTrajectory(List<Vector3> points)
    {
        // создаЄм объект линии
        GameObject obj = new GameObject("SavedTrajectory");
        LineRenderer lr = obj.AddComponent<LineRenderer>();

        lr.material = new Material(trajectoryMaterial);
        lr.positionCount = points.Count;
        lr.SetPositions(points.ToArray());
        lr.widthMultiplier = 0.05f;

        trajectories.Add(lr);

        // если слишком много Ч удал€ем старые
        if (trajectories.Count > maxTrajectories)
        {
            Destroy(trajectories[0].gameObject);
            trajectories.RemoveAt(0);
        }

        UpdateFade();
    }

    public void ClearTrajectories()
    {
        // ”дал€ем все LineRenderer'ы из сцены
        foreach (var lr in trajectories)
        {
            if (lr != null)
                Destroy(lr.gameObject);
        }

        // ќчищаем список
        trajectories.Clear();
    }

    private void UpdateFade()
    {
        // чем старее лини€ Ч тем прозрачнее
        for (int i = 0; i < trajectories.Count; i++)
        {
            float t = (float)i / (trajectories.Count - 1 + 0.0001f);

            Color c = Color.white;
            c.a = Mathf.Lerp(0.2f, 1f, t);

            trajectories[i].material.color = c;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            ClearTrajectories();
    }

}
