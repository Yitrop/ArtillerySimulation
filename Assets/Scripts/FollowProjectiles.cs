using UnityEngine;

public class FollowProjectile : MonoBehaviour
{
    public ArtilleryGun gun;        // ссылка на пушку
    public Vector3 offset = new Vector3(0, 2, -6);
    public float smoothSpeed = 5f;  // плавность камеры

    void LateUpdate()
    {
        if (gun == null || gun.lastProjectile == null)
            return;

        Transform target = gun.lastProjectile.transform;

        Vector3 desiredPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);

        transform.LookAt(target);
    }
}
