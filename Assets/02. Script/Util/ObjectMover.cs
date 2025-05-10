using System.Collections;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public SerializedDictionary<Transform, AnimationClip> MoveTimelineInfo = new (); 
    
    [SerializeField] private AnimationClip lastAnimationClip; // 마지막 애니메이션 클립
    [SerializeField] private float walkSpeed = 0.8f; // 걷는 속도
    [SerializeField] private float runSpeed = 1.4f; // 달리기 속도
    [SerializeField] private GameObject target;
    [SerializeField] private bool isAfterDialog = false;
    
    private Animator animator; // 애니메이터 컴포넌트
    
    public void Move()
    {
        Debug.Log("Move");
        if (MainController.instance)
        {
            MainController.instance.ChangeWaitState();
        }

        if (target)
        {
            animator = target.GetComponent<Animator>();
        }
        
        // 이동 코루틴 시작
        StartCoroutine(MoveThroughPositions());
    }

    private IEnumerator MoveThroughPositions()
    {
        yield return new WaitForSeconds(0.5f);
        
        foreach (var entry in MoveTimelineInfo)
        {
            Transform targetPosition = entry.Key;
            AnimationClip animationClip = entry.Value;
            float speed = animationClip.name.ToLower().Contains("run") ? runSpeed : walkSpeed; // 애니메이션 이름에 따라 속도 결정

            string clipName = animationClip.name.ToLower();

            // 방향 설정
            if (clipName.Contains("left")) SetAnimatorDirection(-1.0f, 0.0f);
            else if (clipName.Contains("right")) SetAnimatorDirection(1.0f, 0.0f);
            else if (clipName.Contains("up")) SetAnimatorDirection(0.0f, 1.0f);
            else if (clipName.Contains("down")) SetAnimatorDirection(0.0f, -1.0f);

            // 상태 설정
            if (clipName.Contains("walk")) SetAnimatorState(AnimatorState.Walk);
            else if (clipName.Contains("run")) SetAnimatorState(AnimatorState.Sprint);
            else SetAnimatorState(AnimatorState.Idle);

            
            // 애니메이션 재생
            if (animator && animationClip)
            {
                animator.Play(animationClip.name);
            }

            // 현재 위치에서 목표 위치로 이동
            while (Vector3.Distance(target.transform.position, targetPosition.position) > 0.001f)
            {
                target.transform.position = Vector3.MoveTowards(target.transform.position, targetPosition.position, speed * Time.deltaTime);
                yield return null;
            }
        }
        
        
        animator.Play(lastAnimationClip.name);
        SetAnimatorState(AnimatorState.Idle);
        
        yield return new WaitForSeconds(0.5f);
        DialogueManager.instance.isTransition = true;
        if (isAfterDialog)
        {
            DialogueManager.instance.ShowDialogue();
        }
        else
        {
            if (MainController.instance)
            {
                MainController.instance.ChangeIdleState();
            }
        }
    }
    
    private enum AnimatorState
    {
        Idle,
        Walk,
        Sprint
    }

    private void SetAnimatorState(AnimatorState state)
    {
        animator.SetBool("Idle", state == AnimatorState.Idle);
        animator.SetBool("Walk", state == AnimatorState.Walk);
        animator.SetBool("Sprint", state == AnimatorState.Sprint);
    }
    
    private void SetAnimatorDirection(float dirX, float dirY)
    {
        animator.SetFloat("DirX", dirX);
        animator.SetFloat("DirY", dirY);
    }
}