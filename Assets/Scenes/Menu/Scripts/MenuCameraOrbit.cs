using UnityEngine;

public class MenuCameraOrbit : MonoBehaviour
{
    [Header("Orbit Target")]
    public Transform target;

    [Header("Orbit Settings")]
    public float orbitSpeed = 10f;
    public float distance = 8f;
    public float height = 2f;

    [Header("Look Settings")]
    public float lookHeightOffset = 0.5f;

    private float angle;

    void Start()
    {
        if (target == null)
        {
            Debug.LogWarning("MenuCameraOrbit has no target assigned.");
            return;
        }

        Vector3 offset = transform.position - target.position;
        angle = Mathf.Atan2(offset.x, offset.z) * Mathf.Rad2Deg;
    }

    void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        angle += orbitSpeed * Time.deltaTime;

        float radians = angle * Mathf.Deg2Rad;

        Vector3 orbitPosition = new Vector3(
            Mathf.Sin(radians) * distance,
            height + Mathf.Sin(Time.time * 0.6f) * 0.15f,
            Mathf.Cos(radians) * distance
        );

        transform.position = target.position + orbitPosition;

        Vector3 lookPoint = target.position + Vector3.up * lookHeightOffset;
        transform.LookAt(lookPoint);
    }
}
