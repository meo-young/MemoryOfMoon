using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceSound : MonoBehaviour
{
    public AudioSource _audioSource;

    public AudioClip mansionOscillateSound;

    public void PlayMansionSound() //저택 흔들리는 소음 재생
    {
        _audioSource.clip = mansionOscillateSound;
        _audioSource.Play();
    }
}
