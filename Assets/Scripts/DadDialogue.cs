using UnityEngine;

public class DadDialogue : MonoBehaviour
{
    public float interactDistance = 3f;
    private bool _ketchupDialogueDone = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (DialogueSystem.Instance.IsDialogueActive) return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance))
            {
                if (hit.transform == transform || hit.transform.IsChildOf(transform))
                {
                    if (QuestManager.Instance.CurrentIndex == 1 && !_ketchupDialogueDone)
                        StartKetchupDialogue();
                    else
                        StartGenericDialogue();
                }
            }
        }
    }

    void StartKetchupDialogue()
    {
        _ketchupDialogueDone = true;
        DialogueSystem.Instance.StartDialogue("Dad", new string[] {
            "Oh hey, what's up?"
        });
        // Chain MC response after
        StartCoroutine(ChainDialogue());
    }

    System.Collections.IEnumerator ChainDialogue()
    {
        yield return new WaitUntil(() => !DialogueSystem.Instance.IsDialogueActive);
        DialogueSystem.Instance.StartDialogue("You", new string[] {
            "Hey Dad, have you seen the ketchup anywhere?"
        });
        yield return new WaitUntil(() => !DialogueSystem.Instance.IsDialogueActive);
        DialogueSystem.Instance.StartDialogue("Dad", new string[] {
            "Ketchup? Yeah I made a sandwhich over there on the table this morning."
        });
    }

    void StartGenericDialogue()
    {
        DialogueSystem.Instance.StartDialogue("Dad", new string[] {
            "Hey, I'm watching TV right now!"
        });
    }
}