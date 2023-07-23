using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image fadePanel;
    public Text textToBlink;
    public AudioSource audioSource; // 추가: 페이드 아웃할 사운드를 가진 오디오 소스
    public float fadeDuration = 1.0f;
    public float blinkInterval = 1.0f;

    private bool canFadeOut = false; // 추가: 화면을 터치할 때 페이드 아웃을 시작하기 위한 플래그

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
        if (Input.GetMouseButtonDown(0) && !canFadeOut)
        {
            canFadeOut = true;
            StartCoroutine(FadeOutAndLoadScene("SampleScene"));
        }
    }

    private IEnumerator FadeIn()
    {
        fadePanel.gameObject.SetActive(true);
        fadePanel.color = Color.black;

        float timer = 0f;

        // 추가: 처음 등장할 때의 사운드 볼륨 저장
        float initialVolume = audioSource.volume;

        while (timer < fadeDuration)
        {
            float alpha = 1f - (timer / fadeDuration);
            fadePanel.color = new Color(0f, 0f, 0f, alpha);
            timer += Time.deltaTime;

            // 추가: 사운드 볼륨 조절 (처음 등장할 때는 초기 볼륨 유지)
            

            yield return null;
        }

        fadePanel.gameObject.SetActive(false);
    }

    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        fadePanel.gameObject.SetActive(true);
        fadePanel.color = new Color(0f, 0f, 0f, 0f);

        float timer = 0f;

        while (timer < fadeDuration)
        {
            float alpha = timer / fadeDuration;
            fadePanel.color = new Color(0f, 0f, 0f, alpha);
            timer += Time.deltaTime;

            // 추가: 사운드 볼륨 조절
            if (audioSource != null)
            {
                float volume = 1f - alpha; // 볼륨은 알파 값의 반대로 설정
                audioSource.volume = volume;
            }

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
}
