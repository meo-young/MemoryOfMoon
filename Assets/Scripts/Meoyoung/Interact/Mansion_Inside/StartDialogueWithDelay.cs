using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogueWithDelay : MonoBehaviour
{
    public int startDialogueID;
    public int currentDialogueID;
    public float delay = 2.0f;
    private PlayerController _playerController;

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
        yield return new WaitForSeconds(delay);
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
