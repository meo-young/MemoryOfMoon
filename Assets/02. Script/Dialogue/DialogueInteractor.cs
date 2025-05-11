using UnityEngine;

public class DialogueInteractor : MonoBehaviour, IInteractable
{
    [SerializeField] private string monologueText;
    [SerializeField] private int dialogueTriggerCriteria;
    
    public void Interact()
    {
        Debug.Log("DialogueCounter : " + DialogueManager.instance.GetCurrentDialogueCounter());
        if (DialogueManager.instance.GetCurrentDialogueCounter() >= dialogueTriggerCriteria)
        {
            DialogueManager.instance.ShowDialogue();
            Destroy(this);
        }
        else {
            MonologueManager.instance.ShowMonologue(monologueText);
        }
    }
}
