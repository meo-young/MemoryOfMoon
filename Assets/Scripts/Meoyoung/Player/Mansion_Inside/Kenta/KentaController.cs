using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KentaController : MonoBehaviour
{
    public Animator anim;
    public Transform moveOut;

    [HideInInspector]
    public PlayerController _playerController;

    [Header("Movement")]
    public float walkSpeed = 15f;
    public StepType stepType;

    [Header("Sound")]
    public AudioSource _stepAudioSource;
    public AudioClip[] CarpetClips; // Carpet1, Carpet2, Carpet3, Carpet4
    public AudioClip[] DirtClips;   // Dirt1, Dirt2, Dirt3, Dirt4
    public AudioClip[] HallClips;   // Hall1, Hall2, Hall3, Hall4
    public AudioClip[] ConcreteClips; // Concrete1, Concrete2, Concrete3, Concrete4
    public AudioClip[] GrassClips;  // Grass1, Grass2, Grass3, Grass4
    public AudioClip[] WoodClips;   // Wood1, Wood2, Wood3, Wood4
    private System.Random random = new();

    [SerializeField] Transform spawnPoint;
    private void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        this.transform.position = spawnPoint.position;
    }

    public void MoveKenta()
    {
        StartCoroutine(MoveOutKenta());
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

    IEnumerator MoveOutKenta() // 김신을 거실까지 이동시키고 플레이어의 방향을 아래로 변환 후 대사 출력
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
