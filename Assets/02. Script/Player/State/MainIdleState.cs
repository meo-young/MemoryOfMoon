using UnityEngine;

public class MainIdleState : MonoBehaviour, IControllerState
{
    private MainController mc;

    public void OnStateEnter(MainController controller)
    {
        if (mc == null)
            mc = controller;
    }

    public void OnStateUpdate()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
        {
            mc.ChangeState(mc._walkState);
        }

        if (Input.GetKeyDown(KeyCode.E)) // 'E' 키로 상호작용
        {
            mc.Interact();
        }
    }

    public void OnStateExit()
    {

    }
}
