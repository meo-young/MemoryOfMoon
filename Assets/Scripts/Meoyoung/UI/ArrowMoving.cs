using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMoving : MonoBehaviour
{
    private float moveSpeed = 1f;       // 오브젝트의 이동 속도
    private float moveDistance = 0.5f;    // 오브젝트가 이동할 최대 거리

    private Vector3 startPosition;
    private float moveDirection = 1f;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Y축을 중심으로 회전
        //transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        // Y축으로 이동
        transform.position += new Vector3(0, moveDirection * moveSpeed * Time.deltaTime, 0);

        // 최대 이동 거리 초과 시 이동 방향 반전
        if (Mathf.Abs(transform.position.y - startPosition.y) > moveDistance)
        {
            moveDirection *= -1;
        }
    }
}
