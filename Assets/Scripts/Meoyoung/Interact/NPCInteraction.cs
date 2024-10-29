using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour, IInteractable
{
    public string objectID;
    public KannaController controller;
    public Transform npc;
    private NPCMonologue _npcMonologue;

    void Start()
    {
        _npcMonologue = FindObjectOfType<NPCMonologue>();
    }
    public void Interact()
    {
        _npcMonologue.ShowNPCMonologue(objectID, npc);
        if (DirectionUtils.CheckDirection(Direction.DOWN)) //플레이어가 말을 건 방향을 쳐다보도록 구현
        {
            controller.anim.SetFloat("DirY", 1.0f);
            controller.anim.SetFloat("DirX", 0.0f);
        }
        else if (DirectionUtils.CheckDirection(Direction.UP))
        {
            controller.anim.SetFloat("DirY", -1.0f);
            controller.anim.SetFloat("DirX", 0.0f);
        }
    }
}
