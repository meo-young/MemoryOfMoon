using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState
{
    void OnStateEnter(PlayerController controller);
    void OnStateUpdate();
    void OnStateExit();
}
