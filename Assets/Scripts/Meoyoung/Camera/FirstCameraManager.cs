using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCameraManager : MonoBehaviour
{
    static public FirstCameraManager instance;

    [SerializeField] LayerMask layerMask;
    public GameObject target;
    public float moveSpeed;
    private Vector3 targetPosition;

    public BoxCollider2D currentBound;

    private Vector3 minBound;
    private Vector3 maxBound;

    private float halfWidth;
    private float halfHeight;

    private Camera theCamera;
   

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        if (target.GetComponent<PlayerController>() != null)
            target.GetComponent<PlayerController>().cameraAudioSource = this.GetComponent<AudioSource>();

        theCamera = GetComponent<Camera>();
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
        Collider2D[] colliders = Physics2D.OverlapPointAll(target.transform.position, layerMask);
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

            this.transform.position = targetPosition;
        }
    }

    public void SetBound(BoxCollider2D newBound)
    {
        currentBound = newBound;
        minBound = currentBound.bounds.min;
        maxBound = currentBound.bounds.max;
    }
}
