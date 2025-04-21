using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{   
    public static TitleManager instance;

    [Header("이동할 씬 이름")]
    [SerializeField] private string prologueSceneName;


    [Header("타이틀 텍스트")]
    [SerializeField] private TMP_Text[] titleText;

    [Header("타이틀 텍스트 폰트 사이즈")]
    [SerializeField] private int titleTextFontSize = 40;
    [SerializeField] private int titleTextFontSizeFocused = 46;

    private int currentIndex = 0;
    private bool isTitleTextFocused = false;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // 게임시작 시 FadeIn 연출
        FadeManager2.instance.FadeIn();

        // 게임시작 시 "처음부터" 버튼 포커싱
        SetFocusedTitleText();

        // 타이틀 UI 활성화
        ShowTitleUI();

        // 게임시작 시 타이틀 브금 출력
        SoundManager.instance.PlayBGM(BGM.TITLE);
    }

    private void Update() 
    {
        ControlTitleText();
        SetFocusedTitleText();
    }

    /// <summary>
    /// 타이틀 UI 활성화
    /// </summary>
    public void ShowTitleUI()
    {
        transform.localScale = Vector3.one;
        isTitleTextFocused = true;
    }

    /// <summary>
    /// 타이틀 UI 비활성화
    /// </summary>
    public void HideTitleUI()
    {
        transform.localScale = Vector3.zero;
        isTitleTextFocused = false;
    }
    


    /// <summary>
    /// 타이틀 텍스트 이동을 방향키로 제어
    /// </summary>
    private void ControlTitleText()
    {
        if(!isTitleTextFocused) return;

        // 아래 방향키를 누르는 경우 다음 텍스트로 이동
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            SoundManager.instance.PlaySFX(SFX.UI_Hover);
            currentIndex++;
            if(currentIndex >= titleText.Length)
            {
                currentIndex = 0;
            }
        }

        // 위 방향키를 누르는 경우 이전 텍스트로 이동
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            SoundManager.instance.PlaySFX(SFX.UI_Hover);
            currentIndex--;
            if(currentIndex < 0)
            {
                currentIndex = titleText.Length - 1;
            }
        }


        // 엔터키를 누르는 경우 포커싱된 텍스트에 맞는 함수 호출
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SoundManager.instance.PlaySFX(SFX.UI_Click);
            // 타이틀 UI 비활성화
            isTitleTextFocused = false;

            if(currentIndex == 0)
            {
                LoadPrologueScene();
            }
            else if(currentIndex == 1)
            {
                ShowLoadSceneUI();
            }
            else if(currentIndex == 2)
            {
                QuitGame();
            }
        }
    }


    /// <summary>
    /// 현재 포커싱된 타이틀 텍스트의 속성 변경
    /// </summary>
    private void SetFocusedTitleText()
    {
        // 현재 포커싱된 텍스트의 폰트 사이즈 변경 및 화살표 이미지 활성화
        for(int i = 0; i < titleText.Length; i++)
        {
            if(i == currentIndex)
            {
                titleText[i].fontSize = titleTextFontSizeFocused;
                titleText[i].GetComponentInChildren<Image>().enabled = true;
            }
            else
            {
                titleText[i].fontSize = titleTextFontSize;
                titleText[i].GetComponentInChildren<Image>().enabled = false;
            }
        }
    }



    /// <summary>
    /// 프롤로그 씬 로드
    /// 처음부터 텍스트 선택시 호출할 함수
    /// </summary>
    private void LoadPrologueScene()
    {
        SceneController.instance.LoadScene(prologueSceneName);
    }

    /// <summary>
    /// 불러오기 UI 활성화
    /// </summary>
    private void ShowLoadSceneUI()
    {
        LoadSceneData.instance.ShowLoadSceneUI();
    }

    /// <summary>
    /// 게임 종료
    /// </summary>
    private void QuitGame()
    {
        Application.Quit();
    }
}
