using UnityEngine;

public class MazeBarrier : MonoBehaviour
{
    public static MazeBarrier Instance;
    public string returnScene = "Demo_02";
    public float triggerDistance = 2f;
    private bool _unlocked = false;
    private bool _triggered = false;
    private bool _dialogueCooldown = false;

    void Awake()
    {
        Instance = this;
    }

    public void UnlockBarrier()
    {
        _unlocked = true;
        // Disable collider so player can walk through when unlocked
        Collider col = GetComponent<Collider>();
        if (col != null) col.isTrigger = true;
    }

    void Update()
    {
        if (_triggered) return;
        if (_dialogueCooldown) return;

        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist <= triggerDistance)
        {
            if (!_unlocked)
            {
                _triggered = true;
                StartCoroutine(BlockCooldown());

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
                _triggered = true;
                StartCoroutine(ReturnHome());
            }
        }
    }

    System.Collections.IEnumerator BlockCooldown()
    {
        _dialogueCooldown = true;
        yield return new WaitForSeconds(2f);
        _triggered = false;
        _dialogueCooldown = false;
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