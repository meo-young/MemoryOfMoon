using UnityEngine;
using System.Collections;
using System;


public class Dialogue : MonoBehaviour, IInteractable, IInteraction
{
    private GameObject arrow;
    
    protected void Awake()
    {
        arrow = transform.GetChild(0).gameObject;
        
        if (arrow.activeSelf)
        {
            arrow.SetActive(false);   
        }
    }

    public void Interact()
    {
        DialogueManager.instance.ShowDialogue();
        Destroy(this);
    }
    
    public void CanInteraction()
    {
        arrow.SetActive(true);
    }

    public void StopInteraction()
    {
        arrow.SetActive(false);
    }
}
