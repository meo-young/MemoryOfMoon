using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    void Awake()
    {
        // 현재 씬에 동일한 UI 오브젝트가 있는지 확인
        if (instance != null)
        {
            Destroy(gameObject);  // 이미 인스턴스가 있다면, 새로 생성된 오브젝트를 파괴
            return;
        }
        else
        {
            instance = this;  // 이 오브젝트를 유일한 인스턴스로 설정
            DontDestroyOnLoad(gameObject);  // 씬이 바뀌어도 이 오브젝트를 유지
        }
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;  // 씬이 로드될 때마다 호출되는 이벤트 등록
    }

    // 씬이 로드될 때 호출되는 메서드
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 타이틀 씬이라면 현재 UI 오브젝트 파괴
        if (scene.name == "TitleScene")
        {
            Destroy(gameObject);
        }
        else
        {
            // 새로 로드된 씬에 UIManager가 또 있으면 현재 오브젝트 파괴
            if (FindObjectsOfType<UIManager>().Length > 1)
            {
                Destroy(gameObject);
            }
        }
    }

    // 오브젝트가 파괴될 때 이벤트 해제
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
