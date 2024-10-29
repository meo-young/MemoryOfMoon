using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairAndDeskMoving : MonoBehaviour
{
    public GameObject chair;
    public GameObject desk;

    private Animator c_anim;
    private Animator d_anim;
    void Start()
    {
        c_anim = chair.GetComponent<Animator>();
        d_anim = desk.GetComponent<Animator>();
    }

    public void MoveCoroutine()
    {
        StartCoroutine(MoveChairAndDesk());
    }

    IEnumerator MoveChairAndDesk()
    {
        d_anim.SetBool("move", true);
        yield return new WaitForSeconds(0.86f);
        c_anim.SetBool("move", true);
    }
}
