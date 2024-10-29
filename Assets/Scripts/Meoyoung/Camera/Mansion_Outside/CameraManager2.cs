using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager2 : MonoBehaviour
{
    [Header("Sound")]
    public AudioSource _audioSource;
    public AudioClip monsterBGM;

    [Header("Transform")]
    public Transform kimsin_Transform;
    public Transform yuji_Transform;

    [Header("Move")]
    public float moveSpeed = 20f;

    [Header("Controller")]
    public KimsinController2 kimsinController;
    public PlayerController _playerController;

    private void Start() //처음 씬이 시작되면 유우지 위치로 포커스
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _playerController._cameraManager = this;

        FadeManager.Instance.JustFade();
        transform.position = yuji_Transform.position;
        FadeManager.Instance.StartFadeOut();
    }

    public void PlayMonsterBGM()
    {
        _audioSource.clip = monsterBGM;
        _audioSource.Play();
    }

    public void TransferToKimsin()
    {
        StartCoroutine(StartTransferKimsin());
    }

    public void TransferToYuji()
    {
        StartCoroutine(StartTransferYuji());
    }

    public void ShowMonster()
    {
        StartCoroutine(StartMonsterBGM());
    }
    IEnumerator StartTransferKimsin()
    {
        while (transform.position != kimsin_Transform.position) //카메라가 김신 위치로 이동할 때까지 대기
        {
            transform.position = Vector3.MoveTowards(transform.position, kimsin_Transform.position, moveSpeed * Time.deltaTime);
            yield return null; // 다음 프레임까지 대기
        }

        yield return new WaitForSeconds(1.0f);
        _playerController.currentDialogueCounter = 94;
        _playerController.maxDialogueCounter = 94;
        _playerController._dialogueManager.ShowDialogue(_playerController.currentDialogueCounter.ToString());
    }

    IEnumerator StartMonsterBGM() //몬스터 브금 재생 후 김신 뒷걸음질
    {
        _playerController.maxDialogueCounter = 97;
        yield return new WaitForSeconds(1.0f);
        PlayMonsterBGM(); 

        yield return new WaitForSeconds(3f);
        kimsinController.WalkingBack();
    }
    
    IEnumerator StartTransferYuji()
    {
        while (transform.position != yuji_Transform.position) //카메라가 유우지 위치로 이동할 때까지 대기
        {
            transform.position = Vector3.MoveTowards(transform.position, yuji_Transform.position, moveSpeed * Time.deltaTime);
            yield return null; // 다음 프레임까지 대기
        }

        yield return new WaitForSeconds(1.0f);
        _playerController.SetTurnBack();
    }
}
