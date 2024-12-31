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

    private LoadDialogueDatatable loadDialogue;                             // 대사를 불러오기위한 참조
    private TMP_Text nameText;                                              // 캐릭터이름 패널
    private TMP_Text dialogueText;                                          // 대사 패널
    private Image characterSprite;                                          // 스프라이트 패널
    private GameObject arrow;                                               // 화살표 아이콘
    private WaitForSeconds typingTime = new WaitForSeconds(0.05f);          // 메모리 누수 방지를 위한 사전 선언
    private Coroutine currentCoroutine;                                     // 반복 호출로 인한 메모리 누수 방지
    private bool isFinish;                                                  // E 키 입력받기위한 변수
    private int currentDialogueCounter;                                     // 현재 대사 카운트


    private int spriteType => loadDialogue.dialogueInfo[currentDialogueCounter].spriteType;
    private string characterName => loadDialogue.dialogueInfo[currentDialogueCounter].characterName;
    private string dialogue => loadDialogue.dialogueInfo[currentDialogueCounter].text;
    private int nextIndex => loadDialogue.dialogueInfo[currentDialogueCounter-1].nextIndex;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        // 변수 초기화
        currentDialogueCounter = 0;
        isFinish = false;

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
            if (nextIndex == -1)
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
    }


    IEnumerator ActiveDialogue()
    {
        arrow.SetActive(false);

        isFinish = false;

        int characterType = (int)(CharacterType)Enum.Parse(typeof(CharacterType), characterName.Replace(" ", ""));

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

        // 대화의 한 사이클이 끝나면 Counter 증가
        currentDialogueCounter++;
    }

    [System.Serializable]
    public class Sprites
    {
        public Sprite[] sprites;
    }

    public enum CharacterType
    {
        엑스트라1 = 0,
        엑스트라2 = 1,
        엑스트라3 = 2,
        츠네모리유우지 = 3,
        스즈키토우마 = 4,
        후케토우지 = 5,
        후지다나카 = 6,
        코즈키료고 = 7,
        기노자사이고 = 8,
        토마코자부로 = 9

    }
}
