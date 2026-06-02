using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 30f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Попадание в: " + collision.gameObject.name);
        Destroy(gameObject, 0.1f);
    }
    
}
