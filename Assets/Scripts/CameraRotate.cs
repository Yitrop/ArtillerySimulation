using UnityEngine;

public class CameraRotate : CameraControllerBase
{
    [Header("Rotation Settings")]
    public bool allowRotation = true;
    public float rotationSpeed = 60f;

    private bool active = false;

    public override void OnActivate()
    {
        active = true;
    }

    public override void OnDeactivate()
    {
        active = false;
    }

    void Update()
    {
        if (!active || !allowRotation)
            return;

        float yaw = 0f;
        float pitch = 0f;

        // Вращение по горизонтали
        if (Input.GetKey(KeyCode.LeftArrow))
            yaw = -1f;

        if (Input.GetKey(KeyCode.RightArrow))
            yaw = 1f;

        // Вращение по вертикали
        if (Input.GetKey(KeyCode.UpArrow))
            pitch = -1f;

        if (Input.GetKey(KeyCode.DownArrow))
            pitch = 1f;

        if (yaw != 0f || pitch != 0f)
        {
            transform.Rotate(
                pitch * rotationSpeed * Time.deltaTime,
                yaw * rotationSpeed * Time.deltaTime,
                0f,
                Space.Self
            );
        }
    }
}
