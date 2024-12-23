using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonologueManager : MonoBehaviour
{
    public static MonologueManager instance;

    private Dictionary<string, string> monologues => loadMonoData.monologues;   // 독백 데이터
    private LoadMonologueDatatable loadMonoData;                                // 독백 데이터를 얻어오기위한 변수
    private GameObject monologuePanel;                                          // 독백이 출력될 패널
    private TMP_Text monologueText;                                             // 독백을 표시할 텍스트
    private WaitForSeconds typingTime = new WaitForSeconds(0.05f);              // 메모리 누수 방지를 위한 사전 선언

    #region Singleton + Initalize
    private void Awake()
    {
        if (instance == null)
            instance = this;

        loadMonoData = GetComponent<LoadMonologueDatatable>();
        monologuePanel = this.gameObject;
        monologueText = GetComponentInChildren<TMP_Text>();

        if (monologuePanel.activeSelf)
            monologuePanel.SetActive(false);
    }
    #endregion

    public void ShowMonologue(string _objectName, Action action)
    {
        monologuePanel.SetActive(true);

        StartCoroutine(ActiveMonologue(_objectName, action));
    }

    private IEnumerator ActiveMonologue(string _objectName, Action action)
    {
        string dialogue = monologues[_objectName];

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
                action();
                monologuePanel.SetActive(false);
                break;
            }
            yield return null;
        }
    }
}
