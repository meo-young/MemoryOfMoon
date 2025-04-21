using UnityEngine;
using System.Collections;
using System;


public class Dialogue : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        DialogueManager.instance.ShowDialogue();
        Destroy(this);
    }
}
