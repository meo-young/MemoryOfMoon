using UnityEngine;

public class ObjectTransfer : MonoBehaviour
{
    [SerializeField] private Transform targetPosition; // 전송할 위치
    [SerializeField] private Vector2 targetDirection;

    public void Transfer()
    {
        MainController.instance.gameObject.transform.position = targetPosition.position;
        MainController.instance.anim.SetFloat("DirX", targetDirection.x);
        MainController.instance.anim.SetFloat("DirY", targetDirection.y);
    }
}
