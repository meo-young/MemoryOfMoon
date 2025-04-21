using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static Constant;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void LoadScene(string sceneName)
    {
        FadeManager2.instance.FadeOut(
            onComplete : () => SceneManager.LoadScene(sceneName)
        );
    }
}
