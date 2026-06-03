using UnityEngine;

public class CameraZoom : CameraControllerBase
{
    [Header("Zoom Settings")]
    public bool allowZoom = true;
    public float zoomSpeed = 40f;
    public float minFOV = 20f;
    public float maxFOV = 80f;

    private Camera cam;
    private bool active = false;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

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
        if (!active || !allowZoom)
            return;

        float delta = 0f;

        // -----  лавиша O Ч приближение -----
        if (Input.GetKey(KeyCode.O))
        {
            delta = -1f;
            Debug.Log("ZOOM IN (O)");
        }

        // -----  лавиша P Ч отдаление -----
        if (Input.GetKey(KeyCode.P))
        {
            delta = 1f;
            Debug.Log("ZOOM OUT (P)");
        }

        if (Mathf.Abs(delta) > 0.01f)
        {
            cam.fieldOfView += delta * zoomSpeed * Time.deltaTime;
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minFOV, maxFOV);

            Debug.Log("FOV = " + cam.fieldOfView);
        }
    }
}
