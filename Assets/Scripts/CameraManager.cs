using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [System.Serializable]
    public class CameraSlot
    {
        public Camera cam;
        public KeyCode hotkey; // клавиша дл€ включени€ камеры
    }

    public CameraSlot[] cameras;

    void Start()
    {
        // ¬ключаем только первую камеру
        for (int i = 0; i < cameras.Length; i++)
            cameras[i].cam.enabled = (i == 0);
    }

    void Update()
    {
        foreach (var slot in cameras)
        {
            if (Input.GetKeyDown(slot.hotkey))
            {
                ActivateCamera(slot.cam);
            }
        }
    }

    void ActivateCamera(Camera target)
    {
        foreach (var slot in cameras)
            slot.cam.enabled = false;

        target.enabled = true;
    }
}
