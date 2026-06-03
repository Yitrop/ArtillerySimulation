using UnityEngine;

public class FreeCameraController : CameraControllerBase
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float verticalSpeed = 7f;

    private bool active = false;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Awake()
    {
        // сохраняем стартовую позицию камеры
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    public override void OnActivate()
    {
        active = true;

        // сбрасываем позицию и поворот
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }

    public override void OnDeactivate()
    {
        active = false;
    }

    void Update()
    {
        if (!active)
            return;

        Vector3 move = Vector3.zero;

        // движение вперёд/назад
        if (Input.GetKey(KeyCode.I))
            move += transform.forward;

        if (Input.GetKey(KeyCode.K))
            move -= transform.forward;

        // движение влево/вправо
        if (Input.GetKey(KeyCode.J))
            move -= transform.right;

        if (Input.GetKey(KeyCode.L))
            move += transform.right;

        // движение вверх/вниз
        if (Input.GetKey(KeyCode.LeftShift))
            move += Vector3.up;

        if (Input.GetKey(KeyCode.LeftControl))
            move -= Vector3.up;

        // нормализуем, чтобы диагонали не ускоряли движение
        if (move.magnitude > 1f)
            move.Normalize();

        transform.position += move * moveSpeed * Time.deltaTime;
    }
}
