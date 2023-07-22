using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image fadePanel;
    public Text textToBlink;
    public float fadeDuration = 1.0f;
    public float blinkInterval = 1.0f;

    private void Start()
    {
        StartCoroutine(StartBlinking());
        StartCoroutine(FadeIn());
    }

    private IEnumerator StartBlinking()
    {
        while (true)
        {
            yield return FadeText(0f, 1f, fadeDuration);
            yield return new WaitForSeconds(blinkInterval);
            yield return FadeText(1f, 0f, fadeDuration);
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FadeToScene("SampleScene");
        }
    }

    private IEnumerator FadeIn()
    {
        fadePanel.gameObject.SetActive(true);
        fadePanel.color = Color.black;

        float timer = 0f;

        while (timer < fadeDuration)
        {
            float alpha = 1f - (timer / fadeDuration);
            fadePanel.color = new Color(0f, 0f, 0f, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        fadePanel.gameObject.SetActive(false);
    }

    private IEnumerator FadeOut(string sceneName)
    {
        fadePanel.gameObject.SetActive(true);
        fadePanel.color = new Color(0f, 0f, 0f, 0f);

        float timer = 0f;

        while (timer < fadeDuration)
        {
            float alpha = timer / fadeDuration;
            fadePanel.color = new Color(0f, 0f, 0f, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator FadeText(float startAlpha, float endAlpha, float duration)
    {
        float currentTime = 0f;
        Color startColor = textToBlink.color;
        Color endColor = textToBlink.color;
        startColor.a = startAlpha;
        endColor.a = endAlpha;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, currentTime / duration);
            textToBlink.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        textToBlink.color = endColor;
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }
}