using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKannaState
{
    void OnStateEnter(KannaController controller);
    void OnStateUpdate();
    void OnStateExit();
}
