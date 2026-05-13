using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;
    private Image _fadeImage;
    public float fadeDuration = 1f;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        GameObject imageObj = new GameObject("FadeImage");
        imageObj.transform.SetParent(transform, false);
        _fadeImage = imageObj.AddComponent<Image>();
        _fadeImage.color = new Color(0, 0, 0, 0);
        RectTransform rt = imageObj.GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FadeFromBlack(fadeDuration));
    }

    public IEnumerator FadeToBlack(float duration)
    {
        yield return StartCoroutine(Fade(0, 1, duration));
    }

    public IEnumerator FadeFromBlack(float duration)
    {
        yield return StartCoroutine(Fade(1, 0, duration));
    }

    IEnumerator Fade(float from, float to, float duration)
    {
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            _fadeImage.color = new Color(0, 0, 0, Mathf.Lerp(from, to, t / duration));
            yield return null;
        }
        _fadeImage.color = new Color(0, 0, 0, to);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}