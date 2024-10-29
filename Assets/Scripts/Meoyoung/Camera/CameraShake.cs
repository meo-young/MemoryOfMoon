using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //메인 카메라
    public Camera mCamera;
    //씬이 로드될 때 카메라 암에 붙어있는 카메라의 위치를 저장하는 변수
    private Vector3 mCameraOriginPos;
    //카메라 흔들기의 흔들림 세기를 지정하는 변수. 기본값으로 지정하여 사용할 수 있도록 한다
    [SerializeField][Range(0.01f, 1f)] private float mOriginShakeRange = .05f;
    //카메라 흔들기 시간을 지정하는 변수. 기본값으로 지정하여 사용할 수 있도록 한다
    [SerializeField][Range(0.1f, 5.0f)] public float mOriginShakeDuration = .5f;
    //카메라를 흔드는 도중 해당 변수의 간격마다 초기 위치로 돌아가게 하는 변수. 사용하지 않으면 초기 위치로부터 크게 벗어날 수 있기에 사용해야 자연스러워짐.
    [SerializeField] private int mOriginShakeInitSpacing = 5;
    //카메라 흔들기에 사용되는 코루틴을 담는 변수
    Coroutine mStartCameraShakeCoroutine, mEndCameraShakeCoroutine;

    public FirstCameraManager camManager;

    //씬이 로드되면 카메라를 등록하고 카메라의 초기 위치를 저장한다.
    private void Start()
    {
        mCamera = Camera.main;
    }

    private void Update()
    {
        mCameraOriginPos = mCamera.transform.localPosition;
    }

    //카메라를 흔들기를 시작하는함수. 기본 매개변수로 호출하면 인스펙터에서 초기화 된 값으로 호출된다.
    public void ShakeCamera(float shakeRange = 0, float duration = 0)
    {
        camManager.enabled = false;
        Debug.Log("카메라 흔들기 호출");

        StopPrevCameraShakeCoroutines();

        shakeRange = shakeRange == 0 ? mOriginShakeRange : shakeRange;
        duration = duration == 0 ? mOriginShakeDuration : duration;

        mStartCameraShakeCoroutine = StartCoroutine("StartShake", shakeRange);
        mEndCameraShakeCoroutine = StartCoroutine("StopShake", duration);

    }

    //일정 시간동안 흔들기를 진행하는 코루틴
    private IEnumerator StartShake(float shakeRange)
    {
        float cameraPosX, cameraPosY;
        Vector3 cameraPos;
        int shakeInitSpacing = mOriginShakeInitSpacing;

        while (true)
        {
            --shakeInitSpacing;

            //최대 0 ~ shakeRange * 2까지 나오는 값에서 shakeRange를 빼면 -shakeRange ~ +shakeRange 까지의 범위를 가진다.
            cameraPosX = Random.value * shakeRange * 2 - shakeRange;
            cameraPosY = Random.value * shakeRange * 2 - shakeRange;

            cameraPos = mCamera.transform.position; // localPosition으로 변경
            cameraPos.x += cameraPosX;
            cameraPos.y += cameraPosY;
            mCamera.transform.position = cameraPos; // localPosition으로 변경

            if (shakeInitSpacing < 0)
            {
                shakeInitSpacing = mOriginShakeInitSpacing;

                mCamera.transform.position = mCameraOriginPos;
            }

            yield return null;
        }
    }

    //단순히 일정 시간 후에 
    private IEnumerator StopShake(float duration)
    {
        yield return new WaitForSeconds(duration);

        mCamera.transform.position = mCameraOriginPos;
        camManager.enabled = true;

        StopPrevCameraShakeCoroutines();
    }

    //비 정상적으로 짧은 시간에 여러 번 호출될경우 의도하지 않은 방향으로 진행되는것을 방지하기 위해 코루틴을 검사하여 제거한다.
    private void StopPrevCameraShakeCoroutines()
    {
        if (mStartCameraShakeCoroutine != null)
        {
            StopCoroutine(mStartCameraShakeCoroutine);
        }

        if (mEndCameraShakeCoroutine != null)
        {
            StopCoroutine(mEndCameraShakeCoroutine);
        }
    }
}
