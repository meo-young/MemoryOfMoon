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
        // 1���� 4������ ���ڸ� �������� �����մϴ�.
        int randomIndex = Random.Range(1, 5);

        // ���õ� ���ڿ� ���� �ش��ϴ� ����� Ŭ���� ����մϴ�.
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

        // ������� ����մϴ�.
        _audioSource.Play();
    }

}
