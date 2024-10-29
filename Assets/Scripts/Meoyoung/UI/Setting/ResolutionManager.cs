using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionManager : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;

    private List<Resolution> resolutions = new List<Resolution>();
    private int optimalResolutionIndex = 0;

    private void Start()
    {
        resolutions.Add(new Resolution { width = 800, height = 600 });
        resolutions.Add(new Resolution { width = 1280, height = 720 });
        resolutions.Add(new Resolution { width = 1280, height = 800 });
        resolutions.Add(new Resolution { width = 1440, height = 900 });
        resolutions.Add(new Resolution { width = 1920, height = 1080 });
        resolutions.Add(new Resolution { width = 1920, height = 1200 });


        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Count; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            // ���� ������ �ػ󵵿� ��ǥ�� ǥ���մϴ�.
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                optimalResolutionIndex = i;
                option += " *";
            }
            options.Add(option);
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = optimalResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // ������ ���� ������ �ػ󵵷� ���۵ǵ��� �����մϴ�.
        SetResolution(optimalResolutionIndex);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void OnChangeWindowResolutionButtonClick()
    {
        // â���� ����
        Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.Windowed);
    }

    public void OnChangeFullResolutionButtonClick()
    {
        Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.FullScreenWindow);
    }
}
