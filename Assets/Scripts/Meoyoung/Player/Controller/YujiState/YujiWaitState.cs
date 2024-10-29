using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class YujiWaitState : MonoBehaviour, IYujiState
{
    private YujiController _playerController;

    public void OnStateEnter(YujiController npcController)
    {
        if (!_playerController)
            _playerController = npcController;

        _playerController.anim.SetBool("Wait", true);
    }
    public void OnStateUpdate()
    {

    }

    public void OnStateExit()
    {
        _playerController.anim.SetBool("Wait", false);
    }

}
