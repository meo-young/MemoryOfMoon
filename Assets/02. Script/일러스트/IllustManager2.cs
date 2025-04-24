using System;
using System.Collections;
using UnityEngine;

public class IllustManager2 : MonoBehaviour
{
    private PlayerController _playerController;
    private MonologueManager2 _monologueManager;
    private SpriteRenderer spriteRenderer;

    public static IllustManager2 Instance { get; private set; } // Singleton 인스턴스


    private void Awake()
    {
        Instance = this;

        /*if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 전환되어도 파괴되지 않음
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 있다면 새로 생성된 것을 파괴
        }*/
    }

    void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _monologueManager = FindObjectOfType<MonologueManager2>();
        spriteRenderer = _playerController.illust.GetComponent<SpriteRenderer>();
    }

    public void ShowIllust(Sprite image, string objectID)
    {
        StartCoroutine(ActiveIllust(image, objectID));
    }

    //1초동안 일러스트 이미지를 투명도를 조절하여 띄운 후에 독백을 띄우는 상태로 전환
    private IEnumerator ActiveIllust(Sprite image, string objectID)
    {
        string monologue = LoadMonologue.Instance.GetMonologue(objectID);

        _playerController.ChangeState(_playerController._waitState);
        spriteRenderer.sprite = image; // 텍스트 초기화
        _playerController.illustPanel.SetActive(true);
        yield return StartCoroutine(Fade(0f, 1f, 1f));
        yield return new WaitForSeconds(1.0f);
        yield return StartCoroutine(Fade(1f, 0f, 1f));
        _playerController.illustPanel.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        _monologueManager.ShowMonologue(objectID);
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            Color newColor = spriteRenderer.color;
            newColor.a = Mathf.Lerp(startAlpha, endAlpha, t);
            spriteRenderer.color = newColor;
            yield return null;
        }

        // Ensure the final alpha is set correctly
        Color finalColor = spriteRenderer.color;
        finalColor.a = endAlpha;
        spriteRenderer.color = finalColor;
    }
}
