using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    // ����� �ҽ��� ������ ����
    private AudioSource audioSource;

    // ��� ������ ������ ����� Ŭ�� ����
    public AudioClip backgroundMusicClip;

    // �ʱ�ȭ
    private void Awake()
    {
        // ���� �Ŵ����� �ν��Ͻ��� �����ϰ� �ڱ� �ڽ��� ����
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // ����� �ҽ� ������Ʈ �߰�
        audioSource = gameObject.AddComponent<AudioSource>();

        // ��� ���� �ε�
        backgroundMusicClip = Resources.Load<AudioClip>("Bgm/BgMusic");
    }

    // ��� ���� ���
    public void PlayBackgroundMusic(float volume = 1f)
    {
        audioSource.clip = backgroundMusicClip;
        audioSource.loop = true;
        audioSource.volume = volume;
        audioSource.Play();
    }
}
