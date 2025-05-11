using System.Collections;
using UnityEngine;

public class ObjectFade : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1f; // Fade duration in seconds
    [SerializeField] private float targetAlpha = 0f; // Target alpha value (0 for transparent, 1 for opaque)
    [SerializeField] SpriteRenderer spriteRenderer;

    public void Fade()
    {
        if(spriteRenderer) StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        Color color = spriteRenderer.color;
        float alpha = color.a;
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            color.a = Mathf.Lerp(alpha, targetAlpha, timeElapsed / fadeDuration);
            spriteRenderer.color = color;
            yield return null;
        }
        
    }
}
