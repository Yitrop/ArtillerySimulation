using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryPreview : MonoBehaviour
{
    public ArtilleryGun gun;

    public int maxSteps = 400;
    public float timeStep = 0.02f;
    public float maxTime = 10f;

    public KeyCode toggleKey = KeyCode.X;
    private bool visible = true;

    LineRenderer lr;

    // œ¿–¿Ã≈“–€  ¿  ” —Õ¿–ﬂƒ¿
    public float airDensity0 = 1.225f;
    public float dragCoefficient = 0.47f;
    public float radius = 0.061f;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.material = new Material(lr.material);

        // ÍÓË˜ÌÂ‚˚È
        lr.startColor = new Color(0.6f, 0.4f, 0.2f);
        lr.endColor = new Color(0.6f, 0.4f, 0.2f);
    }

    void Update()
    {
        if (gun == null)
            return;

        if (Input.GetKeyDown(toggleKey))
        {
            visible = !visible;
            lr.enabled = visible;
        }

        if (visible)
            DrawTrajectory();
    }

    void DrawTrajectory()
    {
        List<Vector3> points = new List<Vector3>();

        Vector3 pos = gun.muzzlePoint.position;
        Vector3 vel = gun.muzzlePoint.forward * gun.initialSpeed;

        float mass = gun.projectileMass;
        float area = Mathf.PI * radius * radius;

        float t = 0f;

        for (int i = 0; i < maxSteps && t < maxTime; i++)
        {
            points.Add(pos);

            // √–¿¬»“¿÷»ﬂ
            vel += Physics.gravity * timeStep;

            // DRAG ó  ¿  ¬ —»Ã”Àﬂ÷»», ÕŒ ¡≈« “”–¡”À≈Õ“ÕŒ—“»
            float speed = vel.magnitude;
            if (speed > 0.01f)
            {
                float height = pos.y;
                float airDensity = airDensity0 * Mathf.Exp(-height / 8500f);

                float dragForce = 0.5f * airDensity * dragCoefficient * area * speed * speed;
                Vector3 drag = -vel.normalized * dragForce;

                // a = F / m
                vel += (drag / mass) * timeStep;
            }

            pos += vel * timeStep;

            if (pos.y < 0f)
                break;

            t += timeStep;
        }

        lr.positionCount = points.Count;
        lr.SetPositions(points.ToArray());
    }
}
