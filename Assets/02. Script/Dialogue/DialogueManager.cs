using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class CallbackEvent : UnityEvent<UnityAction> { }

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public UnityEvent[] uEvent;
        
    [SerializeField] Sprites[] characterSpriteInfo;                         // 캐릭터별 표정 데이터

    private LoadDialogueDatatable loadDialogue;                             // 대사를 불러오기위한 참조
    private TMP_Text nameText;                                              // 캐릭터이름 패널
    private TMP_Text dialogueText;                                          // 대사 패널
    private Image characterSprite;                                          // 스프라이트 패널
    private GameObject arrow;                                               // 화살표 아이콘
    private WaitForSeconds typingTime = new WaitForSeconds(0.05f);          // 메모리 누수 방지를 위한 사전 선언
    private Coroutine currentCoroutine;                                     // 반복 호출로 인한 메모리 누수 방지
    private bool isFinish;                                                  // E 키 입력받기위한 변수
    private int currentDialogueCounter;                                     // 현재 대사 카운트
    private int eventCounter;                                               // 챕터 이벤트 카운트

    public bool eventFlag;


    private int spriteType => loadDialogue.dialogueInfo[currentDialogueCounter].spriteType;
    private string characterName => loadDialogue.dialogueInfo[currentDialogueCounter].characterName;
    private string dialogue => loadDialogue.dialogueInfo[currentDialogueCounter].text;
    private int nextIndex => loadDialogue.dialogueInfo[currentDialogueCounter-1].nextIndex;
    private int characterType => loadDialogue.dialogueInfo[currentDialogueCounter].characterType;
    private int transitionType => loadDialogue.dialogueInfo[currentDialogueCounter].transitionType;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        // 변수 초기화
        currentDialogueCounter = 0;
        eventCounter = 0;
        isFinish = false;
        eventFlag = false;

        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();
        Image[] images = GetComponentsInChildren<Image>();

        loadDialogue = GetComponent<LoadDialogueDatatable>();

        characterSprite = images[2];
        arrow = images[3].gameObject;
        nameText = texts[0];
        dialogueText = texts[1];


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
            isFinish = false;

            if (transitionType == 1 || transitionType == 2)
            {
                StartCoroutine(EventHandler());
            }

            if (nextIndex == -1)
     
                this.gameObject.SetActive(false);
            else
                ShowDialogue();
        }
    }

    public void ShowDialogue()
    {
        if (!this.gameObject.activeSelf)
            this.gameObject.SetActive(true);

        if(currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        currentCoroutine = StartCoroutine(ActiveDialogue());
    }


    IEnumerator ActiveDialogue()
    {
        arrow.SetActive(false);
        isFinish = false;

        if (transitionType == 0 || transitionType == 2)
        {
            this.gameObject.transform.localScale = Vector3.zero;

            uEvent[eventCounter]?.Invoke();
            ++eventCounter;
            yield return new WaitUntil(() => eventFlag);

            eventFlag = false;
            this.gameObject.transform.localScale = Vector3.one;
        }

        characterSprite.sprite = characterSpriteInfo[characterType].sprites[spriteType];
        nameText.text = characterName; 
        dialogueText.text = "";
        
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
        currentDialogueCounter++;
    }

    IEnumerator EventHandler()
    {
        uEvent[eventCounter]?.Invoke();

        ++eventCounter;

        // UnityEvent가 완료될 때까지 대기
        yield return new WaitUntil(() => eventFlag);
    }

    [System.Serializable]
    public class Sprites
    {
        public Sprite[] sprites;
    }
}
