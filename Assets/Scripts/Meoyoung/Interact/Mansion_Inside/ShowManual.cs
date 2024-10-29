using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowManual : MonoBehaviour
{
    [SerializeField] private GameObject manualPanel;
    [SerializeField] private Image manual;
    [SerializeField] private float fadeDuration = 1f; // 페이드 인/아웃 지속 시간
    [SerializeField] private FirstDialogue firstDialogue;
    [SerializeField] private TMP_Text continueText;
    private PlayerController _playerController;



    void Start()
    {
        manualPanel.SetActive(false);
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (_playerController.mansionInside == 0)
        {
            StartCoroutine(ShowManualCoroutine());
        }

        if (continueText.gameObject.activeSelf)
        {
            continueText.gameObject.SetActive(false);
        }
    }

    IEnumerator ShowManualCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        manualPanel.SetActive(true);
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        // 페이드 인 로직
        manual.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(0, 1, false)); // 페이드 인 완료 대기
        continueText.gameObject.SetActive(true);
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                continueText.gameObject.SetActive(false);
                break;
            }
            yield return null;
        }

        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        yield return StartCoroutine(Fade(1, 0, true)); // 페이드 아웃 완료 대기
        manual.gameObject.SetActive(false);
         yield return new WaitForSeconds(1.0f);
        firstDialogue.ShowDialogue();
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, bool deactivateOnEnd)
    {
        manual.gameObject.SetActive(true);

        Color color = manual.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            manual.color = color;
            yield return null;
        }

        color.a = endAlpha;
        manual.color = color;

        if (deactivateOnEnd)
        {
            manual.gameObject.SetActive(false); // 투명해지면 비활성화
        }

    }
}
