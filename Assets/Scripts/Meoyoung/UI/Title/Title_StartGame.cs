using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Title_StartGame : MonoBehaviour 
{
    public string sceneName = "MansionScene_Opening";

    public void StartBtn() 
    {
        Invoke("LoadScene", 1.5f);
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}