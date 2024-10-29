using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YujiRunState : MonoBehaviour, IYujiState
{
    private YujiController _playerController;
    private Vector2 firstInputDirection;

    public void OnStateEnter(YujiController playerController)
    {
        if (!_playerController)
            _playerController = playerController;

        _playerController.anim.SetBool("Run", true);

        _playerController.movement = Vector2.zero;
        firstInputDirection = Vector2.zero;
    }


    public void OnStateUpdate()
    {
        if (_playerController)
        {
            _playerController.movement.x = Input.GetAxisRaw("Horizontal");
            _playerController.movement.y = Input.GetAxisRaw("Vertical");

            // 첫 입력 방향 설정 (선입력 방향이 여전히 활성화되어 있지 않을 때만 업데이트)
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

            // 입력이 없는 경우 방향 초기화
            if (_playerController.movement == Vector2.zero)
            {
                firstInputDirection = Vector2.zero;
            }

            // 선입력된 방향의 애니메이션 재생
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

            // 선입력된 방향의 키가 떼어지면 새 방향 설정
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
                    _playerController._rigidbody.MovePosition(_playerController._rigidbody.position + _playerController.runSpeed * Time.fixedDeltaTime * _playerController.movement.normalized);
                    
                }
                if(Input.GetKeyUp(KeyCode.LeftShift))
                {
                    _playerController.ChangeState(_playerController._walkState);
                }
            }
            else
            {
                _playerController.ChangeState(_playerController._idleState);
            }

            if (Input.GetKeyDown(KeyCode.E)) // 'E' 키로 상호작용
            {
                _playerController.Interact();
            }
        }
    }
    public void OnStateExit()
    {
        _playerController.movement = Vector2.zero;
        firstInputDirection = Vector2.zero;
        _playerController.anim.SetBool("Run", false);
    }

}