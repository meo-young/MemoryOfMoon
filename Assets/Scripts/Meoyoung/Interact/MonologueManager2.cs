using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonologueManager2 : MonoBehaviour
{
    public GameObject player; // 플레이어
    private Transform _playerTransform; //말풍선의 상대적인 위치를 위한 플레이어의 위치
    private Vector3 offset = new(0, 15f, 0); // 말풍선의 위치 오프셋
    public PlayerController _playerController;

    public static MonologueManager2 Instance { get; private set; } // Singleton 인스턴스

    private WaitForSeconds typingTime = new WaitForSeconds(0.05f);


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _playerTransform = player.transform;
        _playerController = player.GetComponent<PlayerController>();
    }

    public void ShowMonologue(string objectID)
    {
        StartCoroutine(ActiveMonologue(objectID));
    }

    private IEnumerator ActiveMonologue(string objectID)
    {
        string dialogue = LoadMonologue.Instance.GetMonologue(objectID);

        _playerController.ChangeState(_playerController._waitState);
        _playerController.monologuePanel.SetActive(true);
        _playerController.monologuePanel.GetComponentInChildren<TMP_Text>().text = ""; // 텍스트 초기화
        _playerController.monologuePanel.transform.position = _playerTransform.position + offset;

        foreach (char letter in dialogue)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _playerController.monologuePanel.GetComponentInChildren<TMP_Text>().text = dialogue;
                break;
            }
            _playerController.monologuePanel.GetComponentInChildren<TMP_Text>().text += letter;
            yield return typingTime;
        }
        _playerController.ChangeState(_playerController._monoState);
    }
}
