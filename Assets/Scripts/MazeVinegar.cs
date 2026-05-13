using UnityEngine;
using UnityEngine.InputSystem;

public class MazeVinegar : MonoBehaviour
{
    public float pickupRange = 3f;
    private bool _collected = false;

    [Header("Highlight")]
    public Renderer itemRenderer;
    public Color highlightColor = Color.yellow;
    private Color _originalColor;

    void Start()
    {
        if (itemRenderer != null)
            _originalColor = itemRenderer.material.color;
        
        // Highlight immediately since it's always collectable
        if (itemRenderer != null)
            itemRenderer.material.color = highlightColor;
    }

    void Update()
    {
        if (_collected) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
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
        gameObject.SetActive(false);
        MazeBarrier.Instance.UnlockBarrier();
        DialogueSystem.Instance.StartDialogue("You", new string[] {
            "Found the vinegar! Now I need to get back to Mom!"
        });
    }
}