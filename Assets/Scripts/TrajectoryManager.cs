using UnityEngine;
using System.Collections.Generic;

public class TrajectoryManager : MonoBehaviour
{
    public static TrajectoryManager Instance;

    private List<LineRenderer> trails = new List<LineRenderer>();
    public bool trackingEnabled = true;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            trackingEnabled = !trackingEnabled;

        if (Input.GetKeyDown(KeyCode.C))
            ClearAll();
    }

    public void RegisterTrajectory(LineRenderer lr)
    {
        trails.Add(lr);

        // оставляем только 2 последних
        if (trails.Count > 2)
        {
            Destroy(trails[0].gameObject);
            trails.RemoveAt(0);
        }

        UpdateFade();
    }

    private void UpdateFade()
    {
        for (int i = 0; i < trails.Count; i++)
        {
            float alpha = (i == trails.Count - 1) ? 1f : 0.35f;

            Color c = trails[i].startColor;
            c.a = alpha;

            trails[i].startColor = c;
            trails[i].endColor = c;
        }
    }

    public void ClearAll()
    {
        foreach (var lr in trails)
            Destroy(lr.gameObject);

        trails.Clear();
    }
}
