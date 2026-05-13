using UnityEngine;

public class RainbowBackground : MonoBehaviour
{
    [Range(0f, 1f)]
    public float hue;
    public float speed = 0.1f;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        // Cycle hue over time
        hue += speed * Time.deltaTime;
        if (hue > 1f) hue -= 1f;

        // Convert HSV to RGB for the background
        cam.backgroundColor = Color.HSVToRGB(hue, 1f, 1f);
    }
}

