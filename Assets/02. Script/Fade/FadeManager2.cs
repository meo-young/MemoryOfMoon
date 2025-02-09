using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Constant;

public class FadeManager2 : MonoBehaviour
{
    public static FadeManager2 instance;

    [SerializeField] FadeType fadeType;

    private Image defaultImage;
    private Coroutine spriteFadeCoroutine;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        defaultImage = GetComponentInChildren<Image>();
    }

    private void Start()
    {
        switch (fadeType)
        {
            case FadeType.Start:
                defaultImage.enabled = false;
                break;
            case FadeType.None:
                break;
        }
    }


    public void FadeOut(Image image = null, Action onComplete = null)
    {
        if (image == null)
            image = defaultImage;


        StartCoroutine(FadeOutCoroutine(image, onComplete));
    }

    public void FadeIn(Image image = null, Action onComplete = null)
    {
        if (image == null)
            image = defaultImage;


        StartCoroutine(FadeInCoroutine(image, onComplete));
    }

    private IEnumerator FadeInCoroutine(Image image, Action onComplete)
    {
        image.enabled = true;

        float elapsedTime = 0f;
        SetAlpha(image, 1f);

        while (elapsedTime < FADE_SCREEN_DURATION)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / FADE_SCREEN_DURATION);
            SetAlpha(image, alpha);
            yield return null;
        }

        // 오차방지 차원에서 투명도를 0으로 설정
        SetAlpha(image, 0f);

        // 이미지 비활성화
        image.enabled = false;

        // fadeDuration만큼의 딜레이
        yield return new WaitForSeconds(FADE_SCREEN_AFTER_DELAY);

        // 콜백함수 호출
        onComplete?.Invoke();
    }

    private IEnumerator FadeOutCoroutine(Image image, Action onComplete)
    {
        image.enabled = true;

        float elapsedTime = 0f;
        SetAlpha(image, 0f);

        while (elapsedTime < FADE_SCREEN_DURATION)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / FADE_SCREEN_DURATION);

            SetAlpha(image, alpha);
            yield return null;
        }

        // 오차방지 차원에서 투명도를 1로 설정
        SetAlpha(image, 1f);


        // fadeDuration만큼의 딜레이
        yield return new WaitForSeconds(FADE_SCREEN_AFTER_DELAY);

        // 콜백함수 호출
        onComplete?.Invoke();
    }
    
    public void FadeOut(SpriteRenderer spriteRenderer)
    {

        spriteFadeCoroutine = StartCoroutine(FadeOutCoroutine(spriteRenderer));
    }

    public void FadeIn(SpriteRenderer spriteRenderer)
    {

        spriteFadeCoroutine = StartCoroutine(FadeInCoroutine(spriteRenderer));
    }

    private IEnumerator FadeInCoroutine(SpriteRenderer spriteRenderer)
    {
        float elapsedTime = 0f;
        //SetAlpha(spriteRenderer, spriteRenderer.color.a);

        while (elapsedTime < FADE_SPRITE_DURATION)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(spriteRenderer.color.a, 0.6f, elapsedTime / FADE_SPRITE_DURATION);
            SetAlpha(spriteRenderer, alpha);
            yield return null;
        }

        // 오차방지 차원에서 투명도를 0으로 설정
        SetAlpha(spriteRenderer, 0.6f);
    }

    private IEnumerator FadeOutCoroutine(SpriteRenderer spriteRenderer)
    {
        float elapsedTime = 0f;
        //SetAlpha(spriteRenderer, spriteRenderer.color.a);

        while (elapsedTime < FADE_SPRITE_DURATION)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(spriteRenderer.color.a, 1f, elapsedTime / FADE_SPRITE_DURATION);
            SetAlpha(spriteRenderer, alpha);
            yield return null;
        }

        // 오차방지 차원에서 투명도를 0으로 설정
        SetAlpha(spriteRenderer, 1f);
    }

    private void SetAlpha(Image image, float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }

    private void SetAlpha(SpriteRenderer spriteRenderer, float alpha)
    {
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }


    enum FadeType
    {
        None,
        Start
    }
}
