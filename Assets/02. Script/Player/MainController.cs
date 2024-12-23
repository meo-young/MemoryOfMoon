using UnityEngine;
using UnityEngine.UIElements;

public class MainController : MonoBehaviour
{
    public static MainController instance;

    [Header("# Movement")]
    public float walkSpeed = 0.8f;                        
    public float runSpeed = 1.8f;

    [Header("# Interact")]
    [SerializeField] LayerMask layerMask;
   
    public IControllerState CurrentState
    {
        get; private set;
    }

    public Vector2 CurrentDirection
    {
        get; set;
    }

    public IControllerState _idleState, _walkState, _sprintState, _waitState;

    [HideInInspector] public Vector2 movement;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Rigidbody2D rb;

    private BoxCollider2D boxCollider2D;

    private void Awake()
    {
        if(instance == null)
            instance = this;

        _idleState = gameObject.AddComponent<MainIdleState>();
        _walkState = gameObject.AddComponent<MainWalkState>();
        _sprintState = gameObject.AddComponent<MainSprintState>();
        _waitState = gameObject.AddComponent<MainWaitState>();

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        CurrentState = _idleState;
        ChangeState(CurrentState);
    }

    private void Update()
    {
        UpdateState();
        CurrentDirection = new(anim.GetFloat("DirX"), anim.GetFloat("DirY"));
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

    public void Interact()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, CurrentDirection, 0.1f, layerMask);

        if(hit.collider != null)
        {
            IInteraction interactable = hit.collider.GetComponent<IInteraction>();
            if (interactable != null)
                ChangeState(_waitState);
                interactable.Interact(() =>
                {
                    ChangeState(_idleState);
                });
        }
    }
}
