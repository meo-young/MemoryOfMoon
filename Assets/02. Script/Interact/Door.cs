using System;
using UnityEngine;
using static Constant;

public class Door : MonoBehaviour, IInteractable, IInteraction
{
    [SerializeField] DoorState doorState;   // 잠겨 있는지 여부

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
    }
    
    /// <summary>
    /// 문의 상태에 따른 사운드 출력 구현
    /// 유우지가 있다면 Idle 상태로 전환
    /// </summary>
    public void Interact()
    {
        switch (doorState)
        {
            case DoorState.Lock:
                AddressableManager.instance.LoadAudioClip(AUDIO_ADDRESS_LOCKED_DOOR, audioSource);
                break;
            case DoorState.Open:
                AddressableManager.instance.LoadAudioClip(AUDIO_ADDRESS_OPEN_DOOR, audioSource);
                Transfer();
                break;
            case DoorState.Close:
                AddressableManager.instance.LoadAudioClip(AUDIO_ADDRESS_CLOSE_DOOR, audioSource);
                Transfer();
                break;
            default:
                break;
        }

        if (MainController.instance)
        {
            MainController.instance.ChangeIdleState();   
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
    }

    enum DoorState
    {
        Lock,
        Open,
        Close
    }
}
