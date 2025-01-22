using UnityEngine;
using UnityEngine.UI;
using static Constant;

public class Manual : MonoBehaviour
{
    private Image manual;
    private bool canClick = false;

    private void Awake() {
        manual = GetComponentInChildren<Image>();
    }

    private void Start() {
        FadeManager2.instance.FadeOut(
            image : manual,
            onComplete : () => canClick = true
        );
    }

    private void Update() {
        if(!canClick) return;

        if(Input.GetKeyDown(KeyCode.E))
        {
            canClick = false;
            SceneController.instance.LoadScene(SCENE_CHAPTER1);
        }
    }
}
