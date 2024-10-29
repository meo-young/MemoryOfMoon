using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowController : MonoBehaviour
{
    public AudioSource _audioSource;

    public AudioClip shadowWalkingSound1;
    public AudioClip shadowWalkingSound2;
    public AudioClip shadowWalkingSound3;
    public AudioClip shadowWalkingSound4;

    [SerializeField] Transform spawnPoint;

    private void Start()
    {
        this.transform.position = spawnPoint.position;
    }

    public void PlayWalkingSound()
    {
        // 1부터 4까지의 숫자를 무작위로 선택합니다.
        int randomIndex = Random.Range(1, 5);

        // 선택된 숫자에 따라 해당하는 오디오 클립을 재생합니다.
        switch (randomIndex)
        {
            case 1:
                _audioSource.clip = shadowWalkingSound1;
                break;
            case 2:
                _audioSource.clip = shadowWalkingSound2;
                break;
            case 3:
                _audioSource.clip = shadowWalkingSound3;
                break;
            case 4:
                _audioSource.clip = shadowWalkingSound4;
                break;
        }

        // 오디오를 재생합니다.
        _audioSource.Play();
    }

}
