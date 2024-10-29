using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* 문이 잠긴 소리 출력
 * 괴물 등장 사운드 출력
 * 김신 뒷걸음질
 * 괴물 등장
 * 대사 출력
 * 카메라 유우지로 포커스 전환
 * 유우지 저택 안으로 진입
 */
public class KimsinController2 : MonoBehaviour
{
    [Header("Animator")]
    public Animator _animator;
    [Header("Walk")]
    public float walkSpeed = 10f;
    [Header("MoveBackZone")]
    public Transform moveBack;
    [Header("Sound")]
    public AudioSource _audioSource;
    public AudioSource _doorAudioSource;
    public AudioClip doorSound;
    public AudioClip doorOpendSound;

    [Header("Camera")]
    [SerializeField]
    private CameraManager2 _cameraManager;

    [Header("Monster")]
    public GameObject monster;

    [Header("Controller")]
    public PlayerController _playerController;

    private void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        PlayDoorCoroutine();
        monster.SetActive(false);
    }

    public void PlayDoorCoroutine() //잠긴 문 소리 출력
    {
        StartCoroutine(StartDoorSound());
    }

    public void PlayDoorSound()
    {
        _audioSource.clip = doorSound;
        _audioSource.Play();
    }

    IEnumerator StartDoorSound() // 시작 후 1초 대기 한뒤 문 쾅쾅 소리 출력 후 카메라 시점 변환
    {
        yield return new WaitForSeconds(2.0f);
        PlayDoorSound();
        yield return new WaitForSeconds(1.0f);

        _cameraManager.TransferToKimsin();
    }

    public void WalkingBack() //뒷걸음질 시작
    {
        StartCoroutine(StartMoveBack());
    }

    IEnumerator StartMoveBack()
    {
        _animator.SetBool("Walk", true);
        while (transform.position != moveBack.position) //김신이 뒷걸음질 시작
        {
            transform.position = Vector3.MoveTowards(transform.position, moveBack.position, walkSpeed * Time.deltaTime);
            yield return null; // 다음 프레임까지 대기
        }
        _animator.SetBool("Walk", false);

        yield return new WaitForSeconds(1.0f);
        monster.SetActive(true);
        _doorAudioSource.clip = doorOpendSound;
        _doorAudioSource.Play();
        yield return new WaitForSeconds(0.5f);
        _playerController._dialogueManager.ShowDialogue(_playerController.currentDialogueCounter.ToString());
    }
}
