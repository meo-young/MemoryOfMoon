using UnityEngine;

public class StartForTest : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Hi");
        DialogueManager.instance.ShowDialogue();
    }

}
