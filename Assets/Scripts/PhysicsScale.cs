using UnityEngine;

public class PhysicsScale : MonoBehaviour
{
    // Коэффициент сжатия дальности
    public static float rangeScale = 75f;

    void Awake()
    {
        // Увеличиваем гравитацию в rangeScale раз
        // чтобы дальность уменьшилась в rangeScale раз
        Physics.gravity = new Vector3(0, -9.81f * rangeScale, 0);
    }
}
