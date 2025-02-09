using System;
using UnityEngine;
using static Constant;

public class Door : MonoBehaviour, IInteractable, IInteraction
{
    [SerializeField] DoorState doorState;   // 잠겨 있는지 여부

    private AudioSource audioSource;
    private GameObject arrow;

    private void Awake()
    {
        arrow = transform.GetChild(0).gameObject;
        audioSource = GetComponent<AudioSource>();

        if(arrow.activeSelf)
            arrow.SetActive(false);
    }
    public void Interact()
    {
        if(doorState == DoorState.Lock)
        {
            AddressableManager.instance.LoadAudioClip(AUDIO_ADDRESS_LOCKED_DOOR, audioSource);
        }
        else if(doorState == DoorState.Open)
        {
            AddressableManager.instance.LoadAudioClip(AUDIO_ADDRESS_OPEN_DOOR, audioSource);
        }
        else if(doorState == DoorState.Close)
        {
            AddressableManager.instance.LoadAudioClip(AUDIO_ADDRESS_CLOSE_DOOR, audioSource);
        }

        MainController.instance.ChangeIdleState();
    }

    public void CanInteraction()
    {
        arrow.SetActive(true);
    }

    public void StopInteraction()
    {
        arrow.SetActive(false);
    }

    enum DoorState
    {
        Lock,
        Open,
        Close
    }
}
