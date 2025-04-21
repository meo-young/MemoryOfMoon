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

    private AudioSource dialogueAudioSource;                                // 대사 출력 사운드 재생을 위한 오디오 소스

    [HideInInspector] public bool isTransition;                             // 트랜지션 여부


    private string spriteType => loadDialogue.dialogueInfo[currentDialogueCounter].spriteType;
    private string characterName => loadDialogue.dialogueInfo[currentDialogueCounter].characterName;
    private string dialogue => loadDialogue.dialogueInfo[currentDialogueCounter].text;
    private int nextIndex => loadDialogue.dialogueInfo[currentDialogueCounter].nextIndex;
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

        dialogueAudioSource = GetComponent<AudioSource>();

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
        // 대사 출력 사운드 중지
        dialogueAudioSource.Stop();

        // 화살표 활성화
        dialogueFlag = true;
        arrow.SetActive(true);
    }

    IEnumerator TransitionEvent()
    {
        this.gameObject.transform.localScale = Vector3.zero;

        isTransition = false;
        uEvent[eventCounter++]?.Invoke();
        Debug.Log("Wait");
        yield return new WaitUntil(() => isTransition == true);
        Debug.Log("Finishied");
        isTransition = false;
        this.gameObject.transform.localScale = Vector3.one;
        Debug.Log("Scale one");
    }
    #endregion



    private void Update()
    {
        if (!dialogueFlag)
            return;

        if (!Input.GetKeyDown(KeyCode.E)) return;
        dialogueFlag = false;

        if(transitionType is 1 or 2)
        {
            StartCoroutine(TransitionEvent());
        }
            
        if (nextIndex == -1)
        {
            // 플레이어 움직일 수 있게 해야함
            if(MainController.instance)
                MainController.instance.ChangeIdleState();
            // 대화창 스케일 0
            this.gameObject.transform.localScale = Vector3.zero;
                
            if (currentDialogueCounter < loadDialogue.dialogueInfo.Count - 1)
            {
                currentDialogueCounter++;   
            }
        }
        else
        {
            if (currentDialogueCounter < loadDialogue.dialogueInfo.Count - 1)
            {
                currentDialogueCounter++;   
            }
                
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

        Debug.Log("Invoke");

        // Transition Type이 Before
        if (transitionType == 0 || transitionType == 2)
        {
            yield return StartCoroutine(TransitionEvent());
        }

        Debug.Log("Transition Finished");

        // 대사 출력 사운드 재생
        AudioClip clip = SoundManager.instance.sfxs[SFX.DIALOGUE].clips[UnityEngine.Random.Range(0, SoundManager.instance.sfxs[SFX.DIALOGUE].clips.Length)];
        dialogueAudioSource.clip = clip;
        dialogueAudioSource.Play();

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
