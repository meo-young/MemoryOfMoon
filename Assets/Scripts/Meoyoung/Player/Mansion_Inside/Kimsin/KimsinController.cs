using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KimsinController : MonoBehaviour
{
    public Transform moveIntoLivingRoom;  // 이동할 경로 지점들
    public List<Transform> moveOut;
    public int currentWaypointIndex2 = 0;
    public List<Transform> moveToLibray;
    public int currentWaypointIndex = 0;
    public float moveSpeed = 15f;   // 이동 속도
    public Animator anim;

    [Header("Controller")]
    public PlayerController _playerController;
    public KannaController _kannaController;
    public KentaController _kentaController;

    [SerializeField]
    private PlayerInventory _playerInventory;

    [Header("Movement")]
    public StepType stepType;

    [Header("Sound")]
    public AudioSource _stepAudioSource;
    public AudioSource _doorAudioSource;
    public AudioSource _outdoorAudioSource;
    public AudioClip[] CarpetClips; // Carpet1, Carpet2, Carpet3, Carpet4
    public AudioClip[] DirtClips;   // Dirt1, Dirt2, Dirt3, Dirt4
    public AudioClip[] HallClips;   // Hall1, Hall2, Hall3, Hall4
    public AudioClip[] ConcreteClips; // Concrete1, Concrete2, Concrete3, Concrete4
    public AudioClip[] GrassClips;  // Grass1, Grass2, Grass3, Grass4
    public AudioClip[] WoodClips;   // Wood1, Wood2, Wood3, Wood4
    public AudioClip doorOpenSound1;
    public AudioClip doorOpenSound2;
    private System.Random random = new();
    private void Start()
    {
        this.gameObject.SetActive(false);
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
    public void RunShowKimsinCoroutine()
    {
        this.gameObject.SetActive(true);
        StartCoroutine(ShowKimsin());
    }

    public void GoToLibraryKimsin()
    {
        StartCoroutine(MoveWhile());
    }

    public void RunMoveOutCoroutine()
    {
        StartCoroutine(MoveOutWhile());
    }

    IEnumerator ShowKimsin() // 김신을 거실까지 이동시키고 플레이어의 방향을 아래로 변환 후 대사 출력
    {
        anim.SetBool("Walk", true);
        anim.SetFloat("DirY", 1.0f);
        while (transform.position != moveIntoLivingRoom.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveIntoLivingRoom.position, moveSpeed * Time.deltaTime);
            yield return null; // 다음 프레임까지 대기
        }
        _kentaController.anim.SetFloat("DirY", -1.0f);
        _kannaController.anim.SetFloat("DirY", -1.0f);

        anim.SetBool("Walk", false);
        _playerController.anim.SetFloat("DirX", 0.0f);
        _playerController.anim.SetFloat("DirY", -1.0f);
        yield return new WaitForSeconds(1.0f);
        _playerController.maxDialogueCounter = 89; //유우카한테 가볼까? 라는 대사까지
        _playerController._dialogueManager.ShowDialogue(_playerController.currentDialogueCounter.ToString());
    }

    IEnumerator MoveWhile()
    {
        while (true)
        {
            if (currentWaypointIndex < moveToLibray.Count)
            {
                // 다음 지점으로 이동
                yield return StartCoroutine(GoToLibrary());

                // 지정된 시간만큼 대기
                yield return null;

                currentWaypointIndex++;
            }
            else
            {
                FadeManager.Instance.StartFade();
                _playerController.anim.SetFloat("DirX", 0.0f);
                _playerController.anim.SetFloat("DirY", -1.0f);
                _doorAudioSource.clip = doorOpenSound1;
                _doorAudioSource.Play();
                this.gameObject.SetActive(false);
                break;
            }
        }
    }

    IEnumerator GoToLibrary()
    {
        anim.SetBool("Walk", true);
        switch (currentWaypointIndex)
        {
            case 0:
                anim.SetFloat("DirX", -1.0f);
                anim.SetFloat("DirY", 0.0f);
                break;
            case 1:
                anim.SetFloat("DirX", 0.0f);
                anim.SetFloat("DirY", 1.0f);
                _playerController.anim.SetFloat("DirX", -1.0f);
                _playerController.anim.SetFloat("DirY", 0.0f);
                break;
            case 2:
                anim.SetFloat("DirX", -1.0f);
                anim.SetFloat("DirY", 0.0f);
                break;
        }
        while (transform.position != moveToLibray[currentWaypointIndex].position)
        {
            // 현재 위치에서 다음 지점으로 부드럽게 이동
            transform.position = Vector3.MoveTowards(transform.position,
                                                     moveToLibray[currentWaypointIndex].position,
                                                     moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator MoveOutWhile()
    {
        while (true)
        {
            if (currentWaypointIndex2 < moveOut.Count)
            {
                // 다음 지점으로 이동
                yield return StartCoroutine(MoveOutKimsin());

                // 지정된 시간만큼 대기
                yield return null;

                currentWaypointIndex2++;
            }
            else
            {
                FadeManager.Instance.StartFade();
                _playerController.FollowFather();
                _outdoorAudioSource.clip = doorOpenSound2;
                _outdoorAudioSource.Play();
                _playerInventory.AddItem("Outside");
                this.gameObject.SetActive(false);
                break;
            }
        }
    }
    IEnumerator MoveOutKimsin() // 김신을 현관까지 이동시킨 후 비활성화
    {
        anim.SetBool("Walk", true);
        switch (currentWaypointIndex2)
        {
            case 0:
                anim.SetFloat("DirX", 1.0f);
                anim.SetFloat("DirY", 0.0f);
                break;
            case 1:
                anim.SetFloat("DirX", 0.0f);
                anim.SetFloat("DirY", -1.0f);
                break;
            case 2:
                anim.SetFloat("DirX", 1.0f);
                anim.SetFloat("DirY", 0.0f);
                _playerController.anim.SetFloat("DirX", 0.0f);
                _playerController.anim.SetFloat("DirY", -1.0f);
                break;
            case 3:
                anim.SetFloat("DirX", 1.0f);
                anim.SetFloat("DirY", -1.0f);
                break;
        }
        while (transform.position != moveOut[currentWaypointIndex2].position)
        {
            // 현재 위치에서 다음 지점으로 부드럽게 이동
            transform.position = Vector3.MoveTowards(transform.position,
                                                     moveOut[currentWaypointIndex2].position,
                                                     moveSpeed * Time.deltaTime);
            yield return null;
        }
        
    }
}
