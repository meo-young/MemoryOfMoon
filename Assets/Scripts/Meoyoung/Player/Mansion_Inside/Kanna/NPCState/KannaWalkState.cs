using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class KannaWalkState : MonoBehaviour, IKannaState
{
    private KannaController _npcController;
    private PlayerController pc;

    private Transform[] waypoints;  // 이동할 경로 지점들
    private float moveSpeed;   // 이동 속도
    private float waitTime;    // 각 지점에서 대기 시간

    private int currentWaypointIndex = 0;
    public void OnStateEnter(KannaController npcController)
    {
        if(!_npcController)
            _npcController = npcController;

        pc = _npcController._playerController;

        _npcController.anim.SetBool("Walk", true);
        waypoints = _npcController.waypoints;
        moveSpeed = _npcController.moveSpeed;
        waitTime = _npcController.waitTime;

        if(pc.currentDialogueCounter < 35)
        {
            StartCoroutine(Move());
        }
    }


    public void OnStateUpdate()
    {
        if (_npcController)
        {
            if (currentWaypointIndex == 0)
            {
                _npcController.anim.SetFloat("DirY", -1.0f);
            }
            else if (currentWaypointIndex == 1)
            {
                _npcController.anim.SetFloat("DirY", 0.0f);
                _npcController.anim.SetFloat("DirX", 1.0f);
            }
            else if (currentWaypointIndex == 2)
            {
                _npcController.anim.SetFloat("DirY", 1.0f);
                _npcController.anim.SetFloat("DirX", 0.0f);
            }
            else if(currentWaypointIndex == 3)
            {
                _npcController.anim.SetFloat("DirY", 0.0f);
                _npcController.anim.SetFloat("DirX", 1.0f);
            }
            else
            {
                //Debug.Log("칸나 일반 이동");
            }
        }
    }
    public void OnStateExit()
    {
        _npcController.anim.SetBool("Walk", false);
    }

    IEnumerator Move()
    {
        while (true)
        {
            if (currentWaypointIndex < waypoints.Length)
            {
                // 다음 지점으로 이동
                yield return StartCoroutine(MoveToNextWaypoint());

                // 지정된 시간만큼 대기
                yield return new WaitForSeconds(waitTime);

                currentWaypointIndex++;
            }
            else
            {
                // 모든 지점을 순회했으면 처음으로 돌아감
                this.transform.position = new(10.0f, -355f, 0.0f);
                _npcController._audioSource.clip = _npcController.doorOpenSound;
                _npcController._audioSource.Play();
                _npcController.goToLivingRoom = false;

                _npcController.anim.SetFloat("DirX", -1.0f);
                _npcController.anim.SetFloat("DirY", 0.0f);

                _npcController.ChangeState(_npcController._idleState);
                _npcController.gameObject.GetComponent<Renderer>().sortingLayerName = "Object";
                _npcController.gameObject.GetComponent<Renderer>().sortingOrder = 3;
                _npcController.boxCollider.SetActive(true);
                _npcController._layerController.enabled = true; //layer Controller 작동하도록 함

                pc.maxDialogueCounter = 32;
                pc._dialogueManager.ShowDialogue(pc.currentDialogueCounter.ToString());
                break;
            }
        }
    }

    IEnumerator MoveToNextWaypoint()
    {
        while (transform.position != waypoints[currentWaypointIndex].position)
        {
            // 현재 위치에서 다음 지점으로 부드럽게 이동
            transform.position = Vector3.MoveTowards(transform.position,
                                                     waypoints[currentWaypointIndex].position,
                                                     moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
