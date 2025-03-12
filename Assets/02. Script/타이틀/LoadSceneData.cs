using UnityEngine;
using UnityEngine.UI;

public class LoadSceneData : MonoBehaviour
{
    public static LoadSceneData instance;

    [SerializeField] private SceneData[] sceneDatas;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform contentRectTransform;

    private int currentSceneIndex = 0;
    private int currentCheckPointIndex = 0;
    private bool isLoadSceneUIActive = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetFocusedCheckPointImage();
    }

    private void Update()
    {
        ControlCheckPointImage();
    }

    /// <summary>
    /// 불러오기 UI 활성화
    /// </summary>
    public void ShowLoadSceneUI()
    {
        transform.localScale = Vector3.one;
        isLoadSceneUIActive = true;
    }

    /// <summary>
    /// 불러오기 UI 비활성화
    /// </summary>
    public void HideLoadSceneUI()
    {
        transform.localScale = Vector3.zero;
        isLoadSceneUIActive = false;
    }

    /// <summary>
    /// 방향키로 체크 포인트 이미지 포커싱 제어
    /// </summary>
    private void ControlCheckPointImage()
    {
        if(!isLoadSceneUIActive) return;

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TitleManager.instance.ShowTitleUI();
            HideLoadSceneUI();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveCheckPoint(ref currentSceneIndex, 1);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveCheckPoint(ref currentSceneIndex, -1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveCheckPoint(ref currentCheckPointIndex, 1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveCheckPoint(ref currentCheckPointIndex, -1);
        }
    }

    /// <summary>
    /// 체크포인트 이동 처리
    /// </summary>
    private void MoveCheckPoint(ref int index, int direction)
    {
        InitializeFocusedCheckPointImage();
        
        index += direction;
        
        if (index < 0)
        {
            index = (index == currentSceneIndex) ? sceneDatas.Length - 1 : sceneDatas[currentSceneIndex].checkPointImages.Length - 1;
        }
        else if (index >= (index == currentSceneIndex ? sceneDatas.Length : sceneDatas[currentSceneIndex].checkPointImages.Length))
        {
            index = 0;
        }

        ValidateCheckPointIndex();
        SetFocusedCheckPointImage();

        if (index == currentSceneIndex)
        {
            AdjustScrollPosition();
        }
    }

    /// <summary>
    /// 체크포인트 인덱스 유효성 검사
    /// </summary>
    private void ValidateCheckPointIndex()
    {
        if (currentCheckPointIndex >= sceneDatas[currentSceneIndex].checkPointImages.Length)
        {
            currentCheckPointIndex = 0;
        }
        else if (currentCheckPointIndex < 0)
        {
            currentCheckPointIndex = sceneDatas[currentSceneIndex].checkPointImages.Length - 1;
        }
    }

    /// <summary>
    /// 현재 체크 포인트 이미지 포커싱
    /// </summary>
    private void SetFocusedCheckPointImage()
    {
        sceneDatas[currentSceneIndex].checkPointImages[currentCheckPointIndex].enabled = false;
    }

    /// <summary>
    /// 이전 체크 포인트 이미지 포커싱 초기화
    /// </summary>
    private void InitializeFocusedCheckPointImage()
    {
        sceneDatas[currentSceneIndex].checkPointImages[currentCheckPointIndex].enabled = true;
    }

    /// <summary>
    /// 현재 선택된 아이템이 보이도록 스크롤 위치 조정
    /// </summary>
    private void AdjustScrollPosition()
    {
        float contentHeight = contentRectTransform.rect.height;
        float viewportHeight = scrollRect.viewport.rect.height;
        float itemHeight = contentHeight / sceneDatas.Length;
        
        float normalizedPosition = 1f - ((currentSceneIndex * itemHeight) / (contentHeight - viewportHeight));
        normalizedPosition = Mathf.Clamp01(normalizedPosition);
        
        scrollRect.verticalNormalizedPosition = normalizedPosition;
    }
}

[System.Serializable]
public class SceneData
{
    public Image[] checkPointImages;
}
