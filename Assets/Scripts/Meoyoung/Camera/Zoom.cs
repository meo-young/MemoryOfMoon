using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Zoom : MonoBehaviour
{
    private const float DirectionForceReduceRate = 0.935f; // 감속비율
    private const float DirectionForceMin = 0.001f; // 설정치 이하일 경우 움직임을 멈춤

    private bool _userMoveInput; // 현재 조작을 하고있는지 확인을 위한 변수
    private Vector3 _startPosition;  // 입력 시작 위치를 기억
    private Vector3 _directionForce; // 조작을 멈췄을때 서서히 감속하면서 이동 시키기 위한 변수

    private Camera _camera;

    public float zoomInSize = 3f; // 줌인 크기
    public float zoomOutSize = 5f; // 줌아웃 크기
    public float zoomSpeed = 5f; // 줌 속도

    private void ControlCameraPosition()
    {
        var mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
            CameraPositionMoveStart(mouseWorldPosition);
        else if (Input.GetMouseButton(0))
            CameraPositionMoveProgress(mouseWorldPosition);
        else
            CameraPositionMoveEnd();
    }

    private void CameraPositionMoveStart(Vector3 startPosition) 
    {
        _userMoveInput = true;
        _startPosition = startPosition;
        _directionForce = Vector2.zero;
    }

    private void CameraPositionMoveProgress(Vector3 targetPosition)
    {
        if (!_userMoveInput)
        {
            CameraPositionMoveStart(targetPosition);
            return;
        }

        _directionForce = _startPosition - targetPosition;

        // 줌인
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, zoomInSize, Time.deltaTime * zoomSpeed);
    }

    private void CameraPositionMoveEnd()
    {
        _userMoveInput = false;

        // 줌아웃
        StartCoroutine(ZoomOut());
    }

    private IEnumerator ZoomOut()
    {
        while (_camera.orthographicSize < zoomOutSize - 0.01f)
        {
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, zoomOutSize, Time.deltaTime * zoomSpeed);
            yield return null;
        }
        _camera.orthographicSize = zoomOutSize; // 정확히 줌아웃 크기로 설정
    }

    private void ReduceDirectionForce()
    {
        // 조작 중일때는 아무것도 안함
        if (_userMoveInput)
            return;
        
        // 감속 수치 적용
        _directionForce *= DirectionForceReduceRate;
        
        // 작은 수치가 되면 강제로 멈춤
        if (_directionForce.magnitude < DirectionForceMin)
            _directionForce = Vector3.zero;
    }

    private void UpdateCameraPosition()
    {
        // 이동 수치가 없으면 아무것도 안함
        if (_directionForce == Vector3.zero)
            return;
        
        var currentPosition = transform.position;
        var targetPosition = currentPosition + _directionForce;
        transform.position = Vector3.Lerp(currentPosition, targetPosition, 0.5f);
    }

    void Start()
    {
        _camera = GetComponent<Camera>();
    }
    void Update()
    {
        // 카메라 포지션 이동
        ControlCameraPosition();
        // 조작을 멈췄을때 감속
        ReduceDirectionForce();
        // 카메라 위치 업데이트
        UpdateCameraPosition();
    }
}
