using UnityEngine;

public class ArtilleryGun : MonoBehaviour
{
    [Header("Transforms")]
    public Transform gunPivot;      // GunPivot Ч вертикальный поворот
    public Transform muzzlePoint;   // MuzzlePoint Ч точка вылета

    [Header("Rotation Settings")]
    public float elevation = 10f;       // угол вверх/вниз
    public float azimuth = 0f;          // угол влево/вправо

    public float minElevation = -5f;    // ограничение вниз
    public float maxElevation = 70f;    // ограничение вверх

    public float minAzimuth = -90f;     // ограничение влево
    public float maxAzimuth = 90f;      // ограничение вправо

    public float elevationSpeed = 30f;  // скорость W/S
    public float azimuthSpeed = 40f;    // скорость A/D

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
        // W Ч вверх
        if (Input.GetKey(KeyCode.W))
            elevation += elevationSpeed * Time.deltaTime;

        // S Ч вниз
        if (Input.GetKey(KeyCode.S))
            elevation -= elevationSpeed * Time.deltaTime;

        // A Ч влево
        if (Input.GetKey(KeyCode.A))
            azimuth -= azimuthSpeed * Time.deltaTime;

        // D Ч вправо
        if (Input.GetKey(KeyCode.D))
            azimuth += azimuthSpeed * Time.deltaTime;

        // ќграничени€
        elevation = Mathf.Clamp(elevation, minElevation, maxElevation);
        azimuth = Mathf.Clamp(azimuth, minAzimuth, maxAzimuth);
    }

    void ApplyRotation()
    {
        // √оризонтальный поворот Ч на корневом объекте ArtilleryGun
        transform.localRotation = Quaternion.Euler(0, azimuth, 0);

        // ¬ертикальный поворот Ч на GunPivot
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
        rb.AddForce(muzzlePoint.forward * initialSpeed, ForceMode.Impulse);

        lastProjectile = proj;
    }
}
