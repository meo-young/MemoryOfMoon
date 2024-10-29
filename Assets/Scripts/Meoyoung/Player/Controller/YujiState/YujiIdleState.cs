using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YujiIdleState : MonoBehaviour, IYujiState
{
    private YujiController _playerController;

    public void OnStateEnter(YujiController npcController)
    {
        if (!_playerController)
            _playerController = npcController;
    }
    public void OnStateUpdate()
    {
        //Debug.Log("Player Idle");
        // ����Ű �Է½� Walking���� ���� ����

        if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            _playerController.ChangeState(_playerController._walkState);
        }

        if (Input.GetKeyDown(KeyCode.E)) // 'E' Ű�� ��ȣ�ۿ�
        {
            _playerController.Interact();
        }

    }

    public void OnStateExit()
    {

    }

}
