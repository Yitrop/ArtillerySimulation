using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject shellPrefab;
    public Transform firePoint;
    public float shootForce = 690f;
    public KeyCode fireKey = KeyCode.Space;

    void Update()
    {
        if (Input.GetKeyDown(fireKey))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (shellPrefab == null || firePoint == null)
        {
            Debug.LogError("ShellPrefab ��� FirePoint �� ���������!");
            return;
        }

        GameObject shell = Instantiate(shellPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = shell.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * shootForce;
        }
    }
}
