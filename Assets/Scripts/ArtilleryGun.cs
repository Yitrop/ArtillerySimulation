using UnityEngine;

public class ArtilleryGun : MonoBehaviour
{
    [Header("Transforms")]
    public Transform gunPivot;      // GunPivot � ������������ �������
    public Transform muzzlePoint;   // MuzzlePoint � ����� ������

    [Header("Rotation Settings")]
    public float elevation = 10f;       // ���� �����/����
    public float azimuth = 0f;          // ���� �����/������

    public float minElevation = -5f;    // ����������� ����
    public float maxElevation = 70f;    // ����������� �����

    public float minAzimuth = -90f;     // ����������� �����
    public float maxAzimuth = 90f;      // ����������� ������

    public float elevationSpeed = 30f;  // �������� W/S
    public float azimuthSpeed = 40f;    // �������� A/D

    [Header("Shooting")]
    public GameObject projectilePrefab;
    public float initialSpeed = 200f;
    public float projectileMass = 5f;

    public GameObject lastProjectile;
    public KeyCode fireKey = KeyCode.Space;

    void Update()
    {
        HandleRotation();
        ApplyRotation();

        if (Input.GetKeyDown(fireKey))
            Fire();
    }

    void HandleRotation()
    {
        // W � �����
        if (Input.GetKey(KeyCode.W))
            elevation += elevationSpeed * Time.deltaTime;

        // S � ����
        if (Input.GetKey(KeyCode.S))
            elevation -= elevationSpeed * Time.deltaTime;

        // A � �����
        if (Input.GetKey(KeyCode.A))
            azimuth -= azimuthSpeed * Time.deltaTime;

        // D � ������
        if (Input.GetKey(KeyCode.D))
            azimuth += azimuthSpeed * Time.deltaTime;

        // �����������
        elevation = Mathf.Clamp(elevation, minElevation, maxElevation);
        azimuth = Mathf.Clamp(azimuth, minAzimuth, maxAzimuth);
    }

    void ApplyRotation()
    {
        // �������������� ������� � �� �������� ������� ArtilleryGun
        transform.localRotation = Quaternion.Euler(0, azimuth, 0);

        // ������������ ������� � �� GunPivot
        if (gunPivot != null)
            gunPivot.localRotation = Quaternion.Euler(-elevation, 0, 0);
    }

    void Fire()
    {
        if (projectilePrefab == null || muzzlePoint == null)
        {
            Debug.LogWarning("ArtilleryGun: projectilePrefab или muzzlePoint не назначены");
            return;
        }

        GameObject proj = Instantiate(projectilePrefab, muzzlePoint.position, muzzlePoint.rotation);

        Rigidbody rb = proj.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogWarning("Projectile не имеет Rigidbody");
            return;
        }

        rb.mass = projectileMass;

        // --- УВЕЛИЧИВАЕМ СКОРОСТЬ В 4 РАЗА ---
        float scaledSpeed = initialSpeed;

        // Задаём скорость напрямую
        rb.linearVelocity = muzzlePoint.forward * scaledSpeed;

        lastProjectile = proj;
    }


}
