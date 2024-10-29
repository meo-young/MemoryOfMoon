using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCMonologue : MonoBehaviour
{
    public GameObject player; // �÷��̾�
    public Transform npc; //��ǳ���� ������� ��ġ�� ���� �÷��̾��� ��ġ
    private Vector3 offset = new(0, 10f, 0); // ��ǳ���� ��ġ ������
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
        _playerController.monologuePanel.GetComponentInChildren<TMP_Text>().text = ""; // �ؽ�Ʈ �ʱ�ȭ
        _playerController.monologuePanel.transform.position = npc.position + offset;

        foreach (char letter in dialogue)
        {
            _playerController.monologuePanel.GetComponentInChildren<TMP_Text>().text += letter;
            yield return new WaitForSeconds(0.05f); // ���� �ð� ���
        }
        _playerController.ChangeState(_playerController._monoState);
    }
}
