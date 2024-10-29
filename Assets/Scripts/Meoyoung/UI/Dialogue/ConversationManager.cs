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
    public List<float> delay; // �� ���� ������ ���� �ð�

    private GameObject player;
    private PlayerController playerController;
    private int currentLineIndex = 0; // ���� ��� �ε���
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
        dialogueText.text = ""; // �ؽ�Ʈ �ʱ�ȭ
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
            yield return new WaitForSeconds(delay[currentLineIndex]); // ���� �ð� ���
        }

        // ���� ���� �̵�
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
