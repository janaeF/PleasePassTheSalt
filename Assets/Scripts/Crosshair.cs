using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Wait a frame before recreating crosshair
        StartCoroutine(RecreateCrosshair());
    }

    System.Collections.IEnumerator RecreateCrosshair()
    {
        yield return null;
        CreateCrosshair();
    }

    void Start()
    {
        Cursor.visible = false;
        CreateCrosshair();
    }

    void CreateCrosshair()
    {
        if (_crosshairParent != null)
            Destroy(_crosshairParent.gameObject);

        Canvas canvas = FindAnyObjectByType<Canvas>();
        if (canvas == null) return;

        GameObject parent = new GameObject("Crosshair");
        parent.transform.SetParent(canvas.transform, false);
        _crosshairParent = parent.AddComponent<RectTransform>();

        GameObject h = new GameObject("Horizontal");
        h.transform.SetParent(parent.transform, false);
        _horizontal = h.AddComponent<Image>();
        _horizontal.color = normalColor;
        RectTransform hRect = h.GetComponent<RectTransform>();
        hRect.sizeDelta = new Vector2(crosshairSize, lineThickness);

        GameObject v = new GameObject("Vertical");
        v.transform.SetParent(parent.transform, false);
        _vertical = v.AddComponent<Image>();
        _vertical.color = normalColor;
        RectTransform vRect = v.GetComponent<RectTransform>();
        vRect.sizeDelta = new Vector2(lineThickness, crosshairSize);
    }

    void Update()
    {
        if (_crosshairParent == null) return;
        if (_horizontal == null) return;
        if (_vertical == null) return;

        _crosshairParent.position = Input.mousePosition;

        if (Camera.main == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 3f))
        {
            if (hit.transform.GetComponent<NPCDialogue>() != null ||
                hit.transform.GetComponent<QuestItem>() != null)
            {
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

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}