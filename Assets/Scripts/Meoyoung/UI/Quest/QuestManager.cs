using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public GameObject questPanel; // Panel을 포함하는 GameObject
    public Text mainText; // 메인 텍스트
    public Text subText; // 서브 텍스트

    private bool inQuestZone = false;

    private bool isQuestActive = false;
    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = questPanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = questPanel.AddComponent<CanvasGroup>();
        }
        questPanel.SetActive(false);
    }

    void Update()
    {
        if (inQuestZone)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ShowQuestUI("호수를 건넌다", "보트 연료를 입수했다");
            }
        }
        

        if (Input.GetKeyDown(KeyCode.I) && isQuestActive)
        {
            questPanel.SetActive(true);
        }
    }

    public void ShowQuestUI(string main, string sub)
    {
        mainText.text = main;
        subText.text = sub;
        StartCoroutine(AnimateQuestUI());
    }

    private IEnumerator AnimateQuestUI()
    {
        questPanel.SetActive(true);
        canvasGroup.alpha = 0;

        // 등장 애니메이션
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            canvasGroup.alpha = i;
            yield return null;
        }
        canvasGroup.alpha = 1;
        yield return new WaitForSeconds(2);

        // 사라짐 애니메이션
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            canvasGroup.alpha = i;
            yield return null;
        }
        canvasGroup.alpha = 0;
        questPanel.SetActive(false);
        canvasGroup.alpha = 1;

        isQuestActive = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inQuestZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inQuestZone = false;
        }
    }
}
