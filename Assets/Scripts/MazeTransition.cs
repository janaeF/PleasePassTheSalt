using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MazeTransition : MonoBehaviour
{
    public string sceneToLoad = "Maze";
    public float interactDistance = 3f;

    void Update()
    {
        if (DialogueSystem.Instance.IsDialogueActive) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance))
            {
                if (hit.transform == transform || hit.transform.IsChildOf(transform))
                {
                    if (QuestManager.Instance.CurrentIndex == 3)
                        SceneManager.LoadScene(sceneToLoad);
                    else
                        DialogueSystem.Instance.StartDialogue("You", new string[] {
                            "Just a locked chest. Maybe I'll need it later..."
                        });
                }
            }
        }
    }
}