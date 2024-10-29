using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDoorSound : MonoBehaviour
{
    public AudioSource _audioSource;

    public AudioClip doorOpenSound;

    private void Start()
    {
        StartCoroutine(StartSound());
    }

    IEnumerator StartSound()
    {
        yield return new WaitForSeconds(0.5f);
        _audioSource.clip = doorOpenSound;
        _audioSource.Play();
    }
}
