using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public static SceneFader Instance;

    public Image fadeImage;
    public float fadeDuration = 0.5f;

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeRoutine(sceneName));
    }

    IEnumerator FadeRoutine(string sceneName)
    {
        // Fade Out (kararma)
         float t = 0;
         /*while (t < fadeDuration)
         {
             t += Time.deltaTime;
             float a = t / fadeDuration;
             fadeImage.color = new Color(0, 0, 0, a);
             yield return null;
         }*/
        fadeImage.color = new Color(0, 0, 0, 255);

        // Scene yükle
        SceneManager.LoadScene(sceneName);

        yield return null; // 1 frame bekle

        // Fade In (açılma)
        t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float a = 1 - (t / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, a);
            yield return null;
        }
    }
}