using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Constant;

public class Manual : MonoBehaviour
{
    private Image manual;
    private TMP_Text keepGoing;
    private bool canClick = false;

    private void Awake() {
        manual = GetComponentInChildren<Image>();
        keepGoing = GetComponentInChildren<TMP_Text>();
        keepGoing.gameObject.SetActive(false);
    }

    private void Start() {
        FadeManager2.instance.FadeOut(
            image : manual,
            onComplete : () => 
            {   
                canClick = true;
                keepGoing.gameObject.SetActive(true);
            }
        );
    }

    private void Update() {
        if(!canClick) return;

        if(Input.GetKeyDown(KeyCode.E))
        {
            canClick = false;
            SceneController.instance.LoadScene(SCENE_PROLOG);
        }
    }
}
