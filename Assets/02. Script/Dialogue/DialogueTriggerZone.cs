using System.Collections;
using UnityEngine;

public class DialogueTriggerZone : MonoBehaviour
{
    [SerializeField] private int dialogueTriggerCriteria = 0; // 이 변수의 값보다 대사 인덱스가 높으면 대사 출력
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (DialogueManager.instance.GetCurrentDialogueCounter() >= dialogueTriggerCriteria)
            {
                StartCoroutine(WaitAndShowDialogue());
            }
        }
    }
    
    private IEnumerator WaitAndShowDialogue()
    {
        MainController.instance.ChangeWaitState();
        yield return new WaitForSeconds(1f);
        DialogueManager.instance.ShowDialogue();
        Destroy(this);
    }
}
