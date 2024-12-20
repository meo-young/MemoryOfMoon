using UnityEngine;

public class MainController : MonoBehaviour
{
    public static MainController instance;

    [Header("Movement")]
    public float walkSpeed = 0.8f;                        
    public float runSpeed = 1.8f;                
   
    public IControllerState CurrentState
    {
        get; private set;
    }

    public IControllerState _idleState, _walkState, _sprintState;

    [HideInInspector] public Vector2 movement;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Rigidbody2D rb;

    private void Awake()
    {
        if(instance == null)
            instance = this;

        _idleState = gameObject.AddComponent<MainIdleState>();
        _walkState = gameObject.AddComponent<MainWalkState>();
        _sprintState = gameObject.AddComponent<MainSprintState>();

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        CurrentState = _idleState;
        ChangeState(CurrentState);
    }

    private void Update()
    {
        UpdateState();
    }

    public void UpdateState()
    {
        CurrentState?.OnStateUpdate();
    }

    public void ChangeState(IControllerState state)
    {
        CurrentState?.OnStateExit();
        CurrentState = state;
        CurrentState.OnStateEnter(this);
    }

    public Direction GetPlayerDirection()
    {

        return Direction.DOWN;
    }
}
