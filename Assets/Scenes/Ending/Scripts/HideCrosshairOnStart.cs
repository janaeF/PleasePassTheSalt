using UnityEngine;

public class HideCrosshairOnStart : MonoBehaviour
{
    private void Start()
    {
        GameObject crosshair = GameObject.Find("Crosshair");

        if (crosshair != null)
        {
            crosshair.SetActive(false);
        }
    }
}
