using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowRegionName : MonoBehaviour
{
    [SerializeField] private TMP_Text regionNamePanel;
    private float fadeDuration = 1f; // 페이드 인/아웃 지속 시간
    private Coroutine currentCoroutine;

    void Start()
    {
        regionNamePanel = GameObject.FindGameObjectWithTag("Location").GetComponent<TMP_Text>();
        regionNamePanel.text = "";
    }

    public void ShowRegion(string region)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine); // 이전 코루틴 중단
        }
        currentCoroutine = StartCoroutine(Fade(region));
    }

    private IEnumerator Fade(string regionName)
    {
        regionNamePanel.gameObject.SetActive(true);

        Color color = regionNamePanel.color;
        float elapsedTime = 0f;
        color.a = 0.0f;
        regionNamePanel.color = color;
        regionNamePanel.text = "< " +regionName+" >";


        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0.0f, 1.0f, elapsedTime / fadeDuration);
            regionNamePanel.color = color;
            yield return null;
        }

        color.a = 1.0f;
        regionNamePanel.color = color;

        yield return new WaitForSeconds(1);

        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1.0f, 0.0f, elapsedTime / fadeDuration);
            regionNamePanel.color = color;
            yield return null;
        }
        regionNamePanel.text = "";
        regionNamePanel.gameObject.SetActive(false); // 투명해지면 비활성화


    }
}
