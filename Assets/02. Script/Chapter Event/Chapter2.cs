using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Playables;
using AYellowpaper.SerializedCollections;


public class Chapter2 : MonoBehaviour
{
    [Header("# 챕터 타임라인")]
    [SerializeField] PlayableDirector[] timeline;

    private int currentTimelineIndex = 0;

    private void Awake() {
        foreach (var item in timeline)
        {
            item.stopped += (pd) => {
                Destroy(pd.gameObject);
            };
        }
    }
    
    public void ChapterTimeline()
    {
        StartCoroutine(TimeLineCoroutine(timeline[currentTimelineIndex]));
    }

    public void NextTimeline()
    {
        currentTimelineIndex++;
        if (currentTimelineIndex >= timeline.Length)
        {
            Debug.LogWarning("더 이상 재생할 타임라인이 없습니다.");
            return;
        }
        StartCoroutine(TimeLineCoroutine(timeline[currentTimelineIndex]));
    }

    private IEnumerator TimeLineCoroutine(PlayableDirector timeline)
    {
        if (timeline == null)
        {
            Debug.LogError("타임라인이 null입니다.");
            yield break;
        }

        MainController.instance.ChangeWaitState();

        timeline.Play();
        yield return new WaitUntil(() => timeline == null || timeline.state == PlayState.Paused);
        
        if(timeline != null)
        DialogueManager.instance.isTransition = true;
    }
}
