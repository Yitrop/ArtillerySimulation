using UnityEngine;

public class FollowProjectileSide : MonoBehaviour
{
    public ArtilleryGun gun;
    public float sideOffset = 5f;
    public float height = 2f;
    public float smooth = 5f;

    void LateUpdate()
    {
        if (!gameObject.GetComponent<Camera>().enabled)
            return;

        if (gun == null || gun.lastProjectile == null)
            return;

        Transform target = gun.lastProjectile.transform;

        Vector3 right = Vector3.Cross(Vector3.up, target.forward).normalized;

        Vector3 desiredPos =
            target.position +
            right * sideOffset +
            Vector3.up * height;

        transform.position = Vector3.Lerp(transform.position, desiredPos, smooth * Time.deltaTime);

        transform.LookAt(target);
    }
}
