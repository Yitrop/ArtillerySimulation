using UnityEngine;

public class RealisticDrag : MonoBehaviour
{
    public float airDensity0 = 1.225f;
    public float dragCoefficient = 0.47f;
    public float radius = 0.061f;
    public float turbulenceStrength = 0.02f;
    public float massInfluence = 50f;

    Rigidbody rb;
    float area;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        area = Mathf.PI * radius * radius;
    }

    void FixedUpdate()
    {
        Vector3 v = rb.linearVelocity;
        float speed = v.magnitude;

        // 1) ��� ����� ������ �������� drag ����� ��������
        if (speed < 0.5f)
            return;

        float height = transform.position.y;
        float airDensity = airDensity0 * Mathf.Exp(-height / 8500f);

        float dragForce;

        // 2) �������� drag ��� ������ �������� (�����������)
        if (speed < 5f)
            dragForce = airDensity * dragCoefficient * area * speed * 0.1f;
        else
            dragForce = 0.5f * airDensity * dragCoefficient * area * speed * speed;

        // 3) �������� ���������
        dragForce /= (1f + rb.mass / massInfluence);

        // 4) ����������� ������������� drag (�����������)
        dragForce = Mathf.Min(dragForce, rb.mass * 200f);

        Vector3 drag = -v.normalized * dragForce;
        
        // --- ТУРБУЛЕНТНОСТЬ, ЗАВИСЯЩАЯ ОТ ВЫСОТЫ ---

        // высота
        float h = transform.position.y;

        // параметры "слоя максимальной турбулентности"
        float h0 = 800f;   // высота максимума турбулентности
        float w = 600f;   // ширина зоны

        // коэффициент по высоте (0..1)
        float heightTurbulenceFactor = Mathf.Exp(-Mathf.Pow((h - h0) / w, 2f));

        // чуть приглушим, чтобы не ломало траекторию
        float effectiveTurbulence = turbulenceStrength * heightTurbulenceFactor;

        Vector3 turbulence = new Vector3(
            Random.Range(-effectiveTurbulence, effectiveTurbulence),
            Random.Range(-effectiveTurbulence, effectiveTurbulence),
            Random.Range(-effectiveTurbulence, effectiveTurbulence)
        );

        // --- ПРИМЕНЕНИЕ СИЛ ---

        rb.AddForce(drag + turbulence, ForceMode.Force);
    }
}
