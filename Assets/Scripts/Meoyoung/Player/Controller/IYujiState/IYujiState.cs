using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IYujiState
{
    void OnStateEnter(YujiController controller);
    void OnStateUpdate();
    void OnStateExit();
}
