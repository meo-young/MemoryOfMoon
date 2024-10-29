using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KannaController : MonoBehaviour
{
    public Animator anim;

    public Transform[] waypoints;  // 이동할 경로 지점들
    public Transform moveOut; //칸나가 나갈 현관 위치
    public float moveSpeed = 15f;   // 이동 속도
    public float waitTime = 1f;    // 각 지점에서 대기 시간
    [HideInInspector]
    public bool goToLivingRoom = false; // 칸나가 서재에서 거실로 이동하기 위한 변수
    [HideInInspector]
    public PlayerController _playerController;
    [HideInInspector]
    public CharacterController _characterController;
    [HideInInspector]
    public Rigidbody2D _rigidbody;
    [HideInInspector]
    private SpriteRenderer _spriteRenderer;
    [HideInInspector]
    public ChairAndDeskMoving chair_desk_Move;
    [HideInInspector]
    public LayerController _layerController;
    public GameObject boxCollider;

    [Header("Movement")]
    public float walkSpeed = 5f;
    public StepType stepType;

    [Header("Sound")]
    public AudioSource _audioSource;
    public AudioSource _stepAudioSource;
    public AudioClip[] CarpetClips; // Carpet1, Carpet2, Carpet3, Carpet4
    public AudioClip[] DirtClips;   // Dirt1, Dirt2, Dirt3, Dirt4
    public AudioClip[] HallClips;   // Hall1, Hall2, Hall3, Hall4
    public AudioClip[] ConcreteClips; // Concrete1, Concrete2, Concrete3, Concrete4
    public AudioClip[] GrassClips;  // Grass1, Grass2, Grass3, Grass4
    public AudioClip[] WoodClips;   // Wood1, Wood2, Wood3, Wood4
    private System.Random random = new();
    public AudioClip doorOpenSound;
    public AudioClip hideAndSeek; // 방문 여는 사운드

    [SerializeField] Transform spawnPoint;
    public IKannaState CurrentState
    {
        get; set;
    }

    public IKannaState _idleState, _walkState, _hideState, _outState;



    private void Start()
    {
        this.transform.position = spawnPoint.position;
        goToLivingRoom = false;

        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        _idleState = gameObject.AddComponent<KannaIdleState>();
        _walkState = gameObject.AddComponent<KannaWalkState>();
        _hideState = gameObject.AddComponent<KannaHideState>();
        _outState = gameObject.AddComponent<KannaOutOfDeskState>();

        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        chair_desk_Move = GetComponent<ChairAndDeskMoving>();
        _layerController = GetComponent<LayerController>();

        _layerController.enabled = false;
        boxCollider.SetActive(false);

        CurrentState = _hideState;
        ChangeState(CurrentState);
    }

    private void Update()
    {
        UpdateState();
    }

    public void ChangeState(IKannaState npcState)
    {
        if (CurrentState != null)
            CurrentState.OnStateExit();
        CurrentState = npcState;
        CurrentState.OnStateEnter(this);
    }

    public void UpdateState()
    {
        if (CurrentState != null)
        {
            CurrentState.OnStateUpdate();
        }
    }


    public void PlayWalkingSound()
    {
        AudioClip[] selectedClips = null;

        switch (stepType)
        {
            case StepType.Carpet:
                selectedClips = CarpetClips;
                break;
            case StepType.Dirt:
                selectedClips = DirtClips;
                break;
            case StepType.Hall:
                selectedClips = HallClips;
                break;
            case StepType.Concrete:
                selectedClips = ConcreteClips;
                break;
            case StepType.Grass:
                selectedClips = GrassClips;
                break;
            case StepType.Wood:
                selectedClips = WoodClips;
                break;
        }

        if (selectedClips != null && selectedClips.Length > 0)
        {
            int randomIndex = random.Next(0, selectedClips.Length);
            _stepAudioSource.clip = selectedClips[randomIndex];
            _stepAudioSource.Play();
        }
    }
    public void SetTransparency()
    {
        Color color = _spriteRenderer.color;

        color.a = 0f;

        _spriteRenderer.color = color;
    }

    public void RecoverTransparency()
    {
        Color color = _spriteRenderer.color;

        color.a = 1f;

        _spriteRenderer.color = color;
    }


    public void OnAnimationEnd()
    {
        // y축으로 -1만큼 이동
        transform.position += new Vector3(0, -3, 0);
    }

    public void MoveKanna()
    {
        StartCoroutine(MoveOutKanna());
    }

    IEnumerator MoveOutKanna() // 칸나를 거실밖으로 이동시킴
    {
        anim.SetBool("Walk", true);
        anim.SetFloat("DirX", 0.0f);
        anim.SetFloat("DirY", -1.0f);
        while (transform.position != moveOut.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveOut.position, walkSpeed * Time.deltaTime);
            yield return null; // 다음 프레임까지 대기
        }
        
        this.gameObject.SetActive(false);
    }
}
