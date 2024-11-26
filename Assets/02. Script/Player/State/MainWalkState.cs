using UnityEngine;

public class MainWalkState : MonoBehaviour, IControllerState
{
    private MainController mc;
    private readonly int hashWalk = Animator.StringToHash("Walk");
    private readonly int hashDirX = Animator.StringToHash("DirX");
    private readonly int hashDirY = Animator.StringToHash("DirY");
    public void OnStateEnter(MainController controller)
    {
        if (mc == null)
            mc = controller;
        mc.anim.SetBool(hashWalk, true);
    }

    public void OnStateUpdate()
    {
        mc.movement.x = Input.GetAxisRaw("Horizontal");
        mc.movement.y = Input.GetAxisRaw("Vertical");

        if (mc.movement == Vector2.zero)
        {
            mc.ChangeState(mc._idleState);
            return;
        }
        else
        {
            mc.rb.MovePosition(mc.rb.position + mc.movement.normalized * mc.walkSpeed * Time.fixedDeltaTime);
        }

        mc.anim.SetFloat(hashDirX, mc.movement.x);
        mc.anim.SetFloat(hashDirY, mc.movement.y);



    }

    public void OnStateExit()
    {
        mc.anim.SetBool(hashWalk, false);
    }
}