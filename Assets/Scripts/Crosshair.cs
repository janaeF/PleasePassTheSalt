using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    [Header("Crosshair Settings")]
    public Color normalColor = Color.white;
    public Color hoverColor = Color.yellow;
    public float crosshairSize = 20f;
    public float lineThickness = 2f;

    private Image _horizontal;
    private Image _vertical;
    private RectTransform _crosshairParent;

    void Start()
    {
        // Hide default cursor
        Cursor.visible = false;

        // Create crosshair UI
        Canvas canvas = FindAnyObjectByType<Canvas>();

        // Parent object
        GameObject parent = new GameObject("Crosshair");
        parent.transform.SetParent(canvas.transform, false);
        _crosshairParent = parent.AddComponent<RectTransform>();

        // Horizontal line
        GameObject h = new GameObject("Horizontal");
        h.transform.SetParent(parent.transform, false);
        _horizontal = h.AddComponent<Image>();
        _horizontal.color = normalColor;
        RectTransform hRect = h.GetComponent<RectTransform>();
        hRect.sizeDelta = new Vector2(crosshairSize, lineThickness);

        // Vertical line
        GameObject v = new GameObject("Vertical");
        v.transform.SetParent(parent.transform, false);
        _vertical = v.AddComponent<Image>();
        _vertical.color = normalColor;
        RectTransform vRect = v.GetComponent<RectTransform>();
        vRect.sizeDelta = new Vector2(lineThickness, crosshairSize);
    }

    void Update()
    {
        // Move crosshair to mouse position
        _crosshairParent.position = Input.mousePosition;

        // Check if hovering over interactable
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 3f))
        {
            if (hit.transform.GetComponent<NPCDialogue>() != null ||
                hit.transform.GetComponent<QuestItem>() != null)
            {
                // Turn yellow when hovering over interactable
                _horizontal.color = hoverColor;
                _vertical.color = hoverColor;
            }
            else
            {
                _horizontal.color = normalColor;
                _vertical.color = normalColor;
            }
        }
        else
        {
            _horizontal.color = normalColor;
            _vertical.color = normalColor;
        }
    }
}