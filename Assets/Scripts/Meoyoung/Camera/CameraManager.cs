using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    static public FirstCameraManager instance;

    [SerializeField] GameObject target;
    [SerializeField] float moveSpeed;
    [SerializeField] Camera theCamera;
    [SerializeField] BoxCollider2D currentBound;

    private Vector3 minBound;
    private Vector3 maxBound;

    private float halfWidth;
    private float halfHeight;

    private Vector3 targetPosition;

    void Start()
    {
        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        if(theCamera == null)
        {
            theCamera = GetComponent<Camera>();
        }
        if(currentBound == null)
        {
            UpdateCurrentBound();
        }
        minBound = currentBound.bounds.min;
        maxBound = currentBound.bounds.max;
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
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
        Collider2D[] colliders = Physics2D.OverlapPointAll(target.transform.position);
        foreach (var collider in colliders)
        {
            if (collider is BoxCollider2D && collider.CompareTag("Camera"))
            {
                SetBound(collider as BoxCollider2D);
                break;
            }
        }
    }

    private void UpdateCameraPosition()
    {
        if (currentBound != null)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);

            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.fixedDeltaTime);

            float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
            float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

            this.transform.position = new Vector3(clampedX, clampedY, this.transform.position.z);
        }
    }

    public void SetBound(BoxCollider2D newBound)
    {
        currentBound = newBound;
        minBound = currentBound.bounds.min;
        maxBound = currentBound.bounds.max;
    }
}
