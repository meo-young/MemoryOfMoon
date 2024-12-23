using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferScene : MonoBehaviour, IInteractable
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
    private PlayerInventory playerInventory;
    private MonologueManager2 _monologueManager;
    private PlayerController _playerController;
    private bool playerInRange = false; // 플레이어가 포탈 위에 있는지 여부
    private bool isMonologue = false;

    public GameObject arrow_UI;

    [Header("Target")]
    public Direction direction;
    public bool isLocked = false;
    public string keyItemName = "Key";
    public bool stair = false;

    [Header("Script")]
    public string doorClosedScript = "문이 잠겨있어.";
    public string doorOpendScript = "문이 열렸어";

    void Start()
    {
        _monologueManager = FindObjectOfType<MonologueManager2>();
        playerInventory = FindObjectOfType<PlayerInventory>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = player.GetComponent<Animator>();
        _playerController = player.GetComponent<PlayerController>();
        HideUI();

       /* playerInventory.AddItem("Outside");
        playerInventory.AddItem("KannaInteract");*/
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
    }

    public void Interact()
    {
        if (Input.GetKeyDown((KeyCode)CustomKey.Interact) && (_playerController.CurrentState == _playerController._idleState || _playerController.CurrentState == _playerController._walkState))
        {
            if (!isMonologue)
            {
                if (isLocked)
                {
                    if (playerInventory.HasItem(keyItemName))
                    {
                        TransformWithSound(); // 방문 사운드 재생
                    }
                    else
                    {
                        _monologueManager.ShowMonologue(doorClosedScript);
                        isMonologue = true;
                    }
                }
            }
            else
            {
                isMonologue = false;
            }
        }
    }
    public void UnlockDoor()
    {
        isLocked = false;
        isMonologue = true;
        _monologueManager.ShowMonologue(doorOpendScript);
    }

    public void TransformWithSound()
    {
        StartCoroutine(StartLoadScene());
    }

    IEnumerator StartLoadScene()
    {
        _playerController.ChangeState(_playerController._waitState);
        FadeManager.Instance.StartFadeIn();
        _playerController.mansionInside++;
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("MansionOutScene");
    }

    private bool CheckDirection()
    {
        if (direction == Direction.RIGHT)
        {
            if (anim.GetFloat("DirX") == 1)
            {
                return true;
            }
        }
        else if (direction == Direction.LEFT)
        {
            if (anim.GetFloat("DirX") == -1)
            {
                return true;
            }
        }
        else if (direction == Direction.UP)
        {
            if (anim.GetFloat("DirY") == 1)
            {
                return true;
            }
        }
        else if (direction == Direction.DOWN)
        {
            if (anim.GetFloat("DirY") == -1)
            {
                return true;
            }
        }
        return false;
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
            if (CheckDirection())
            {
                GetInteractScript();
            }
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

