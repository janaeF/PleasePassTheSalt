using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public Button playAgainButton;
    public Button quitButton;
    public AudioClip buttonSound;
    private AudioSource _audioSource;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _audioSource = gameObject.AddComponent<AudioSource>();

        if (messageText != null)
            messageText.text = "Mom finished cooking!\nThanks for your help!";

        if (playAgainButton != null)
            playAgainButton.onClick.AddListener(PlayAgain);

        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlaySound();
            PlayAgain();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlaySound();
            QuitGame();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            PlaySound();
            GoToMenu();
        }
    }

    void PlaySound()
    {
        if (buttonSound != null && _audioSource != null)
            _audioSource.PlayOneShot(buttonSound);
    }

    void PlayAgain()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (QuestManager.Instance != null)
            Destroy(QuestManager.Instance.gameObject);

        if (DialogueSystem.Instance != null)
            Destroy(DialogueSystem.Instance.gameObject);

        SceneManager.LoadScene("Demo_02");
    }

    void GoToMenu()
    {
        if (QuestManager.Instance != null)
            Destroy(QuestManager.Instance.gameObject);

        if (DialogueSystem.Instance != null)
            Destroy(DialogueSystem.Instance.gameObject);

        SceneManager.LoadScene("Menu");
    }

    void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}