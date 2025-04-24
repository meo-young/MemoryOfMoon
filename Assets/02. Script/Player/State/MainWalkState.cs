using System.Collections.Generic;
using UnityEngine;

public class MainWalkState : MonoBehaviour, IControllerState
{
    private MainController mc;
    private readonly int hashWalk = Animator.StringToHash("Walk");
    private readonly int hashDirX = Animator.StringToHash("DirX");
    private readonly int hashDirY = Animator.StringToHash("DirY");
    private List<int> inputArr;

    public void OnStateEnter(MainController controller)
    {
        if (!mc)
            mc = controller;
        mc.anim.SetBool(hashWalk, true);

        inputArr = new List<int>(4);
    }

    public void OnStateUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if(!inputArr.Contains(0))
                inputArr.Add(0);

            if (inputArr.Contains(2))
                mc.movement.x = 0;
            else
                mc.movement.x = -1;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (!inputArr.Contains(1))
                inputArr.Add(1);

            if(inputArr.Contains(3))
                mc.movement.y = 0;
            else
                mc.movement.y = -1;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!inputArr.Contains(2))
                inputArr.Add(2);

            if (inputArr.Contains(0))
                mc.movement.x = 0;
            else
                mc.movement.x = 1;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (!inputArr.Contains(3))
                inputArr.Add(3);

            if(inputArr.Contains(1))
                mc.movement.y = 0;
            else
                mc.movement.y = 1;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            mc.movement.x = 0;
            inputArr.Remove(0);
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            mc.movement.y = 0;
            inputArr.Remove(1);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            mc.movement.x = 0;
            inputArr.Remove(2);
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            mc.movement.y = 0;
            inputArr.Remove(3);
        }

        if(inputArr.Count > 0)
        {
            switch (inputArr[0])
            {
                case 0:
                    mc.anim.SetFloat(hashDirY, 0);
                    mc.anim.SetFloat(hashDirX, -1);
                    break;
                case 1:
                    mc.anim.SetFloat(hashDirX, 0);
                    mc.anim.SetFloat(hashDirY, -1);
                    break;
                case 2:
                    mc.anim.SetFloat(hashDirY, 0);
                    mc.anim.SetFloat(hashDirX, 1);
                    break;
                case 3:
                    mc.anim.SetFloat(hashDirX, 0);
                    mc.anim.SetFloat(hashDirY, 1);
                    break;
            }
        }
        

        if (Input.GetKey(KeyCode.LeftShift))
        {
            mc.ChangeState(mc._sprintState);
            return;
        }

        if (mc.movement == Vector2.zero)
        {
            mc.ChangeState(mc._idleState);
            return;
        }
        else
        {
            mc.rb.MovePosition(mc.rb.position + mc.movement.normalized * mc.walkSpeed * Time.fixedDeltaTime);
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            mc.Interact();
        }
    }

    public void OnStateExit()
    {
        mc.anim.SetBool(hashWalk, false);
        mc.movement = Vector2.zero;
    }
}