using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDialogue : MonoBehaviour
{
    public int startDialogueID;
    public int currentDialogueID;
    public PlayerController _playerController;

    public void ShowDialogue()
    {
        StartCoroutine(ShowDialogueCoroutine());
    }

    IEnumerator ShowDialogueCoroutine()
    {
        yield return new WaitForSeconds(2.0f);
        _playerController._dialogueManager.ShowDialogue(_playerController.currentDialogueCounter.ToString());
        _playerController.maxDialogueCounter = currentDialogueID;
    }
    
}
