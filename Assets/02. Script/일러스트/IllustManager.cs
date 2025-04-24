using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IllustManager : MonoBehaviour
{
    #region Singleton
    public static IllustManager instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private Image image;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Image[] images = GetComponentsInChildren<Image>();
        image = images[1];
        
        rectTransform.localScale = Vector3.zero;
    }
    
    public void ShowIllust(Sprite sprite, string ItemDesc)
    {
        rectTransform.localScale = Vector3.one;
        image.sprite = sprite;
        StartCoroutine(FadeInOut());
    }
    
    private IEnumerator FadeInOut()
    {
        // 투명도를 0에서 1로 변경
        yield return StartCoroutine(Fade(0f, 1f, 1f));

        // 1초 대기
        yield return new WaitForSeconds(1f);

        // 투명도를 1에서 0으로 변경
        yield return StartCoroutine(Fade(1f, 0f, 1f));

        // 스케일을 0으로 변경
        rectTransform.localScale = Vector3.zero;
        
        // 캐릭터 움직일 수 있게 상태 전환
        MainController.instance.ChangeIdleState();
        //MonologueManager
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            Color newColor = image.color;
            newColor.a = Mathf.Lerp(startAlpha, endAlpha, t);
            image.color = newColor;
            yield return null;
        }

        // 최종 알파값 설정
        Color finalColor = image.color;
        finalColor.a = endAlpha;
        image.color = finalColor;
    }
}
