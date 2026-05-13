using UnityEngine;
using TMPro;
using System.Collections;

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
        Instance = this;
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (!_dialogueActive) return;

        if (Input.GetMouseButtonDown(0))
        {
            // Double click to close
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
        StopAllCoroutines();
        dialoguePanel.SetActive(true);
        nameText.text = speakerName;
        _dialogueActive = true;
        StartCoroutine(PlayLines(lines));
    }

    IEnumerator PlayLines(string[] lines)
    {
        // Wait one frame so the click that opened dialogue doesn't immediately advance it
        yield return null;

        foreach (string line in lines)
        {
            yield return StartCoroutine(TypeLine(line));

            // Wait for click to advance
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
        dialoguePanel.SetActive(false);
    }
}