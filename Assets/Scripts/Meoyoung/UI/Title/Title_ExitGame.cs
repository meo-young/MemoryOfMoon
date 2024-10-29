using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_ExitGame : MonoBehaviour 
{
    public void OnQuitButtonClick()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}