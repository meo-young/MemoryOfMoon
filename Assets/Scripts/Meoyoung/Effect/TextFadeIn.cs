using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextFadeIn : MonoBehaviour
{
    public float fadeDuration = 1f;  // 페이드 인 완료 시간 (초)
    [SerializeField] TMP_Text text;

    private void Start()
    {
        FadeInText();
    }

    // TMP 텍스트의 투명도를 0에서 1로 천천히 변화시키는 함수
    public void FadeInText()
    {
        // TMP_Text 컴포넌트를 확인
        if (text != null)
        {
            // TMP_Text의 투명도를 조절하는 코루틴 실행
            StartCoroutine(FadeInTextCoroutine(text));
        }
        else
        {
            Debug.LogWarning("Target object does not have a TMP_Text component.");
        }
    }

    // TMP 텍스트의 투명도를 천천히 0에서 1로 변경하는 코루틴
    private IEnumerator FadeInTextCoroutine(TMP_Text tmpText)
    {
        Color color = tmpText.color;
        float startAlpha = color.a;

        // 투명도를 0으로 설정 (완전히 보이지 않게 시작)
        color.a = 0;
        tmpText.color = color;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            color.a = Mathf.Lerp(0, 1, normalizedTime);
            tmpText.color = color;
            yield return null;
        }

        // 마지막으로 완전히 불투명하게 설정
        color.a = 1;
        tmpText.color = color;


        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("TitleScene");
    }
}
