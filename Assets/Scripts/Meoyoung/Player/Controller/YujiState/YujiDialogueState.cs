using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YujiDialogueState : MonoBehaviour, IYujiState
{
    private YujiController _playerController;

    public void OnStateEnter(YujiController playerController)
    {
        if (!_playerController)
            _playerController = playerController;
        _playerController.anim.SetBool("Dialogue", true);
    }
    public void OnStateUpdate()
    {
        
    }

    public void OnStateExit()
    {
        _playerController.anim.SetBool("Dialogue", false);
    }
}
