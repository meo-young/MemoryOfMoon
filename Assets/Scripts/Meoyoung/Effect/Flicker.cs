using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    //오브젝트 깜빡임 구현으로 조명 깜빡임 연출
    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;
    public AudioSource _audioSource;
    public AudioClip _flickerSound;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        playerController._flicker = this;
    }

    public void StartFlicker()
    {
        StartCoroutine(FlickerRoutine());
    }

    IEnumerator FlickerRoutine()
    {
        PlayFlickerSound();
        yield return new WaitForSeconds(0.5f);
        SetTransparency(240);
        yield return new WaitForSeconds(0.1f);
        SetTransparency(0);
        yield return new WaitForSeconds(0.2f);
        SetTransparency(240);
        yield return new WaitForSeconds(0.1f);
        SetTransparency(0);
        yield return new WaitForSeconds(0.15f);
        SetTransparency(240);
        yield return new WaitForSeconds(0.1f);
        SetTransparency(0);
        yield return new WaitForSeconds(0.5f);
        SetTransparency(240);
        yield return new WaitForSeconds(0.1f);
        SetTransparency(0);
    }

    void SetTransparency(float alphaValue)
    {
        Color color = spriteRenderer.color;
        color.a = alphaValue / 255f; // 유니티에서는 투명도가 0~1 범위로 설정됩니다.
        spriteRenderer.color = color;
    }

    public void PlayFlickerSound()
    {
        _audioSource.clip = _flickerSound;
        _audioSource.Play();
    }
}
