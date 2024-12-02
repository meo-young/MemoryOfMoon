using UnityEngine;

public class MainSprintState : MonoBehaviour, IControllerState
{
    private MainController mc;
    private readonly int hashSprint = Animator.StringToHash("Sprint");
    private readonly int hashDirX = Animator.StringToHash("DirX");
    private readonly int hashDirY = Animator.StringToHash("DirY");

    public void OnStateEnter(MainController controller)
    {
        if (mc == null)
            mc = controller;

        mc.anim.SetBool(hashSprint, true);
    }

    public void OnStateUpdate()
    {
        mc.movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            mc.ChangeState(mc._walkState);
            return;
        }

        if(mc.movement == Vector2.zero)
        {
            mc.ChangeState(mc._idleState);
            return;
        }
        else
        {
            mc.rb.MovePosition(mc.rb.position + mc.runSpeed * Time.fixedDeltaTime * mc.movement.normalized);
        }
    }

    public void OnStateExit()
    {
        mc.anim.SetBool(hashSprint, false);
    }
}
