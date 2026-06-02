using UnityEngine;

public class ArtilleryGun : MonoBehaviour
{
    public Transform muzzlePoint;
    public GameObject projectilePrefab;

    public float initialSpeed = 200f;
    public float projectileMass = 5f;

    public GameObject lastProjectile;

    public KeyCode fireKey = KeyCode.Space;

    void Update()
    {
        if (Input.GetKeyDown(fireKey))
            Fire();
    }

    void Fire()
    {
        GameObject proj = Instantiate(projectilePrefab, muzzlePoint.position, muzzlePoint.rotation);

        Rigidbody rb = proj.GetComponent<Rigidbody>();
        rb.mass = projectileMass;

        // ВАЖНО: используем импульс, а не velocity
        rb.AddForce(muzzlePoint.forward * initialSpeed, ForceMode.Impulse);

        lastProjectile = proj;
    }
}
