using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [System.Serializable]
    public class CameraSlot
    {
        public Camera cam;
        public KeyCode hotkey;
        public CameraType type;
    }

    public CameraSlot[] cameras;

    private CameraSlot activeSlot;

    void Start()
    {
        if (cameras.Length > 0)
            ActivateCamera(cameras[0]);
    }

    void Update()
    {
        foreach (var slot in cameras)
        {
            if (Input.GetKeyDown(slot.hotkey))
            {
                ActivateCamera(slot);
            }
        }
    }

    void ActivateCamera(CameraSlot slot)
    {
        // выключаем все камеры и все их контроллеры
        foreach (var s in cameras)
        {
            s.cam.enabled = false;

            var controllers = s.cam.GetComponents<CameraControllerBase>();
            foreach (var c in controllers)
                c.OnDeactivate();
        }

        // включаем нужную камеру и все её контроллеры
        slot.cam.enabled = true;

        var activeControllers = slot.cam.GetComponents<CameraControllerBase>();
        foreach (var c in activeControllers)
            c.OnActivate();

        activeSlot = slot;
    }
}
