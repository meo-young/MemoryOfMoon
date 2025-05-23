using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Constant;

public class MonologueManager : MonoBehaviour
{
    public static MonologueManager instance;

    private GameObject monologuePanel;                                          // 독백이 표시될 패널
    private TMP_Text monologueText;                                             // 독백을 표시할 텍스트
    private WaitForSeconds typingTime = new WaitForSeconds(MONOLOGUE_TYPING_TIME);              // 모노로그 출력 딜레이를 위한 시간 변수

    #region Singleton + Initalize
    private void Awake()
    {
        if (instance == null)
            instance = this;

        monologuePanel = this.gameObject;
        monologueText = GetComponentInChildren<TMP_Text>();

        monologuePanel.GetComponent<RectTransform>().localScale = Vector3.zero;
    }
    #endregion

    public void ShowMonologue(string inMonologueText)
    {
        // 대화 상태로 전환
        if(MainController.instance) MainController.instance.ChangeWaitState();
        
        // 대화 패널 스케일 1로 변경
        monologuePanel.GetComponent<RectTransform>().localScale = Vector3.one;
        
        // 대화 시작
        StartCoroutine(ActiveMonologue(inMonologueText));
    }

    private IEnumerator ActiveMonologue(string inMonologueText)
    {
        string dialogue = inMonologueText;

        monologueText.text = "";
        monologuePanel.transform.position = GameManager.instance.player.transform.position + new Vector3(0f, 0.75f, 0f);

        foreach (char letter in dialogue)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                monologueText.text = dialogue;
                break;
            }
            monologueText.text += letter;
            yield return typingTime;
        }

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                MainController.instance.ChangeIdleState();
                monologuePanel.GetComponent<RectTransform>().localScale = Vector3.zero;
                break;
            }
            yield return null;
        }
    }
}
