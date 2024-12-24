using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
        
    [SerializeField] Sprites[] characterSpriteInfo;                         // ĳ���ͺ� ǥ�� ������

    private List<DialogueInfo> dialogueInfo => loadDialogue.dialogueInfo;   // ĳ�����̸�, ���, ��������Ʈ�� ���� ����
    private LoadDialogueDatatable loadDialogue;                             // ��縦 �ҷ��������� ����
    private TMP_Text nameText;                                              // ĳ�����̸� �г�
    private TMP_Text dialogueText;                                          // ��� �г�
    private Image characterSprite;                                          // ��������Ʈ �г�
    private GameObject arrow;                                               // ȭ��ǥ ������
    private int currentDialogueCounter;                                     // ���� ��� ī��Ʈ
    private WaitForSeconds typingTime = new WaitForSeconds(0.05f);          // �޸� ���� ������ ���� ���� ����
    private Coroutine currentCoroutine;                                     // �ݺ� ȣ��� ���� �޸� ���� ����
    private bool isFinish;                                                  // E Ű �Է¹ޱ����� ����

    private void Awake()
    {
        if (instance == null)
            instance = this;

        loadDialogue = GetComponent<LoadDialogueDatatable>();

        // ���� �Ҵ�
        characterSprite = GetComponentsInChildren<Image>()[2];
        arrow = GetComponentsInChildren<Image>()[3].gameObject;
        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();
        nameText = texts[0];
        dialogueText = texts[1];

        // ĳ���� �̹��� �г� ����

        // ���� �ʱ�ȭ
        currentDialogueCounter = 0;
        isFinish = false;

        // ������Ʈ�� �����ִٸ� ��Ȱ��ȭ
        if(this.gameObject.activeSelf)
            this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!isFinish)
            return;

        if(Input.GetKeyDown(KeyCode.E))
        {
            if (dialogueInfo[currentDialogueCounter].nextIndex == -1)
                this.gameObject.SetActive(false);
            else
                ShowDialogue();
        }
    }

    public void ShowDialogue()
    {
        if(!this.gameObject.activeSelf)
            this.gameObject.SetActive(true);

        if(currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        currentCoroutine = StartCoroutine(ActiveDialogue());
        currentDialogueCounter++;
    }


    IEnumerator ActiveDialogue()
    {
        arrow.SetActive(false);

        isFinish = false;

        int characterType = (int)(LoadDialogueDatatable.CharacterType)Enum.Parse(typeof(LoadDialogueDatatable.CharacterType), dialogueInfo[currentDialogueCounter].characterName.Replace(" ", ""));
        int spriteType = dialogueInfo[currentDialogueCounter].spriteType;
        string dialogue = dialogueInfo[currentDialogueCounter].text;

        characterSprite.sprite = characterSpriteInfo[characterType].sprites[spriteType];
        nameText.text = dialogueInfo[currentDialogueCounter].characterName; 
        dialogueText.text = ""; // �ؽ�Ʈ �ʱ�ȭ
        
        //PlaySound(SoundType.Default);

        for (int i = 0; i < dialogue.Length; i++) //��� ������ ���� Space�ٸ� ������ ��� ��ŵ
        {
            if (Input.GetKey(KeyCode.Space))
            {
                dialogueText.text = dialogue;
                //PlaySound(SoundType.SKIP);
                break;
            }
            dialogueText.text += dialogue[i];
            yield return typingTime;

        }

        isFinish = true;


        //_audioSource.Stop();
        arrow.SetActive(true);
    }

    [System.Serializable]
    public class Sprites
    {
        public Sprite[] sprites;
    }
}
