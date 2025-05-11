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
    private RaycastHit2D hit;
    private IInteraction lastInteractedObject;

    private void Awake()
    {
        if (instance == null)
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
        if (movement == Vector2.zero)
            anim.SetBool("Idle", true);
        else
            anim.SetBool("Idle", false);

        CheckInteraction();
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
        if (hit.collider == null)
            return;

        IInteractable interactable = hit.collider.GetComponent<IInteractable>();
        if (interactable != null)
            ChangeState(_waitState);
        interactable?.Interact();
    }

    void CheckInteraction()
    {
        hit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, CurrentDirection, 0.1f, layerMask);

        if (hit.collider == null)
        {
            if (lastInteractedObject != null)
            {
                lastInteractedObject.StopInteraction();
                lastInteractedObject = null;
            }

            return;
        }

        IInteraction currentInteraction = hit.collider.GetComponent<IInteraction>();
        if (currentInteraction != lastInteractedObject)
        {
            if (lastInteractedObject != null)
                lastInteractedObject.StopInteraction();

            currentInteraction?.CanInteraction();

            lastInteractedObject = currentInteraction;
        }
    }

    public void ChangeWaitState()
    {
        ChangeState(_waitState);
    }

    public void ChangeIdleState()
    {
        ChangeState(_idleState);
    }

    public void FlipX()
    {
        if (movement.x > 0) anim.SetFloat("DirX", -1.0f);
        else if(movement.x < 0) anim.SetFloat("DirX", 1.0f);
    }
    
    public void FlipY()
    {
        if (movement.y > 0) anim.SetFloat("DirY", -1.0f);
        else if(movement.y < 0) anim.SetFloat("DirY", 1.0f);
    }

}
