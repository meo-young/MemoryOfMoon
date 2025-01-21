using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Playables;

public class Chapter2 : MonoBehaviour
{
    [Header("# 챕터 타임라인")]
    [SerializeField] PlayableDirector doorOpen;

    
    public void ChapterTimeline()
    {
        StartCoroutine(TimeLineCoroutine(doorOpen));
    }

    private IEnumerator TimeLineCoroutine(PlayableDirector timeline, Action onCompleted = null)
    {
        MainController.instance.ChangeWaitState();

        timeline.Play();
        yield return new WaitUntil(() => timeline.state == PlayState.Paused);
        onCompleted?.Invoke();
        DialogueManager.instance.isTransition = true;
    }

}
