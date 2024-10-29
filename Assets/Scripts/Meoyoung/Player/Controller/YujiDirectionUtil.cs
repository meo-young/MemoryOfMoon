using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class YujiDirectionUtils
{
    private static YujiController _playerController;

    // PlayerController 인스턴스를 설정하는 메서드
    public static void Initialize(YujiController playerController)
    {
        _playerController = playerController;
    }
    // 플레이어의 현재 방향을 체크해주는 함수
    public static bool CheckDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.UP:
                return _playerController.anim.GetFloat("DirY") == 1;
            case Direction.DOWN:
                return _playerController.anim.GetFloat("DirY") == -1;
            case Direction.RIGHT:
                return _playerController.anim.GetFloat("DirX") == 1;
            case Direction.LEFT:
                return _playerController.anim.GetFloat("DirX") == -1;
            default:
                return false;
        }
    }
}
