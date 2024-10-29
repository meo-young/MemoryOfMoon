using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCMonologue : MonoBehaviour
{
    public GameObject player; // 플레이어
    public Transform npc; //말풍선의 상대적인 위치를 위한 플레이어의 위치
    private Vector3 offset = new(0, 10f, 0); // 말풍선의 위치 오프셋
    public PlayerController _playerController;
    void Start()
    {
        _playerController = player.GetComponent<PlayerController>();
    }

    public void ShowNPCMonologue(string objectID, Transform npc)
    {
        StartCoroutine(ActiveNPCMonologue(objectID, npc));
    }

    // Update is called once per frame
    private IEnumerator ActiveNPCMonologue(string objectID, Transform npc)
    {
        string dialogue = LoadMonologue.Instance.GetMonologue(objectID);

        _playerController.ChangeState(_playerController._waitState);
        _playerController.monologuePanel.SetActive(true);
        _playerController.monologuePanel.GetComponentInChildren<TMP_Text>().text = ""; // 텍스트 초기화
        _playerController.monologuePanel.transform.position = npc.position + offset;

        foreach (char letter in dialogue)
        {
            _playerController.monologuePanel.GetComponentInChildren<TMP_Text>().text += letter;
            yield return new WaitForSeconds(0.05f); // 지연 시간 대기
        }
        _playerController.ChangeState(_playerController._monoState);
    }
}
