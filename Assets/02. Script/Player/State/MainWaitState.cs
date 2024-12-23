using UnityEngine;

public class MainWaitState : MonoBehaviour, IControllerState
{
    private MainController mc;
    public void OnStateEnter(MainController controller)
    {
        if (mc == null)
            mc = controller;
    }

    public void OnStateUpdate()
    {

    }

    public void OnStateExit()
    {

    }
}
