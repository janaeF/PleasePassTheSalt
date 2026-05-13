using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string gameSceneName = "Demo_02";

    [Header("Optional Transition")]
    [SerializeField] private CanvasGroup fadePanel;
    [SerializeField] private float fadeDuration = 0.75f;

    private bool isTransitioning = false;

    public void PlayGame()
    {
        if (isTransitioning)
        {
            return;
        }

        StartCoroutine(LoadGameScene());
    }

    public void QuitGame()
    {
        if (isTransitioning)
        {
            return;
        }
        

        Debug.Log("Quit game.");

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private System.Collections.IEnumerator LoadGameScene()
    {
        isTransitioning = true;

        if (fadePanel != null)
        {
            yield return StartCoroutine(FadeOut());
        }

        SceneManager.LoadScene(gameSceneName);
    }

    private System.Collections.IEnumerator FadeOut()
    {
        float timer = 0f;

        fadePanel.gameObject.SetActive(true);
        fadePanel.alpha = 0f;
        fadePanel.blocksRaycasts = true;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadePanel.alpha = Mathf.Clamp01(timer / fadeDuration);
            yield return null;
        }

        fadePanel.alpha = 1f;
    }
}

