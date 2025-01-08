using UnityEngine;
using UnityEngine.Playables;

public class Chapter2 : MonoBehaviour
{
    [Header("# ID : 0")]
    [SerializeField] PlayableDirector eyeCatch;

    [Header("# ID : 9")]
    [SerializeField] PlayableDirector doorOpen;

    private void Awake()
    {
        eyeCatch.stopped += OnTimelineStopped;
    }

    private void OnTimelineStopped(PlayableDirector timeline)
    {
        if(this.eyeCatch == timeline)
        {
            Invoke(nameof(NextDialogue), 1.0f);
        }
    }

    private void NextDialogue()
    {
        DialogueManager.instance.ShowDialogue();
    }
}
