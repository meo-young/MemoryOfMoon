using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
    private bool dialogueFlag;                                              // E 키 입력받기위한 변수
    private int currentDialogueCounter;                                     // 현재 대사 카운트
    private int eventCounter;                                               // 챕터 이벤트 카운트

    private event Action OnDialogueStarted;                                 // 대사 시작 이벤트
    private event Action OnDialogueEnded;                                   // 대사 종료 이벤트

    [HideInInspector] public bool isTransition;                             // 트랜지션 여부


    private string spriteType => loadDialogue.dialogueInfo[currentDialogueCounter].spriteType;
    private string characterName => loadDialogue.dialogueInfo[currentDialogueCounter].characterName;
    private string dialogue => loadDialogue.dialogueInfo[currentDialogueCounter].text;
    private int nextIndex => loadDialogue.dialogueInfo[currentDialogueCounter-1].nextIndex;
    private int transitionType => loadDialogue.dialogueInfo[currentDialogueCounter].transitionType;

    #region 초기화
    private void Awake()
    {
        if (instance == null)
            instance = this;

        
        Initalize();
        OnDialogueStarted += HandleDialogueStart;
        OnDialogueEnded += HandleDialogueEnd;
    }

    private void Initalize()
    {
        // 변수 초기화
        currentDialogueCounter = 0;
        eventCounter = 0;
        dialogueFlag = false;

        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();
        Image[] images = GetComponentsInChildren<Image>();

        loadDialogue = GetComponent<LoadDialogueDatatable>();

        characterSprite = images[2];
        arrow = images[3].gameObject;
        nameText = texts[0];
        dialogueText = texts[1];

        // 대화창 스케일 0으로 설정
        this.gameObject.transform.localScale = Vector3.zero;
    }
    #endregion
    
    #region 콜백함수 이벤트
    private void HandleDialogueStart()
    {
        // 화살표 비활성화
        arrow.SetActive(false);
        dialogueFlag = false;

        // 대화 창 초기화
        this.gameObject.transform.localScale = Vector3.one;

        AddressableManager.instance.LoadSprite(characterName + "_" + spriteType, characterSprite);
        nameText.text = System.Text.RegularExpressions.Regex.Replace(characterName, @"\d", "");
        dialogueText.text = "";

    }

    private void HandleDialogueEnd()
    {
        // 화살표 활성화
        dialogueFlag = true;
        arrow.SetActive(true);
        if(currentDialogueCounter < loadDialogue.dialogueInfo.Count-1)
            currentDialogueCounter++;
    }

    IEnumerator TransitionEvent()
    {
        this.gameObject.transform.localScale = Vector3.zero;

        isTransition = false;
        uEvent[eventCounter++]?.Invoke();
        yield return new WaitUntil(() => isTransition == true);
        isTransition = false;
        this.gameObject.transform.localScale = Vector3.one;
    }
    #endregion



    private void Update()
    {
        if (!dialogueFlag)
            return;

        if(Input.GetKeyDown(KeyCode.E))
        {
            dialogueFlag = false;

            if(transitionType == 1 || transitionType == 2)
            {
                StartCoroutine(TransitionEvent());
            }

            if (nextIndex == -1)
            {
                // 플레이어 움직일 수 있게 해야함
                if(MainController.instance != null)
                    MainController.instance.ChangeIdleState();
                // 대화창 스케일 0
                this.gameObject.transform.localScale = Vector3.zero;
            }
            else
                ShowDialogue();
        }
    }

    public void ShowDialogue()
    {
        MainController.instance.ChangeWaitState();

        if(currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        currentCoroutine = StartCoroutine(ActiveDialogue());
    }


    IEnumerator ActiveDialogue()
    {
        // 대사 시작 이벤트 호출
        OnDialogueStarted?.Invoke();


        // Transition Type이 Before
        if (transitionType == 0 || transitionType == 2)
        {
            yield return StartCoroutine(TransitionEvent());
        }

        //대사 나오는 도중 Space바를 누르면 대사 스킵
        for (int i = 0; i < dialogue.Length; i++) 
        {
            if (Input.GetKey(KeyCode.Space))
            {
                dialogueText.text = dialogue;
                break;
            }
            dialogueText.text += dialogue[i];
            yield return typingTime;
        }

        // 대사 종료 이벤트 호출
        OnDialogueEnded?.Invoke();
    }


    [System.Serializable]
    public class Sprites
    {
        public Sprite[] sprites;
    }
}
