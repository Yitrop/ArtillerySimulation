using UnityEngine;

public class CoriolisForce : MonoBehaviour
{
    public float latitudeDeg = 56f; // широта

    Rigidbody rb;
    Vector3 omega;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        float lat = latitudeDeg * Mathf.Deg2Rad;
        float omegaEarth = 7.2921159e-5f; // рад/с

        omega = new Vector3(0, omegaEarth * Mathf.Cos(lat), 0);
    }

    void FixedUpdate()
    {
        Vector3 v = rb.linearVelocity;

        Vector3 aCoriolis = -2f * Vector3.Cross(omega, v);

        rb.AddForce(aCoriolis, ForceMode.Acceleration);
    }
}
