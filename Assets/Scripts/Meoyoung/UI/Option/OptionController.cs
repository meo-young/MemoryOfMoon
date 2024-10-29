using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionController : MonoBehaviour
{
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private Image option;
    private bool onFlag = false;

    private void Start()
    {
        optionPanel.SetActive(false);
    }

    //옵션 창이 활성화되어있으면 끄고, 아니면 키고
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!onFlag)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }
    public void PauseGame()
    {
        // 게임 일시 정지
        Time.timeScale = 0f;
        AudioListener.pause = true; // 모든 오디오 정지

        // 환경설정 창 활성화
        optionPanel.SetActive(true);
        onFlag = true;
    }

    public void ResumeGame()
    {
        // 게임 재개
        Time.timeScale = 1f;
        AudioListener.pause = false; // 모든 오디오 재개

        // 환경설정 창 비활성화
        optionPanel.SetActive(false);
        onFlag = false;
    }
}
