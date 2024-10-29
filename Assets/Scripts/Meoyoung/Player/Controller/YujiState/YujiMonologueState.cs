using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YujiMonologueState : MonoBehaviour, IYujiState
{
    private YujiController _playerController;

    public void OnStateEnter(YujiController playerController)
    {
        if (!_playerController)
            _playerController = playerController;
        _playerController.anim.SetBool("Monologue", true);
    }
    public void OnStateUpdate()
    {

    }

    public void OnStateExit()
    {
        _playerController.anim.SetBool("Monologue", false);
    }
}
