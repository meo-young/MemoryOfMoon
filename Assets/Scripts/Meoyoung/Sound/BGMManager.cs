using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance { get; private set; } // Singleton 인스턴스

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip mansionInside;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 전환되어도 파괴되지 않음
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 있다면 새로 생성된 것을 파괴
        }
    }

    void Start()
    {
        // AudioSource 컴포넌트를 가져옵니다.
        audioSource = GetComponent<AudioSource>();
    }
    public void StartFadeOut(float duration)
    {
        StartCoroutine(FadeOut(duration));
    }

    public void StartFadeIn(float duration)
    {
        StartCoroutine(FadeIn(duration));
    }

    private IEnumerator FadeOut(float duration)
    {
        audioSource.volume = 0f;
        audioSource.Play();

        while (audioSource.volume < 1.0f)
        {
            audioSource.volume += Time.deltaTime / duration;

            yield return null;
        }

        audioSource.volume = 1f; // 볼륨을 1로 고정
    }

    private IEnumerator FadeIn(float duration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / duration;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume; // 초기 볼륨으로 복구 (필요한 경우)
    }
}
