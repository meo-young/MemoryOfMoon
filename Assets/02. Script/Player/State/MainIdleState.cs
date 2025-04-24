using UnityEngine;

public class MainIdleState : MonoBehaviour, IControllerState
{
    private MainController mc;

    public void OnStateEnter(MainController controller)
    {
        if (!mc)
            mc = controller;
    }

    public void OnStateUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.DownArrow))
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
