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
        // выключаем все камеры и их контроллеры
        foreach (var s in cameras)
        {
            s.cam.enabled = false;

            var controller = s.cam.GetComponent<CameraControllerBase>();
            if (controller != null)
                controller.OnDeactivate();
        }

        // включаем нужную
        slot.cam.enabled = true;

        var activeController = slot.cam.GetComponent<CameraControllerBase>();
        if (activeController != null)
            activeController.OnActivate();

        activeSlot = slot;
    }
}
