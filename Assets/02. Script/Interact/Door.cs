using System;
using UnityEngine;
using static Constant;

public class Door : MonoBehaviour, IInteractable, IInteraction
{
    [Header("State")]
    [SerializeField] DoorState doorState;   // 잠겨 있는지 여부
    [SerializeField] bool isFlippedX;
    [SerializeField] bool isFlippedY;

    [Header("Item")]
    [SerializeField] ItemInfo itemInfo;

    [Header("Sound")]
    [SerializeField] private AudioClip doorOpenSound;
    [SerializeField] private AudioClip doorCloseSound;
    [SerializeField] private AudioClip doorLockSound;
    
    [Header("Monologue")]
    [SerializeField] string monologueText;
    
    private Transform transferPoint; // 이동할 위치
    private AudioSource audioSource;
    private GameObject arrow;
    private string locationName;

    private void Awake()
    {
        arrow = transform.GetChild(0).gameObject;
        transferPoint = transform.GetChild(1);
        locationName = gameObject.name;
        audioSource = GetComponent<AudioSource>();

        if(arrow.activeSelf)
            arrow.SetActive(false);
        
        isFlippedX = false;
        isFlippedY = false;
    }
    
    /// <summary>
    /// 문의 상태에 따른 사운드 출력 구현
    /// 유우지가 있다면 Idle 상태로 전환
    /// </summary>
    public void Interact()
    {
        if (itemInfo)
        {
            if (InventoryManager.instance.HasItem(itemInfo))
            {
                doorState = DoorState.Open;
                InventoryManager.instance.RemoveItem(itemInfo);
            }
        }
        
        if (MainController.instance)
        {
            MainController.instance.ChangeIdleState();   
        }
        
        switch (doorState)
        {
            case DoorState.Lock:
                PlayDoorSound(DoorState.Lock);
                if(monologueText != "") MonologueManager.instance.ShowMonologue(monologueText);
                break;
            case DoorState.Open:
                PlayDoorSound(DoorState.Open);
                Transfer();
                break;
            case DoorState.Close:
                PlayDoorSound(DoorState.Close);
                Transfer();
                break;
            default:
                break;
        }
    }

    public void CanInteraction()
    {
        arrow.SetActive(true);
    }

    public void StopInteraction()
    {
        arrow.SetActive(false);
    }

    public void UnlockDoor()
    {
        doorState = DoorState.Open;
    }

    private void Transfer()
    {
        if (transferPoint)
        {
            MainController.instance.transform.position = transferPoint.position;
        }

        if (LocationText.instance)
        {
            LocationText.instance.ShowLocationText(locationName);
        }

        if (isFlippedX)
        {
            MainController.instance.FlipX();
        }

        if (isFlippedY)
        {
            MainController.instance.FlipY();
        }
    }

    private void PlayDoorSound(DoorState doorState)
    {
        switch (doorState)
        {
            case DoorState.Close:
                if (doorCloseSound)
                {
                    audioSource.clip = doorCloseSound;
                    audioSource.Play();
                }

                break;
            case DoorState.Open:
                if (doorOpenSound)
                {
                    audioSource.clip = doorOpenSound;
                    audioSource.Play();
                }
                break;
            case DoorState.Lock:
                if (doorLockSound)
                {
                    audioSource.clip = doorLockSound;
                    audioSource.Play();
                }
                break;
        }
    }
}

public enum DoorState
{
    Lock,
    Open,
    Close
}
