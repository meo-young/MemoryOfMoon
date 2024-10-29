using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscillateObject : MonoBehaviour
{
    public GameObject targetObject; // 상호작용할 오브젝트

    private bool interactFlag = false;

    public float shakeDuration = 0.5f; // 흔들림 지속 시간
    public float shakeMagnitude = 0.1f; // 흔들림 강도

    public float moveDistance = 0.1f; // 이동 거리
    public float moveDuration = 0.1f; // 이동 시간

    void Update()
    {
        if (interactFlag)
        {
            // E 키 입력 감지
            if (Input.GetKeyDown(KeyCode.E) && targetObject != null)
            {
                MoveLeftRight();
            }
        }
    }

    // 트리거 영역에 들어갔을 때
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactFlag = true;
        }
    }

    // 트리거 영역에서 나갔을 때
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactFlag = false;
        }
    }

    public void MoveLeftRight()
    {
        StartCoroutine(MoveLeftRightCoroutine());
    }

    private IEnumerator MoveLeftRightCoroutine()
    {
        Vector3 originalPosition = targetObject.transform.localPosition;

        // 좌로 이동
        Vector3 leftPosition = new Vector3(originalPosition.x - moveDistance, originalPosition.y, originalPosition.z);
        yield return MoveToPosition(leftPosition);

        // 우로 이동
        Vector3 rightPosition = new Vector3(originalPosition.x + moveDistance, originalPosition.y, originalPosition.z);
        yield return MoveToPosition(rightPosition);

        // 원래 위치로 복귀
        yield return MoveToPosition(originalPosition);
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        float elapsedTime = 0;
        Vector3 startingPosition = targetObject.transform.localPosition;

        while (elapsedTime < moveDuration)
        {
            targetObject.transform.localPosition = Vector3.Lerp(startingPosition, targetPosition, (elapsedTime / moveDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        targetObject.transform.localPosition = targetPosition;
    }
}
