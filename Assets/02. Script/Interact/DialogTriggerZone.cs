using UnityEngine;

public class DialogTriggerZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger Entered");
        if (other.CompareTag("Player"))
        {
            DialogueManager.instance.ShowDialogue();
            Destroy(this);
        }
    }
}
