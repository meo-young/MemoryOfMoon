using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance { get; private set; } // Singleton 인스턴스

    public Image fadeImage; // 페이드 효과를 위한 Image
    public float fadeDuration = 1f; // 페이드 인/아웃 지속 시간
    public float delayBeforeFadeOut = 1f; // 페이드 인 후 페이드 아웃까지의 대기 시간
    private WaitForSeconds oneSecondWait = new(1.0f);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // 씬이 전환되어도 파괴되지 않음
        }
       /* else
        {
            Destroy(gameObject); // 이미 인스턴스가 있다면 새로 생성된 것을 파괴
        }*/
    }

    void Start()
    {
        fadeImage = GameObject.Find("FadeImage").GetComponent<Image>();
        JustFade();
        //fadeImage.gameObject.SetActive(false);
    }

    public void JustFade() //바로 Fade in 되는 효과
    {
        fadeImage.gameObject.SetActive(true);
        Color color = fadeImage.color;
        color.a = 1;
        fadeImage.color = color;
    }

    public void JustFadeOut() //바로 Fade out 되는 효과
    {
        fadeImage.gameObject.SetActive(true);
        Color color = fadeImage.color;
        color.a = 0;
        fadeImage.color = color;
    }

    public void StartFade()
    {
        StartCoroutine(FadeInAndOut());
    }

    public void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        // 페이드 인 로직
        fadeImage.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(0, 1, false)); // 페이드 인 완료 대기
    }

    private IEnumerator FadeOut()
    {
        // 페이드 아웃 로직
        fadeImage.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(1, 0, true)); // 페이드 아웃 완료 대기
        fadeImage.gameObject.SetActive(false);
    }

    /* startAlpha : 시작 투명도
     * endAlpha : 끝 투명도
     * deactivateOnEnd : 연출이 끝나고 비활성화의 여부
     */
    private IEnumerator Fade(float startAlpha, float endAlpha, bool deactivateOnEnd)
    {
        fadeImage.gameObject.SetActive(true);

        Color color = fadeImage.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = endAlpha;
        fadeImage.color = color;

        if (deactivateOnEnd)
        {
            fadeImage.gameObject.SetActive(false); // 투명해지면 비활성화
        }

    }

    private IEnumerator FadeInAndOut()
    {
        yield return oneSecondWait; // 1초 대기
        yield return StartCoroutine(FadeIn()); // 페이드 인 효과 시작 및 완료 대기
        yield return new WaitForSeconds(delayBeforeFadeOut); // 추가 대기 시간
        yield return StartCoroutine(FadeOut()); // 페이드 아웃 효과 시작 및 완료 대기
        yield return oneSecondWait; // 1초 대기
    }

}
