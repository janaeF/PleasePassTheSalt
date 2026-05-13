using UnityEngine;

public class MovingWall : MonoBehaviour
{
    [Header("Movement")]
    public float moveDistance = 3f;
    public float moveSpeed = 2f;

    private Vector3 _startPos;
    private Vector3 _upPos;
    private bool _movingUp = true;

    void Start()
    {
        _startPos = transform.position;
        _upPos = _startPos + Vector3.up * moveDistance;
    }

    void Update()
    {
        Vector3 target = _movingUp ? _upPos : _startPos;
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.01f)
            _movingUp = !_movingUp;
    }
}