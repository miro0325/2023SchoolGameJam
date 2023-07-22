using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EscFader : MonoBehaviour
{
    public Image fadePanel;
    
    public float fadeDuration = 1.0f;
    

    private void Start()
    {
        
        StartCoroutine(FadeIn());
    }

    

    public void OnEscButtonClick()
    {
        FadeToScene("Title");
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

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }
}