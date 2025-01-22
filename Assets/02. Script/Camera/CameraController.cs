using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;

    private Camera theCamera;
    private GameObject target;
    private Vector3 targetPosition;
    private Vector3 minBound;
    private Vector3 maxBound;
    private float halfWidth;
    private float halfHeight;
    private BoxCollider2D currentBound;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        theCamera = GetComponent<Camera>();
    }

    private void Start() {
        CalculateCameraSize();
    }

    void LateUpdate()
    {
        if (target != null)
        {
            UpdateCurrentBound();
            UpdateCameraPosition();
        }
    }

    private void UpdateCurrentBound()
    {
        Collider2D collider = Physics2D.OverlapPoint(target.transform.position, layerMask);

        if (collider as BoxCollider2D == currentBound)
            return;

        if (collider != null)
        {
            SetBound(collider as BoxCollider2D);
        }
    }

    private void UpdateCameraPosition()
    {
        if (currentBound != null)
        {
            // 이동할 목표 위치 설정
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);

            // 바운드 안으로 제한
            float clampedX = Mathf.Clamp(targetPosition.x, minBound.x + halfWidth, maxBound.x - halfWidth);
            float clampedY = Mathf.Clamp(targetPosition.y, minBound.y + halfHeight, maxBound.y - halfHeight);

            // 제한된 위치 적용
            this.transform.position = new Vector3(clampedX, clampedY, targetPosition.z);
        }
    }

    public void SetBound(BoxCollider2D newBound)
    {
        currentBound = newBound;
        minBound = currentBound.bounds.min;
        maxBound = currentBound.bounds.max;
    }

    private void CalculateCameraSize()
    {
        float cameraDistance = Mathf.Abs(theCamera.transform.position.z); // 카메라와 평면 사이 거리
        halfHeight = Mathf.Tan(Mathf.Deg2Rad * theCamera.fieldOfView * 0.5f) * cameraDistance;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }
}
