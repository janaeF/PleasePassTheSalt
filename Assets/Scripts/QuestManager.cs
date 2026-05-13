using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [Header("Quest Items in Order")]
    public GameObject[] questItems;
    public string[] itemNames = { "salt", "ketchup", "mustard", "vinegar" };

    [Header("Mom Dialogue")]
    public string[] requestDialogue = {
        "Now can you find the ketchup? It should be around the main room somewhere.",
        "I also need the mustard, can you find it for me?",
        "Almost done! Last one, can you find the vinegar?"
    };

    public string[] deliverDialogue = {
        "You found the salt! Thank you so much!",
        "The ketchup! Great job!",
        "Wonderful, you found the mustard!",
        "The vinegar! Now I have everything I need. Thank you!"
    };

    public string[] hintDialogue = {
        "Aw shoot.. I forgot to take the trash out yesterday",
        "Try asking your dad. He's always leaving things around...",
        "I left the mustard near the living room I think...",
        "The vinegar should be on a shelf somewhere..."
    };

    private int _currentIndex = 0;
    private bool _itemPickedUp = false;
    private bool _questComplete = false;

    public int CurrentIndex => _currentIndex;
    public bool ItemPickedUp => _itemPickedUp;
    public bool QuestComplete => _questComplete;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        for (int i = 0; i < questItems.Length; i++)
        {
            questItems[i].SetActive(true);
            questItems[i].GetComponent<QuestItem>().SetCollectable(i == 0);
        }
    }

    public void StartFirstDialogue()
    {
        DialogueSystem.Instance.StartDialogue("Mom", new string[] {
            "Can you help me find the salt? I think I left it somewhere in the house..."
        });
    }

    public void ItemCollected(int itemIndex)
    {
        if (itemIndex != _currentIndex) return;
        _itemPickedUp = true;
        DialogueSystem.Instance.StartDialogue("You", new string[] {
            "I found the " + itemNames[_currentIndex] + "! Let me bring it to Mom."
        });
    }

    public void DeliverToMom()
    {
        if (!_itemPickedUp) return;
        _itemPickedUp = false;

        string deliver = deliverDialogue[_currentIndex];
        _currentIndex++;

        if (_currentIndex >= questItems.Length)
        {
            _questComplete = true;
            DialogueSystem.Instance.StartDialogue("Mom", new string[] {
                deliver,
                "You found everything! Now I can finish cooking. Thank you so much!"
            });
            return;
        }

        DialogueSystem.Instance.StartDialogue("Mom", new string[] {
            deliver,
            requestDialogue[_currentIndex]
        });

        questItems[_currentIndex].GetComponent<QuestItem>().SetCollectable(true);
    }

    public void AskForHint()
    {
        if (_questComplete) return;
        DialogueSystem.Instance.StartDialogue("Mom", new string[] {
            hintDialogue[_currentIndex]
        });
    }
}