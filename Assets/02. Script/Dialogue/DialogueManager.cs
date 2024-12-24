using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
        
    [SerializeField] Sprites[] characterSpriteInfo;                         // 캐릭터별 표정 데이터

    private List<DialogueInfo> dialogueInfo => loadDialogue.dialogueInfo;   // 캐릭터이름, 대사, 스프라이트를 가진 변수
    private LoadDialogueDatatable loadDialogue;                             // 대사를 불러오기위한 참조
    private TMP_Text nameText;                                              // 캐릭터이름 패널
    private TMP_Text dialogueText;                                          // 대사 패널
    private Image characterSprite;                                          // 스프라이트 패널
    private GameObject arrow;                                               // 화살표 아이콘
    private int currentDialogueCounter;                                     // 현재 대사 카운트
    private WaitForSeconds typingTime = new WaitForSeconds(0.05f);          // 메모리 누수 방지를 위한 사전 선언
    private Coroutine currentCoroutine;                                     // 반복 호출로 인한 메모리 누수 방지
    private bool isFinish;                                                  // E 키 입력받기위한 변수

    private void Awake()
    {
        if (instance == null)
            instance = this;

        loadDialogue = GetComponent<LoadDialogueDatatable>();

        // 참조 할당
        characterSprite = GetComponentsInChildren<Image>()[2];
        arrow = GetComponentsInChildren<Image>()[3].gameObject;
        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();
        nameText = texts[0];
        dialogueText = texts[1];

        // 변수 초기화
        currentDialogueCounter = 0;
        isFinish = false;

        // 오브젝트가 켜져있다면 비활성화
        if(this.gameObject.activeSelf)
            this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!isFinish)
            return;

        if(Input.GetKeyDown(KeyCode.E))
        {
            if (dialogueInfo[currentDialogueCounter-1].nextIndex == -1)
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
        dialogueText.text = ""; // 텍스트 초기화
        
        //PlaySound(SoundType.Default);

        for (int i = 0; i < dialogue.Length; i++) //대사 나오는 도중 Space바를 누르면 대사 스킵
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
