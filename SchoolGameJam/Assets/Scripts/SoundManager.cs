using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    // 오디오 소스를 저장할 변수
    private AudioSource audioSource;

    // 배경 음악을 저장할 오디오 클립 변수
    public AudioClip backgroundMusicClip;

    // 초기화
    private void Awake()
    {
        // 사운드 매니저의 인스턴스를 생성하고 자기 자신을 저장
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // 오디오 소스 컴포넌트 추가
        audioSource = gameObject.AddComponent<AudioSource>();

        // 배경 음악 로드
        backgroundMusicClip = Resources.Load<AudioClip>("Bgm/BgMusic");
    }

    // 배경 음악 재생
    public void PlayBackgroundMusic(float volume = 1f)
    {
        audioSource.clip = backgroundMusicClip;
        audioSource.loop = true;
        audioSource.volume = volume;
        audioSource.Play();
    }
}
