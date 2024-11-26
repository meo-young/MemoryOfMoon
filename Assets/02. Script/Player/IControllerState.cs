public interface IControllerState
{
    public void OnStateEnter(MainController controller);
    public void OnStateUpdate();
    public void OnStateExit();
}