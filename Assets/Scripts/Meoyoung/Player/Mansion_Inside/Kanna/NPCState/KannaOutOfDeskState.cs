using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KannaOutOfDeskState : MonoBehaviour, IKannaState
{
    private KannaController _npcController;
    private PlayerController pc;
    private float animationTimer = 3f;
    private float startTimer;

    public void OnStateEnter(KannaController npcController)
    {
        if (!_npcController)
            _npcController = npcController;

        pc = _npcController._playerController;
        pc.ChangeState(pc._waitState);
        StartCoroutine(StartAnimationSet());
        startTimer = 0;
    }
    public void OnStateUpdate() // animation 시간에 도달한 경우 idleState로 전환하고 플레이어를 DialogueState로 전환
    {
        startTimer += Time.deltaTime;
        if(startTimer > animationTimer) 
        {
            _npcController.ChangeState(_npcController._idleState);
            pc.ChangeState(pc._diaState);
            pc.maxDialogueCounter = 30;
            pc._dialogueManager.ShowDialogue(pc.currentDialogueCounter.ToString());
        }
    }

    public void OnStateExit()
    {
        _npcController.anim.SetBool("Out", false);
    }


    IEnumerator StartAnimationSet()
    {
        pc._audioSource.clip = _npcController.hideAndSeek;
        pc._audioSource.Play();
        _npcController.chair_desk_Move.MoveCoroutine();
        yield return new WaitForSeconds(1f);
        _npcController.RecoverTransparency();
        _npcController.anim.SetBool("Out", true);
    }
}
