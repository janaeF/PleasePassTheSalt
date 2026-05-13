using UnityEngine;

public class BurgerEndSceneMotion : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private Vector3 rotationAxis = Vector3.up;
    [SerializeField] private float rotationSpeed = 45f; // degrees per second

    [Header("Bobbing")]
    [SerializeField] private float bobHeight = 0.25f;
    [SerializeField] private float bobFrequency = 1f; // bobs per second

    [Header("End Screen")]
    [SerializeField] private bool useUnscaledTime = true;

    private Vector3 startLocalPosition;
    private float elapsedTime;

    private void Awake()
    {
        startLocalPosition = transform.localPosition;
    }

    private void Update()
    {
        float delta = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
        elapsedTime += delta;

        // Rotate in one direction.
        Vector3 axis = rotationAxis.sqrMagnitude > 0f ? rotationAxis.normalized : Vector3.up;
        transform.Rotate(axis, rotationSpeed * delta, Space.Self);

        // Bob up and down while keeping the same X and Z position.
        Vector3 nextPosition = startLocalPosition;
        nextPosition.y += Mathf.Sin(elapsedTime * bobFrequency * Mathf.PI * 2f) * bobHeight;
        transform.localPosition = nextPosition;
    }
}
