using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public string speakerName = "Mom";
    public float interactDistance = 3f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance))
            {
                if (hit.transform == transform || hit.transform.IsChildOf(transform))
                {
                    if (QuestManager.Instance.ItemPickedUp)
                        QuestManager.Instance.DeliverToMom();
                    else if (QuestManager.Instance.CurrentIndex == 0 && !QuestManager.Instance.QuestComplete)
                        QuestManager.Instance.StartFirstDialogue();
                    else
                        QuestManager.Instance.AskForHint();
                }
            }
        }
    }
}