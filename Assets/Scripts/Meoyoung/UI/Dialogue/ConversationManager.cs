using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ConversationManager : MonoBehaviour
{
    [Header("Canvas")]
    public GameObject UI;
    public GameObject arrow_UI;
    public Image character;

    [Header("Dialog")]
    public TMP_Text dialogueText;
    public List<string> dialogueLines;
    public List<Sprite> dialogImages;
    public List<float> delay; // 각 글자 사이의 지연 시간

    private GameObject player;
    private PlayerController playerController;
    private int currentLineIndex = 0; // 현재 대사 인덱스
    private bool inZone = false;
    private bool isTalking = false;
    private bool isFinish = false;
    private bool isStart = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        dialogueText.text = "";
    }


    void Update()
    {
        if (inZone)
        {
            if (!isTalking)
            {
                arrow_UI.SetActive(true);
                if (Input.GetKeyDown((KeyCode)CustomKey.Interact))
                {
                    if (isFinish)
                    {
                        if(UI != null)
                        {
                            playerController.ChangeState(playerController._idleState);
                            InactiveCanvas();
                            this.gameObject.SetActive(false);
                        }
                    }
                    else
                    {
                        StartCoroutine(ActivateCanvas());
                    }
                }
            }
            else
            {
                arrow_UI.SetActive(false);
            }
        }
    }

   

    private IEnumerator ActivateCanvas()
    {
        isTalking = true;
        dialogueText.text = ""; // 텍스트 초기화
        character.sprite = dialogImages[currentLineIndex];
        UI.gameObject.SetActive(true);
        if (!isStart)
        {
            isStart = true;
            playerController.ChangeState(playerController._waitState);
            yield return new WaitForSeconds(1);
        }
        foreach (char letter in dialogueLines[currentLineIndex].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(delay[currentLineIndex]); // 지연 시간 대기
        }

        // 다음 대사로 이동
        isTalking = false;
        if (currentLineIndex < dialogueLines.Count -1)
        {
            currentLineIndex++;
        }
        else
        {
            isFinish = true;
        }
    }

    private void InactiveCanvas()
    {
        UI.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Debug.Log("conversation In");
            inZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Debug.Log("conversation Out");
            InactiveCanvas();
            inZone = false;
        }
    }
}
