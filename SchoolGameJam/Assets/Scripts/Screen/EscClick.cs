using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscClick : MonoBehaviour
{
    public Animator fadeAnimator;
    public float fadeDuration = 1.0f;
    private string targetSceneName = "Title";

    public void OnEscClick()
    {
        StartCoroutine(FadeAndLoadScene());
    }

    IEnumerator FadeAndLoadScene()
    {
        fadeAnimator.SetTrigger("FadeOut"); // 페이드 아웃 트리거 설정

        yield return new WaitForSeconds(fadeDuration); // 페이드 아웃 애니메이션 재생시간만큼 기다림

        SceneManager.LoadScene(targetSceneName); // 씬 전환

        yield return new WaitForSeconds(0.1f); // 로딩이 완료될 때까지 잠시 대기

        fadeAnimator.SetTrigger("FadeIn"); // 페이드 인 트리거 설정
    }
}