using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Playables;
using AYellowpaper.SerializedCollections;
using static Constant;


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

    public void SceneTransition(string sceneName)
    {
        SceneController.instance.LoadScene(sceneName);
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
        if (!timeline)
        {
            Debug.LogError("타임라인이 null입니다.");
            yield break;
        }
        
        MainController.instance.ChangeWaitState();

        timeline.Play();
        yield return new WaitUntil(() => !timeline || timeline.state == PlayState.Paused);
        DialogueManager.instance.isTransition = true;
  
    }
}
