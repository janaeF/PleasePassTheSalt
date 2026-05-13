using UnityEngine;
using UnityEngine.InputSystem;

public class Flashlight : MonoBehaviour
{
    public Light flashlight;
    public KeyCode toggleKey = KeyCode.F;
    private bool _isOn = true;

    void Start()
    {
        if (flashlight != null)
            flashlight.enabled = _isOn;
    }

    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            _isOn = !_isOn;
            if (flashlight != null)
                flashlight.enabled = _isOn;
        }
    }
}