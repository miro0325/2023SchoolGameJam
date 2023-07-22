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
        fadeAnimator.SetTrigger("FadeOut"); // ���̵� �ƿ� Ʈ���� ����

        yield return new WaitForSeconds(fadeDuration); // ���̵� �ƿ� �ִϸ��̼� ����ð���ŭ ��ٸ�

        SceneManager.LoadScene(targetSceneName); // �� ��ȯ

        yield return new WaitForSeconds(0.1f); // �ε��� �Ϸ�� ������ ��� ���

        fadeAnimator.SetTrigger("FadeIn"); // ���̵� �� Ʈ���� ����
    }
}