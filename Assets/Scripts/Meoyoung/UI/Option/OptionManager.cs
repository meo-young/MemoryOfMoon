using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    [Tooltip("옵션 창")]
    [SerializeField] GameObject optionPanel;

    [Tooltip("소리 조절을 위한 Audio Mixer")]
    [SerializeField] AudioMixer audioMixer;

    [Tooltip("전체 소리 조절을 위한 Slider")]
    [SerializeField] Slider m_MusicMasterSlider;

    [Tooltip("소리를 OFF 하기전 sfx 소리값")]
    [SerializeField] float currentVolumeSFX;

    [Tooltip("소리를 OFF 하기전 bgm 소리값")]
    [SerializeField] float currentVolumeBGM;

    // On 버튼과 Off 버튼의 텍스트 컴포넌트
    [SerializeField] TMP_Text onButtonTextSFX;
    [SerializeField] TMP_Text offButtonTextSFX;

    // On 버튼과 Off 버튼의 텍스트 컴포넌트
    [SerializeField] TMP_Text onButtonTextBGM;
    [SerializeField] TMP_Text offButtonTextBGM;

    [SerializeField] TMP_Text fullResolutionText;
    [SerializeField] TMP_Text windowResolutionText;

    private void Awake()
    {
        m_MusicMasterSlider.onValueChanged.AddListener(SetMasterVolume);
    }

    public void LoadTitle()
    {
        // 게임 재개
        Time.timeScale = 1f;
        AudioListener.pause = false; // 모든 오디오 재개

        // 환경설정 창 비활성화
        optionPanel.SetActive(false);
        SceneManager.LoadScene("TitleScene");
    }

    // 게임 종료 버튼을 눌렀을 때 실행되는 함수
    public void ExitGame()
    {
#if UNITY_EDITOR
        // 에디터에서 실행 중일 때는 게임 종료 대신 에디터를 중지
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 빌드된 게임에서는 Application.Quit() 호출
        Application.Quit();
#endif
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    public void ONBGM()
    {
        audioMixer.SetFloat("BGM", currentVolumeBGM);
        // On 버튼의 텍스트를 불투명하게 (100%)
        SetTextAlpha(onButtonTextBGM, 1.0f);
        // Off 버튼의 텍스트를 50% 투명하게
        SetTextAlpha(offButtonTextBGM, 0.5f);
    }
    public void OffBGM()
    {
        // 현재 볼륨 값을 저장하고, 볼륨을 0으로 설정합니다
        audioMixer.GetFloat("BGM", out currentVolumeBGM);
        audioMixer.SetFloat("BGM", -80f);
        // On 버튼의 텍스트를 불투명하게 (100%)
        SetTextAlpha(onButtonTextBGM, 0.5f);
        // Off 버튼의 텍스트를 50% 투명하게
        SetTextAlpha(offButtonTextBGM, 1.0f);
    }

    public void ONSFX()
    {
        audioMixer.SetFloat("SFX", currentVolumeSFX);
        // On 버튼의 텍스트를 불투명하게 (100%)
        SetTextAlpha(onButtonTextSFX, 1.0f);
        // Off 버튼의 텍스트를 50% 투명하게
        SetTextAlpha(offButtonTextSFX, 0.5f);
    }
    public void OffSFX()
    {
        // 현재 볼륨 값을 저장하고, 볼륨을 0으로 설정합니다
        audioMixer.GetFloat("SFX", out currentVolumeSFX);
        audioMixer.SetFloat("SFX", -80f);
        // On 버튼의 텍스트를 불투명하게 (100%)
        SetTextAlpha(onButtonTextSFX, 0.5f);
        // Off 버튼의 텍스트를 50% 투명하게
        SetTextAlpha(offButtonTextSFX, 1.0f);
    }

    private void SetTextAlpha(TMP_Text textComponent, float alpha)
    {
        Color color = textComponent.color;
        color.a = alpha; // 알파 값 설정
        textComponent.color = color; // 텍스트에 새로운 색상 적용
    }
    public void ResumeGame()
    {
        // 게임 재개
        Time.timeScale = 1f;
        AudioListener.pause = false; // 모든 오디오 재개

        // 환경설정 창 비활성화
        optionPanel.SetActive(false);
    }

    public void OnChangeWindowResolutionButtonClick()
    {
        // 창모드로 설정
        Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.Windowed);
        SetTextAlpha(windowResolutionText, 1.0f);
        SetTextAlpha(fullResolutionText, 0.5f);
    }

    public void OnChangeFullResolutionButtonClick()
    {
        Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.FullScreenWindow);
        SetTextAlpha(fullResolutionText, 1.0f);
        SetTextAlpha(windowResolutionText, 0.5f);
    }
}
