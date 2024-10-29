using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    public int startDialogueID;
    public int currentDialogueID;
    private PlayerController _playerController;

    void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    public void Interact()
    {
        if (startDialogueID == _playerController.currentDialogueCounter)
        {
            _playerController._dialogueManager.ShowDialogue(_playerController.currentDialogueCounter.ToString());
            _playerController.maxDialogueCounter = currentDialogueID;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Interact();
        }
    }
}
