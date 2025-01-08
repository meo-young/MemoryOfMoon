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
        doorOpen.stopped += OnTimelineStopped;
    }

    private void OnTimelineStopped(PlayableDirector timeline)
    {
        if (this.eyeCatch == timeline)
        {
            Invoke(nameof(NextDialogue), 0.0f);
        }
        else if (this.doorOpen == timeline)
        {
            //Invoke(nameof(NextDialogue), 0.5f);
            DialogueManager.instance.eventFlag = true;

        }
    }

    public void PlayDoorTimeLine()
    {
        doorOpen.Play();
    }

    private void NextDialogue()
    {
        DialogueManager.instance.ShowDialogue();
    }
}
