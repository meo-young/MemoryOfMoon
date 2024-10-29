using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KannaHideState : MonoBehaviour, IKannaState
{
    private KannaController _npcController;

    public void OnStateEnter(KannaController npcController)
    {
        if (!_npcController)
            _npcController = npcController;

    }
    public void OnStateUpdate()
    {
        //Debug.Log("Kanna Hide");

        _npcController.SetTransparency();
    }

    public void OnStateExit()
    {

    }
}
