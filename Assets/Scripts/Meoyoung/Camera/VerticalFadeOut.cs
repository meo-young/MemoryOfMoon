using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerticalFadeOut : MonoBehaviour
{
    public Image blackImage;
    public float fadeDuration = 2.0f;

    private Coroutine fadeCoroutine;

    void Start()
    {
        if (blackImage != null)
        {
            // 시작할 때 이미지의 알파값을 1로 설정합니다.
            Color color = blackImage.color;
            color.a = 1f;
            blackImage.color = color;
        }
        StartFadeOut();
    }

    public void StartFadeOut()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeOutRoutine());
    }

    private IEnumerator FadeOutRoutine()
    {
        yield return new WaitForSeconds(fadeDuration);
        float currentTime = 0f;
        Color color = blackImage.color;

        while (currentTime < fadeDuration)
        {
            currentTime += 0.1f;
            float alpha = Mathf.Lerp(1f, 0f, currentTime / fadeDuration);

            // 이미지의 중앙에서 위아래로 알파값을 조절합니다.
            UpdateImageAlpha(alpha);

            yield return null;
        }

        blackImage.gameObject.SetActive(false);

        // 페이드 아웃이 완료되면 코루틴을 종료합니다.
        fadeCoroutine = null;
    }

    private void UpdateImageAlpha(float alpha)
    {
        if (blackImage != null)
        {
            Color color = blackImage.color;
            RectTransform rt = blackImage.rectTransform;

            Texture2D tex = new Texture2D((int)rt.rect.width, (int)rt.rect.height);
            for (int y = 0; y < tex.height; y++)
            {
                float normalizedY = Mathf.Abs((float)y / tex.height - 0.5f) * 2f;
                float yAlpha = Mathf.Lerp(alpha, 1f, normalizedY);

                for (int x = 0; x < tex.width; x++)
                {
                    tex.SetPixel(x, y, new Color(color.r, color.g, color.b, yAlpha));
                }
            }
            tex.Apply();
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            blackImage.sprite = sprite;
        }
    }
}
