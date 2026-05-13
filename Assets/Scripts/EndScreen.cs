using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public Button playAgainButton;

    void Start()
    {
        if (messageText != null)
            messageText.text = "Mom finished cooking!\nThanks for your help!";

        if (playAgainButton != null)
            playAgainButton.onClick.AddListener(PlayAgain);
    }

    void PlayAgain()
    {
        if (QuestManager.Instance != null)
            Destroy(QuestManager.Instance.gameObject);

        if (DialogueSystem.Instance != null)
            Destroy(DialogueSystem.Instance.gameObject);

        SceneManager.LoadScene("Demo_02");
    }
}