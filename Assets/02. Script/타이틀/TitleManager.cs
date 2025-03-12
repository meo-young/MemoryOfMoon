using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [Header("타이틀 텍스트")]
    [SerializeField] private TMP_Text[] titleText;

    [Header("타이틀 텍스트 폰트 사이즈")]
    [SerializeField] private int titleTextFontSize = 40;
    [SerializeField] private int titleTextFontSizeFocused = 46;

    private int currentIndex = 0;

    void Start()
    {
        // 게임시작 시 "처음부터" 버튼 포커싱
        SetFocusedTitleText();
    }

    private void Update() 
    {
        ControlTitleText();
        SetFocusedTitleText();
    }
    
    private void ControlTitleText()
    {
        // 아래 방향키를 누르는 경우 다음 텍스트로 이동
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentIndex++;
            if(currentIndex >= titleText.Length)
            {
                currentIndex = 0;
            }
        }

        // 위 방향키를 누르는 경우 이전 텍스트로 이동
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentIndex--;
            if(currentIndex < 0)
            {
                currentIndex = titleText.Length - 1;
            }
        }
    }

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
}
