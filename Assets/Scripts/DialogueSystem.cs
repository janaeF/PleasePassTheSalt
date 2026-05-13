using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class DialogueSystem : MonoBehaviour
{
    [Header("UI")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;

    [Header("Settings")]
    public float typingSpeed = 0.05f;

    private bool _isTyping = false;
    private bool _skipTyping = false;
    private bool _dialogueActive = false;
    private bool _waitingForClick = false;
    private float _lastClickTime = 0f;
    private float _doubleClickThreshold = 0.3f;

    public bool IsDialogueActive => _dialogueActive;

    public static DialogueSystem Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (!_dialogueActive) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (Time.time - _lastClickTime < _doubleClickThreshold)
            {
                CloseDialogue();
                return;
            }
            _lastClickTime = Time.time;

            if (_isTyping)
                _skipTyping = true;
            else if (_waitingForClick)
                _waitingForClick = false;
        }
    }

    public void StartDialogue(string speakerName, string[] lines)
    {
        if (dialoguePanel == null) return;
        StopAllCoroutines();
        dialoguePanel.SetActive(true);
        nameText.text = speakerName;
        _dialogueActive = true;
        StartCoroutine(PlayLines(lines));
    }

    IEnumerator PlayLines(string[] lines)
    {
        yield return null;

        foreach (string line in lines)
        {
            yield return StartCoroutine(TypeLine(line));
            _waitingForClick = true;
            yield return new WaitUntil(() => !_waitingForClick);
        }
    }

    IEnumerator TypeLine(string line)
    {
        _isTyping = true;
        _skipTyping = false;
        dialogueText.text = "";

        foreach (char c in line)
        {
            if (_skipTyping)
            {
                dialogueText.text = line;
                break;
            }
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        _isTyping = false;
    }

    public void CloseDialogue()
    {
        _dialogueActive = false;
        _waitingForClick = false;
        StopAllCoroutines();
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }
}