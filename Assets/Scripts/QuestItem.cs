using UnityEngine;

public class QuestItem : MonoBehaviour
{
    public int itemIndex;
    public string itemName;
    public float pickupRange = 3f;
    public AudioClip pickupSound;

    private bool _collectable = false;
    private bool _collected = false;

    [Header("Highlight")]
    public Renderer itemRenderer;
    public Color highlightColor = Color.yellow;
    private Color _originalColor;

    void Start()
    {
        if (itemRenderer != null)
            _originalColor = itemRenderer.material.color;
    }

    public void SetCollectable(bool collectable)
    {
        _collectable = collectable;
        if (itemRenderer != null)
            itemRenderer.material.color = collectable ? highlightColor : _originalColor;
    }

    void Update()
    {
        if (!_collectable || _collected) return;

        if (UnityEngine.InputSystem.Mouse.current.leftButton.wasPressedThisFrame)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                float dist = Vector3.Distance(transform.position, player.transform.position);
                if (dist <= pickupRange)
                    Collect();
            }
        }
    }

    void Collect()
    {
        _collected = true;

        if (pickupSound != null)
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);

        gameObject.SetActive(false);
        QuestManager.Instance.ItemCollected(itemIndex);
    }
}