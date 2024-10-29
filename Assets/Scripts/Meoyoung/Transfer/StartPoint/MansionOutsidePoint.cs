using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MansionOutsidePoint : MonoBehaviour
{
    private PlayerController player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        if (player != null)
        {
            player.transform.position = transform.position;
        }

        player.ChangeState(player._waitState);
    }
}
