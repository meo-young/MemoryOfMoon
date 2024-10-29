using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconMover : MonoBehaviour
{
    public RectTransform iconTransform; // 이동할 아이콘의 RectTransform
    public float speed = 5.0f; // 아이콘이 이동하는 속도
    public float height = 50.0f; // 아이콘이 이동할 높이

    private Vector2 originalPosition; // 아이콘의 원래 위치
    private bool movingUp = true; // 아이콘이 위로 이동 중인지 여부

    void Start()
    {
        if (iconTransform == null)
        {
            iconTransform = GetComponent<RectTransform>();
        }
        originalPosition = iconTransform.anchoredPosition;
    }

    void Update()
    {
        float newY = iconTransform.anchoredPosition.y + (movingUp ? speed : -speed) * Time.deltaTime;

        if (movingUp && newY > originalPosition.y + height)
        {
            newY = originalPosition.y + height;
            movingUp = false;
        }
        else if (!movingUp && newY < originalPosition.y)
        {
            newY = originalPosition.y;
            movingUp = true;
        }

        iconTransform.anchoredPosition = new Vector2(iconTransform.anchoredPosition.x, newY);
    }
}
