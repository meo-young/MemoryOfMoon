using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Playables;

public class Chapter2 : MonoBehaviour
{
    [Header("# ID : 0 아이캐치")]
    [SerializeField] PlayableDirector eyeCatch;

    private void Awake()
    {
        StartCoroutine(TimeLineCoroutine(eyeCatch, () => {DialogueManager.instance.ShowDialogue();}));
    }
    
    [Header("# ID : 9 토우마 걸어오기")]
    [SerializeField] PlayableDirector doorOpen;

    public void PlayDoorTimeLine()
    {
        StartCoroutine(TimeLineCoroutine(doorOpen));
    }

    [Header("# ID : 15 우유 던지기")]
    [SerializeField] PlayableDirector milkThrow;

    public void PlayMilkTimeLine()
    {
        StartCoroutine(TimeLineCoroutine(milkThrow));
    }


    private IEnumerator TimeLineCoroutine(PlayableDirector timeline, Action onCompleted = null)
    {
        MainController.instance.ChangeState(MainController.instance._waitState);

        timeline.Play();
        yield return new WaitUntil(() => timeline.state == PlayState.Paused);
        onCompleted?.Invoke();
        DialogueManager.instance.isTransition = true;
    }

}
