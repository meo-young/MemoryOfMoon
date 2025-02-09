using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Playables;

public class Chapter2 : MonoBehaviour
{
    [Header("# 챕터 타임라인")]
    [SerializeField] PlayableDirector[] timeline;

    private int currentTimelineIndex = 0;
    
    public void ChapterTimeline()
    {
        StartCoroutine(TimeLineCoroutine(timeline[currentTimelineIndex]));
    }

    public void NextTimeline()
    {
        currentTimelineIndex++;
        StartCoroutine(TimeLineCoroutine(timeline[currentTimelineIndex]));
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
