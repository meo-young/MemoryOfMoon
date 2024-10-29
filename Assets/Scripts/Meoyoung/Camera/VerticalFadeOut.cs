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
            // ������ �� �̹����� ���İ��� 1�� �����մϴ�.
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

            // �̹����� �߾ӿ��� ���Ʒ��� ���İ��� �����մϴ�.
            UpdateImageAlpha(alpha);

            yield return null;
        }

        blackImage.gameObject.SetActive(false);

        // ���̵� �ƿ��� �Ϸ�Ǹ� �ڷ�ƾ�� �����մϴ�.
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
