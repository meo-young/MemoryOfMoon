using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KannaIdleState : MonoBehaviour, IKannaState
{
    private KannaController _npcController;
    private PlayerController pc;

    public void OnStateEnter(KannaController npcController)
    {
        if (!_npcController)
            _npcController = npcController;
        pc = _npcController._playerController;
    }
    public void OnStateUpdate()
    {
        if (_npcController.goToLivingRoom)
        {
            _npcController.ChangeState(_npcController._walkState);
        }
    }

    public void OnStateExit()
    {

    }

}
