using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MansionInsidePoint : MonoBehaviour
{
    private PlayerController player;

    [Header("Sound")]
    [SerializeField] private AudioSource _doorAudioSource;
    [SerializeField] private AudioClip _doorClip;

    [SerializeField]
    private Transform startPoint2;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        if(player != null)
        {
            if(player.mansionInside == 0) //저택 씬을 첫 번째 로드하는 경우
            {
                player.transform.position = this.transform.position;
            }
            else //저택 씬을 두 번째 로드하는 경우
            {
                StartInsideCoroutine();
                FadeManager.Instance.JustFade();
                player.anim.SetFloat("DirY", -1.0f);

                if(startPoint2 != null)
                {
                    player.transform.position = startPoint2.position;
                }
            }
        }
    }

    public void StartInsideCoroutine()
    {
        player._audioSource.clip = player.heartbeatSound;
        player._audioSource.loop = true;
        player._audioSource.Play();
        _doorAudioSource.clip = _doorClip;
        _doorAudioSource.Play();
        StartCoroutine(StartFadeOutCoroutine());
    }


    IEnumerator StartFadeOutCoroutine() //FadeOut이 된지 2초 후에 다음 대사 출력
    {
        yield return new WaitForSeconds(2.0f);
        FadeManager.Instance.StartFadeOut();

        player.maxDialogueCounter = 99;
        player._dialogueManager.ShowDialogue(player.currentDialogueCounter.ToString());

    }
}
