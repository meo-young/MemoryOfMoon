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
        mc.movement = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        if (mc.movement != Vector2.zero)
        {
            mc.ChangeState(mc._walkState);
        }

/*        if (Input.GetKeyDown(KeyCode.E)) // 'E' 키로 상호작용
        {
            mc.Interact();
        }*/
    }

    public void OnStateExit()
    {

    }
}
