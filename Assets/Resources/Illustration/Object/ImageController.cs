using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageController : MonoBehaviour
{
    public RectTransform imageTransform; // 배경 이미지의 RectTransform
    public float imageWidth = 2500f;     // 이미지의 실제 너비
    public float screenWidth = 1920f;    // 화면 너비

    private float maxOffset;             // 이미지가 이동할 수 있는 최대 거리

    void Start()
    {
        // 이미지가 이동할 수 있는 최대 오프셋 계산 (중앙에서 양쪽으로 이동할 수 있는 거리)
        maxOffset = (imageWidth - screenWidth) / 2;
    }

    void Update()
    {
        // 마우스의 X 위치를 0 ~ 1 범위로 변환
        float mouseX = Mathf.Clamp01(Input.mousePosition.x / Screen.width);

        // 이미지의 X 위치를 계산 (중앙을 기준으로 -maxOffset ~ maxOffset까지 이동)
        float targetX = (mouseX - 0.5f) * 2 * (-maxOffset);

        // 이미지 위치를 부드럽게 이동 (Lerp 사용)
        Vector2 newPosition = new Vector2(targetX, imageTransform.anchoredPosition.y);
        imageTransform.anchoredPosition = Vector2.Lerp(imageTransform.anchoredPosition, newPosition, Time.deltaTime * 5f);
    }
}
