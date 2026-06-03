using UnityEngine;

public class MarkerAppear : MonoBehaviour
{
    public float appearTime = 0.25f;   // время анимации
    public float startScale = 0.1f;    // начальный размер
    public float endScale = 1f;        // конечный размер

    private float timer = 0f;

    void Start()
    {
        transform.localScale = Vector3.one * startScale;
    }

    void Update()
    {
        if (timer < appearTime)
        {
            timer += Time.deltaTime;
            float t = timer / appearTime;
            float scale = Mathf.Lerp(startScale, endScale, t);
            transform.localScale = Vector3.one * scale;
        }
    }
}
