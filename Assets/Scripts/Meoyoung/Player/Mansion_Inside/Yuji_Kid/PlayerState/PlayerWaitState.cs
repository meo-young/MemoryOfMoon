using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerWaitState : MonoBehaviour, IPlayerState
{
    private PlayerController _playerController;

    public void OnStateEnter(PlayerController npcController)
    {
        if (!_playerController)
            _playerController = npcController;

        _playerController.anim.SetBool("Wait", true);
        if (_playerController.currentDialogueCounter == 59) //켄타를 찾아서 옷장에서 나오는 부분
        {
            StartCoroutine(GotoLivingRoom());
        }
    }
    public void OnStateUpdate()
    {
        
    }

    public void OnStateExit()
    {
        _playerController.anim.SetBool("Wait", false);
    }

    public IEnumerator GotoLivingRoom() //켄타를 찾고난 후 페이드인 페이드아웃 효과로 거실로 시점 변환
    {
        BGMManager.Instance.StartFadeIn(1.0f);
        FadeManager.Instance.StartFade(); // 페이드 인 후 1초 대기 후 페이드 아웃
        yield return new WaitForSeconds(2.5f);
        BGMManager.Instance.StartFadeOut(1.0f);
        _playerController.anim.SetFloat("DirX", 0.0f);
        _playerController.anim.SetFloat("DirY", -1.0f);

        _playerController.gameObject1.GetComponent<Renderer>().sortingOrder = 1;
        _playerController.gameObject2.GetComponent<Renderer>().sortingOrder = 1;
        _playerController.gameObject3.GetComponent<Renderer>().sortingOrder = 1;
        _playerController.gameObject4.GetComponent<Renderer>().sortingOrder = 1;
        _playerController.gameObject5.GetComponent<Renderer>().sortingOrder = 1;

        _playerController._camera.transform.position = new Vector3(-6.2f, -348.8f, -10.0f);
        _playerController.transform.position = new(-6.2f, -348.8f, 0.0f);

        yield return new WaitForSeconds(2.0f);
        _playerController.maxDialogueCounter = 60; //츠네모리 신야가 거실로 들어오는 부분전까지
        _playerController._dialogueManager.ShowDialogue(_playerController.currentDialogueCounter.ToString());
    }
}
