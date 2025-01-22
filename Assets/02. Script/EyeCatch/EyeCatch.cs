using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Playables;

public class EyeCatch : MonoBehaviour
{
    [Header("# 아이캐치 상태")]
    [SerializeField] EyeCatchState eyeCatchState;

    private PlayableDirector eyeCatch;

    private void Awake() {
        eyeCatch = GetComponent<PlayableDirector>();
    }

    private void Start() {
        switch (eyeCatchState)
        {
            case EyeCatchState.Dialogue:
                StartCoroutine(EyeCatchCoroutine(ShowDialogue));
                break;
            case EyeCatchState.Move:
                StartCoroutine(EyeCatchCoroutine(PlayerWaitToIdle));
                break;
            case EyeCatchState.Skip:
                PlayerWaitToIdle();
                break;
        }
    }

    void PlayerWaitToIdle()
    {
        MainController.instance.ChangeIdleState();
    }

    void ShowDialogue()
    {
        DialogueManager.instance.ShowDialogue();
    }

    private IEnumerator EyeCatchCoroutine(Action onCompleted = null)
    {
        MainController.instance.ChangeWaitState();

        eyeCatch.Play();
        yield return new WaitUntil(() => eyeCatch.state == PlayState.Paused);

        yield return new WaitForSeconds(1.0f);
        onCompleted?.Invoke();
    }



    enum EyeCatchState
    {
        Dialogue,
        Move,
        Skip
    }
}
