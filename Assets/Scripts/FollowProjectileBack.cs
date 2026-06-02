using UnityEngine;

public class FollowProjectileBack : CameraControllerBase
{
    public ArtilleryGun gun;
    public Vector3 offset = new Vector3(0, 2, -6);
    public float smooth = 5f;

    private bool active = false;

    public override void OnActivate()
    {
        active = true;
    }

    public override void OnDeactivate()
    {
        active = false;
    }

    void LateUpdate()
    {
        if (!active)
            return;

        if (gun == null || gun.lastProjectile == null)
            return;

        Transform target = gun.lastProjectile.transform;

        Vector3 desiredPos = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, desiredPos, smooth * Time.deltaTime);

        transform.LookAt(target);
    }
}
