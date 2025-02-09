using UnityEngine;
using System;

public class Dialogue : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        DialogueManager.instance.ShowDialogue();
        Destroy(this);
    }
}
