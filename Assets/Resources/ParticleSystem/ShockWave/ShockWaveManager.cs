using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShockWaveManager : MonoBehaviour
{
    [SerializeField] float _shockWaveTime = 0.75f;

    Coroutine _shockWaveCoroutine;
    Material _material;
    static int _waveDistanceFromCenter = Shader.PropertyToID("_WaveDistanceFromCenter");

    private void Awake()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            CallShockWave();
        }
    }

    public void CallShockWave()
    {
        _shockWaveCoroutine = StartCoroutine(ShockWaveAction(-0.1f, 1f));
    }

    private IEnumerator ShockWaveAction(float startPos, float endPos)
    {
        _material.SetFloat(_waveDistanceFromCenter, startPos);

        float lerpedAmount = 0f;

        float elapsedTime = 0f;
        while (elapsedTime < _shockWaveTime)
        {

            elapsedTime += Time.deltaTime;

            lerpedAmount = Mathf.Lerp(startPos, endPos, elapsedTime/_shockWaveTime);
            _material.SetFloat(_waveDistanceFromCenter, lerpedAmount);

            yield return null;
        }
    }
}
