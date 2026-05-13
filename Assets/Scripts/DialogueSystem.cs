using UnityEngine;
using UnityEngine.UI;
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
    private string _currentFullText = "";
    private Coroutine _typingCoroutine;
    private bool _dialogueActive = false;

    public static DialogueSystem Instance;

    void Awake()
    {
        Instance = this;
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (_dialogueActive && Input.GetMouseButtonDown(0))
        {
            if (_isTyping)
                _skipTyping = true;
            else
                CloseDialogue();
        }
    }

    public void StartDialogue(string speakerName, string[] lines)
    {
        dialoguePanel.SetActive(true);
        nameText.text = speakerName;
        _dialogueActive = true;
        StartCoroutine(PlayLines(lines));
    }

    IEnumerator PlayLines(string[] lines)
    {
        foreach (string line in lines)
        {
            yield return StartCoroutine(TypeLine(line));
            // Wait for click before next line
            yield return new WaitUntil(() => !_isTyping && Input.GetMouseButtonDown(0));
        }
        CloseDialogue();
    }

    IEnumerator TypeLine(string line)
    {
        _isTyping = true;
        _skipTyping = false;
        _currentFullText = line;
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

    void CloseDialogue()
    {
        _dialogueActive = false;
        dialoguePanel.SetActive(false);
        if (_typingCoroutine != null)
            StopCoroutine(_typingCoroutine);
    }
}