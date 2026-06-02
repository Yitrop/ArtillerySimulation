using UnityEngine;
using UnityEngine.UI;

public class ArtilleryUI : MonoBehaviour
{
    public ArtilleryGun gun;

    [Header("Sliders")]
    public Slider massSlider;
    public Slider speedSlider;

    [Header("Texts")]
    public Text massValueText;
    public Text speedValueText;

    void Start()
    {
        // Инициализируем слайдеры значениями из пушки
        if (gun != null)
        {
            massSlider.value = gun.projectileMass;
            speedSlider.value = gun.initialSpeed;
        }

        UpdateUI();
    }

    public void OnMassChanged()
    {
        if (gun != null)
            gun.projectileMass = massSlider.value;

        UpdateUI();
    }

    public void OnSpeedChanged()
    {
        if (gun != null)
            gun.initialSpeed = speedSlider.value;

        UpdateUI();
    }

    void UpdateUI()
    {
        if (massValueText != null)
            massValueText.text = massSlider.value.ToString("F1") + " кг";

        if (speedValueText != null)
            speedValueText.text = speedSlider.value.ToString("F0") + " м/с";
    }
}
