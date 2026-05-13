using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public string speakerName = "Mom";
    public float interactDistance = 3f;
    private bool _firstDialoguePlayed = false;

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
                    if (QuestManager.Instance.ItemPickedUp)
                        QuestManager.Instance.DeliverToMom();
                    else if (!_firstDialoguePlayed)
                    {
                        _firstDialoguePlayed = true;
                        QuestManager.Instance.StartFirstDialogue();
                    }
                    else
                        QuestManager.Instance.AskForHint();
                }
            }
        }
    }
}