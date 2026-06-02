using UnityEngine;

public class FollowProjectileBack : MonoBehaviour
{
    public ArtilleryGun gun;
    public Vector3 offset = new Vector3(0, 2, -6);
    public float smooth = 5f;

    void LateUpdate()
    {
        if (!gameObject.GetComponent<Camera>().enabled)
            return;

        if (gun == null || gun.lastProjectile == null)
            return;

        Transform target = gun.lastProjectile.transform;

        Vector3 desiredPos = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, desiredPos, smooth * Time.deltaTime);

        transform.LookAt(target);
    }
}
