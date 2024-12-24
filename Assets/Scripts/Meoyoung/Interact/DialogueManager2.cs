using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager2 : MonoBehaviour
{
    public PlayerController _playerController;
    public GameObject arrow;

    private enum SoundType
    {
        SKIP,
        Default
    }

    [Header("Sound")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip typingSound;
    [SerializeField] private AudioClip skipSound;
    [SerializeField] private float fadeDuration = 0.1f; // 페이드 아웃 시간

    public static DialogueManager2 Instance { get; private set; } // Singleton 인스턴스

    private WaitForSeconds typingTime = new WaitForSeconds(0.05f);


    private void Awake()
    {
        Instance = this;

        /*if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 전환되어도 파괴되지 않음
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 있다면 새로 생성된 것을 파괴
        }*/
    }

    void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void ShowDialogue(string objectID)
    {
        StartCoroutine(ActiveDialogue(objectID));
    }

    // Update is called once per frame
    private IEnumerator ActiveDialogue(string objectID)
    {
        arrow.SetActive(false);

        (string characterName, string sprite, string dialogue) = LoadDialogue.Instance.GetDialogue(objectID);

        _playerController.isDialogue = true;
        _playerController.ChangeState(_playerController._diaState);
        _playerController.charcter.sprite = LoadSprite(characterName, sprite); //캐릭터 이미지 불러오기
        _playerController.characterName.text = characterName; //캐릭터 이름 불러오기
        _playerController.dialogue.text = ""; // 텍스트 초기화
        _playerController.dialoguePanel.SetActive(true);
        _playerController.currentDialogueCounter++;

        PlaySound(SoundType.Default);

        for (int i = 0; i < dialogue.Length; i++) //대사 나오는 도중 Space바를 누르면 대사 스킵
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _playerController.dialogue.text = dialogue;
                PlaySound(SoundType.SKIP);
                break;
            }
            _playerController.dialogue.text += dialogue[i];
            yield return typingTime;

        }

        _audioSource.Stop();
        arrow.SetActive(true);
        _playerController.isDialogue = false;
    }

    private void PlaySound(SoundType _soundType)
    {
        switch (_soundType)
        {
            case SoundType.SKIP:
                _audioSource.clip = skipSound;
                _audioSource.loop = false;
                _audioSource.Play();
                break;
            case SoundType.Default:
                _audioSource.clip = typingSound;
                _audioSource.loop = true;
                _audioSource.Play();
                break;
        }
    }

    public void StopAudioWithFadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        float startVolume = _audioSource.volume;

        while (_audioSource.volume > 0)
        {
            _audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        _audioSource.loop = false;
        _audioSource.Stop();
        _audioSource.volume = startVolume; // 원래 볼륨으로 되돌림
    }

    public Sprite LoadSprite(string folderName, string spriteName)
    {
        string path = "Illustration/" + folderName + "/" + spriteName;
        Debug.Log(path);
        Sprite sprite = Resources.Load<Sprite>(path);
        Debug.Log(sprite);
        if (sprite != null)
        {
            Debug.Log("Sprite loaded successfully from " + path);
        }
        else
        {
            Debug.LogError("Sprite not found at " + path);
        }
        return sprite;
    }
}
