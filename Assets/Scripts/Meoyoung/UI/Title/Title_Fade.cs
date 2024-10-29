using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title_Fade : MonoBehaviour 
{

    public Image fadeImage;
    private float time = 0f;
    private float fadeTime = 1f;

    void Start()
    {
        Fade();
    }

    public void Fade()
    {
        StartCoroutine(FadeFlow());
    }   
    IEnumerator FadeFlow()
    {
        fadeImage.gameObject.SetActive(true);
        time = 0f;
        Color alpha = fadeImage.color;
        
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / fadeTime;
            alpha.a = Mathf.Lerp(0, 1, time);
            fadeImage.color = alpha;
            yield return null;
        }

        time = 0f;
        yield return new WaitForSeconds(1f);

        while (alpha.a > 0f)
        {
            time += Time.deltaTime / fadeTime;
            alpha.a = Mathf.Lerp(1, 0, time);
            fadeImage.color = alpha;
            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
        yield return null;
    }
}