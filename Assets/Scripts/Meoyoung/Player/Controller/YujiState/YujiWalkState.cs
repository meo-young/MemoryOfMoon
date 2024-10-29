using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class YujiWalkState : MonoBehaviour, IYujiState
{
    private YujiController _playerController;
    private Vector2 firstInputDirection;

    public void OnStateEnter(YujiController playerController)
    {
        if(!_playerController)
            _playerController = playerController;

        _playerController.anim.SetBool("Walk", true);
        _playerController.movement = Vector2.zero;
        firstInputDirection = Vector2.zero;
    }


    public void OnStateUpdate()
    {
        if (_playerController)
        {
            _playerController.movement.x = Input.GetAxisRaw("Horizontal");
            _playerController.movement.y = Input.GetAxisRaw("Vertical");

            // ù �Է� ���� ���� (���Է� ������ ������ Ȱ��ȭ�Ǿ� ���� ���� ���� ������Ʈ)
            if (firstInputDirection == Vector2.zero)
            {
                if (_playerController.movement.x != 0)
                {
                    firstInputDirection = new Vector2(_playerController.movement.x, 0);
                }
                else if (_playerController.movement.y != 0)
                {
                    firstInputDirection = new Vector2(0, _playerController.movement.y);
                }
            }

            // �Է��� ���� ��� ���� �ʱ�ȭ
            if (_playerController.movement == Vector2.zero)
            {
                firstInputDirection = Vector2.zero;
            }

            // ���Էµ� ������ �ִϸ��̼� ���
            if (firstInputDirection.x > 0)
            {
                _playerController.anim.SetFloat("DirX", 1.0f);
                _playerController.anim.SetFloat("DirY", 0.0f);
            }
            else if (firstInputDirection.x < 0)
            {
                _playerController.anim.SetFloat("DirX", -1.0f);
                _playerController.anim.SetFloat("DirY", 0.0f);
            }
            else if (firstInputDirection.y > 0)
            {
                _playerController.anim.SetFloat("DirX", 0.0f);
                _playerController.anim.SetFloat("DirY", 1.0f);
            }
            else if (firstInputDirection.y < 0)
            {
                _playerController.anim.SetFloat("DirX", 0.0f);
                _playerController.anim.SetFloat("DirY", -1.0f);
            }

            // ���Էµ� ������ Ű�� �������� �� ���� ����
            if (firstInputDirection.x != 0 && _playerController.movement.x == 0)
            {
                firstInputDirection = Vector2.zero;
            }
            else if (firstInputDirection.y != 0 && _playerController.movement.y == 0)
            {
                firstInputDirection = Vector2.zero;
            }
           

            if (_playerController.movement.x != 0 || _playerController.movement.y != 0)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    _playerController.ChangeState(_playerController._runState);
                }
                _playerController._rigidbody.MovePosition(_playerController._rigidbody.position + _playerController.movement.normalized * _playerController.walkSpeed * Time.fixedDeltaTime);
                
            }
            else
            {
                _playerController.ChangeState(_playerController._idleState);
            }

            if (Input.GetKeyDown(KeyCode.E)) // 'E' Ű�� ��ȣ�ۿ�
            {
                _playerController.Interact();
            }
        }
    }
    public void OnStateExit()
    {
        _playerController.movement = Vector2.zero;
        firstInputDirection = Vector2.zero;
        _playerController.anim.SetBool("Walk", false);
    }

}
