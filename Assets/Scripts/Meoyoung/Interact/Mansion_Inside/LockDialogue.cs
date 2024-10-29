using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDialogue : MonoBehaviour
{
    public int startDialogueID;
    public int currentDialogueID;
    public float firstDelay = 1.0f;
    public float secondDelay = 3.0f;
    private PlayerController _playerController;
    public AudioClip lockDoorSound;

    void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    public void Interact()
    {
        if (startDialogueID == _playerController.currentDialogueCounter)
        {
            StartCoroutine(ShowDialogueWithDelay());
        }
    }

    IEnumerator ShowDialogueWithDelay()
    {
        _playerController.ChangeState(_playerController._waitState);
        yield return new WaitForSeconds(firstDelay);
        _playerController._audioSource.clip = lockDoorSound;
        _playerController._audioSource.Play();
        yield return new WaitForSeconds(secondDelay);
        _playerController._dialogueManager.ShowDialogue(_playerController.currentDialogueCounter.ToString());
        _playerController.maxDialogueCounter = currentDialogueID;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Interact();
        }
    }
}
