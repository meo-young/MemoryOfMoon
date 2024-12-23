using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractWithTransfer2 : MonoBehaviour, IInteractable
{
    public enum Direction //플레이어가 바라보고 있는 방향을 얻기위한 변수
    {
        RIGHT,
        LEFT,
        UP,
        DOWN
    }
    private Animator anim;
    private GameObject player;
    public FirstCameraManager _cameraManager;
    private AudioSource audioSource; // AudioSource 컴포넌트
    private PlayerInventory playerInventory;
    private MonologueManager2 _monologueManager;
    private PlayerController _playerController;
    private Camera _camera;
    private bool playerInRange = false; // 플레이어가 포탈 위에 있는지 여부

    public GameObject arrow_UI;

    [Header("Target")]
    public Direction direction;
    public bool isLocked = false;
    public string keyItemName = "Key";
    public bool stair = false;
    public Transform kanna;
    public bool isInteracted = false;
    public InteractWithTransfer interactTransfer;
    [Header("Sound")]
    public AudioClip openDoorSound; // 방문 여는 사운드
    public AudioClip closeDoorSound; // 방문 닫는 사운드
    [Header("Script")]
    public string doorClosedScript = "문이 잠겨있어.";
    public string doorOpendScript = "문이 열렸어";
    public string disabledScript;

    void Start()
    {
        _camera = FindObjectOfType<Camera>();
        _monologueManager = FindObjectOfType<MonologueManager2>();
        playerInventory = FindObjectOfType<PlayerInventory>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = player.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        _playerController = player.GetComponent<PlayerController>();
        HideUI();
    }

    void Update()
    {
        if (playerInRange)
        {
            if (DirectionUtils.CheckDirection((global::Direction)direction))
            {
                ShowUI();
            }
            else
            {
                HideUI();
            }
        }
        else
        {
            HideUI();
        }
    }

    public void Interact()
    {
        if (DirectionUtils.CheckDirection((global::Direction)direction))
        {
            if ((_playerController.CurrentState == _playerController._idleState || _playerController.CurrentState == _playerController._walkState))
            {
                
                    if (isLocked)
                    {
                        if (playerInventory.HasItem(keyItemName))
                        {
                            UnlockDoor();
                        }
                        else
                        {
                            _monologueManager.ShowMonologue(doorClosedScript);
                        }
                    }
                    else
                    {
                        if (!isInteracted)
                        {
                            StartKannaDialogue(); // 칸나가 유우지에게 사과하는 대사 출력
                            isInteracted = true;
                            interactTransfer.isInteracted = true;
                        }
                        else
                        {
                            _monologueManager.ShowMonologue(disabledScript);
                        }
                    }
                
            }
        }
    }
    public void UnlockDoor()
    {
        isLocked = false;
        _monologueManager.ShowMonologue(doorOpendScript);
    }



    public void StartKannaDialogue() //카메라의 포커스를 칸나에게 맞추고, 대사 출력 후 유우지 2층으로 이동
    {
        BGMManager.Instance.StartFadeIn(1.0f);
        _cameraManager.enabled = false;
        _playerController.ChangeState(_playerController._waitState);
        SetTransparency();
        _playerController.targetNum = 2;
        if (_playerController.targetNum == 1)
        {
            _playerController.transform.position = _playerController.mansion2F_1.position;
        }
        else if (_playerController.targetNum == 2)
        {
            _playerController.transform.position = _playerController.mansion2F_2.position;
        }
        StartCoroutine(StartTransferKanna());
    }
    IEnumerator StartTransferKanna()
    {
        Vector3 targetPosition = new(kanna.position.x, kanna.position.y, -10f);
        yield return new WaitForSeconds(1.0f);
        while (_camera.transform.position != targetPosition) //카메라가 김신 위치로 이동할 때까지 대기
        {
            _camera.transform.position = Vector3.MoveTowards(_camera.transform.position, targetPosition, 100f * Time.deltaTime);
            yield return null; // 다음 프레임까지 대기
        }
        _camera.transform.position = new Vector3(kanna.position.x, kanna.position.y, -10f);

        yield return new WaitForSeconds(1.0f);
        _playerController.maxDialogueCounter = 49;
        _playerController._dialogueManager.ShowDialogue(_playerController.currentDialogueCounter.ToString());

    }

    public void SetTransparency()
    {
        Color color = player.GetComponent<SpriteRenderer>().color;

        color.a = 0f;

        player.GetComponent<SpriteRenderer>().color = color;
    }



    private void ShowUI()
    {
        arrow_UI.SetActive(true);
    }

    private void HideUI()
    {
        arrow_UI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            GetInteractScript();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            RemoveInteractScript();
        }
    }

    private void GetInteractScript()
    {
        _playerController.interactable = this.GetComponent<IInteractable>();
        _playerController.interactRange = true;
    }

    private void RemoveInteractScript()
    {
        _playerController.interactRange = false;
        if (_playerController.interactable != null)
        {
            _playerController.interactable = null;
        }
    }
}
