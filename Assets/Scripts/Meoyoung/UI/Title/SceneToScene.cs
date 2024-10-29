using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneToScene : MonoBehaviour 
{
    public string nextSceneName = "MansionScene";
    public Image fadeImage1;
    public Image fadeImage2;
    private float time = 0f;
    private float fadeTime = 1f;

    public void Start()
    {
        FadeIn();
        FadeOut();
        Invoke("LoadScene", 14f);
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInn());
    }   
    public void FadeOut()
    {
        StartCoroutine(FadeOutt());
    }   
    IEnumerator FadeOutt()
    {
        yield return new WaitForSeconds(12f);

        fadeImage2.gameObject.SetActive(true);
        time = 0f;
        Color alpha = fadeImage2.color;
        
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / fadeTime;
            alpha.a = Mathf.Lerp(0, 1, time);
            fadeImage2.color = alpha;
            yield return null;
        }

        yield return null;
    }

    IEnumerator FadeInn()
    {
        yield return new WaitForSeconds(6.5f);
        fadeImage1.gameObject.SetActive(true);
        time = 0f;
        Color alpha = fadeImage1.color;
        
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / fadeTime;
            alpha.a = Mathf.Lerp(1, 0, time);
            fadeImage1.color = alpha;
            yield return null;
        }

        fadeImage1.gameObject.SetActive(false);
        yield return null;
    }
}