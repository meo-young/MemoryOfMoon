using UnityEngine;

public class YujiController : MonoBehaviour
{
    [Header("Animator")]
    public Animator anim;

    [Header("Rigidbody")]
    [Tooltip("캐릭터의 움직임을 제어하는 컴포넌트")]
    public Rigidbody2D _rigidbody;

    [Header("Interact")]
    [SerializeField] LayerMask interactableLayer; // 상호작용 가능한 레이어 설정
    [SerializeField] Vector2 interactionAreaSize = new(2f, 1f); // 상호작용 영역의 크기
    [SerializeField] IInteractable interactable; //상호작용이 가능한 스크립트를 적용하기 위한 변수

    [Header("Movement")]
    [HideInInspector]
    public Vector2 movement; //플레이어가 바라보는 방향
    public float walkSpeed = 5f; //플레이어가 걷는 속도
    public float runSpeed = 8f; //플레이어가 뛰는 속도


    public static YujiController Instance { get; private set; } // Singleton 인스턴스


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 전환되어도 파괴되지 않음
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 있다면 새로 생성된 것을 파괴
        }
    }

    public IYujiState CurrentState
    {
        get; set;
    }

    public Vector2 CurrentDirection
    {
        get; set;
    }

    public IYujiState _idleState, _walkState, _waitState, _monoState, _diaState, _runState;


    private void Start()
    {
        YujiDirectionUtils.Initialize(this); // 플레이어 Direction 체크하는 함수 초기화

        _waitState = gameObject.AddComponent<YujiWaitState>();
        _idleState = gameObject.AddComponent<YujiIdleState>();
        _walkState = gameObject.AddComponent<YujiWalkState>();
        _monoState = gameObject.AddComponent<YujiMonologueState>();
        _diaState = gameObject.AddComponent<YujiDialogueState>();
        _runState = gameObject.AddComponent<YujiRunState>();

        _rigidbody = GetComponent<Rigidbody2D>();

        ChangeState(_idleState);
    }

    private void Update()
    {
        UpdateState();
        UpdateDirection();
    }

    public void ChangeState(IYujiState playerState)
    {
        if (CurrentState != null)
            CurrentState.OnStateExit();
        CurrentState = playerState;
        CurrentState.OnStateEnter(this);
    }

    public void UpdateState()
    {
        if (CurrentState != null)
        {
            CurrentState.OnStateUpdate();
        }
    }

    public void UpdateDirection()
    {
        CurrentDirection = new(anim.GetFloat("DirX"), anim.GetFloat("DirY"));
    }


    public void Interact()
    {
        Vector2 centerPosition = new(0.0f, 0.0f);
        // X축으로 이동중일 때에 이동하는 방향에 따라 왼쪽, 오른쪽 영역 적용
        if (CurrentDirection.x != 0)
        {
            Vector2 location = new(anim.GetFloat("DirX"), 0.35f);
            interactionAreaSize = new(2.5f, 2.0f);
            centerPosition = (Vector2)transform.position + location * (interactionAreaSize);
        }
        //Y축으로 이동중일 때에 이동하는 방향에 따라 위, 아래 영역 적용
        else if (CurrentDirection.y != 0)
        {
            Vector2 location = new(0.0f, anim.GetFloat("DirY"));
            interactionAreaSize = new(5.5f, 1.5f);
            centerPosition = (Vector2)transform.position + CurrentDirection * (interactionAreaSize);
        }


        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(centerPosition, interactionAreaSize, 0f, interactableLayer);
        foreach (Collider2D hitCollider in hitColliders)
        {
            interactable = hitCollider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
                Debug.Log("상호작용 대상: " + hitCollider.gameObject.name);
                // 가장 가까운 하나의 오브젝트와만 상호작용하려면 여기서 break;
                break;
            }
        }
    }


    // 디버그를 위한 기즈모 그리기 (에디터에서만 표시됨)
    private void OnDrawGizmosSelected()
    {
        Vector2 location = new(0.0f, 1f);
        Gizmos.color = Color.yellow;
        Vector2 centerPosition = (Vector2)transform.position + location * (interactionAreaSize);
        Gizmos.DrawWireCube(centerPosition, interactionAreaSize);
    }
}


