using UnityEngine;
using System.Collections;

public class DialogTimer : MonoBehaviour
{
    // dialogueDelayTime 만큼 대기 후 대화 시작
    [SerializeField] private float dialogueDelayTime;

    private void Start()
    {
        StartCoroutine(DialogueCoroutine());
    }

    private IEnumerator DialogueCoroutine()

    {
        yield return new WaitForSeconds(dialogueDelayTime);
        DialogueManager.instance.ShowDialogue();
        Destroy(this);
    }
}
