using UnityEngine;

public class MazeBarrier : MonoBehaviour
{
    public static MazeBarrier Instance;
    public string returnScene = "Demo_02";
    public float triggerDistance = 2f;
    private bool _unlocked = false;
    private bool _triggered = false;

    void Awake()
    {
        Instance = this;
    }

    public void UnlockBarrier()
    {
        _unlocked = true;
    }

    void Update()
    {
        if (_triggered) return;

        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist <= triggerDistance)
        {
            _triggered = true;

            if (!_unlocked)
            {
                _triggered = false;
                if (DialogueSystem.Instance != null && !DialogueSystem.Instance.IsDialogueActive)
                {
                    DialogueSystem.Instance.StartDialogue("You", new string[] {
                        "I should find the vinegar first before leaving...",
                        "It has to be somewhere in here!"
                    });
                }
            }
            else
            {
                StartCoroutine(ReturnHome());
            }
        }
    }

    System.Collections.IEnumerator ReturnHome()
    {
        if (DialogueSystem.Instance != null)
            yield return new WaitUntil(() => !DialogueSystem.Instance.IsDialogueActive);

        if (QuestManager.Instance != null)
            QuestManager.Instance.ItemCollected(3);

        yield return new WaitForSeconds(0.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(returnScene);
    }
}