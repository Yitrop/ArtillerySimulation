using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 30f;
    private Vector3 startPosition;
    private bool hasHit = false;

    void Start()
    {
        startPosition = transform.position;
        Destroy(gameObject, lifetime);
    }


    void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return; // защита от двойного вызова
        hasHit = true;

        Debug.Log("Попадание в: " + collision.gameObject.name);

        // проверяем, что это земля
        if (collision.gameObject.CompareTag("Ground"))
        {
            float distance = Vector3.Distance(startPosition, transform.position);

            // создаём маркер
            MarkerManager.Instance.CreateMarker(transform.position, distance);
        }

        Destroy(gameObject, 0.03f);
        enabled = false;
    }

}
