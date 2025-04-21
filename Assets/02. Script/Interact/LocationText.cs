using System.Collections;
using TMPro;
using UnityEngine;

public class LocationText : MonoBehaviour
{
    public static LocationText instance;

    [SerializeField] private float duration = 1f;
    private TMP_Text locationName;
    private Coroutine currentCoroutine; // 현재 실행 중인 코루틴 참조

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
        
        locationName = GetComponent<TMP_Text>();
        locationName.rectTransform.localScale = Vector3.zero;
    }

    public void ShowLocationText(string _locationName)
    {
        locationName.text = "<" + _locationName + ">";

        // 실행 중인 코루틴이 있으면 중지
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        // 새로운 코루틴 시작
        currentCoroutine = StartCoroutine(TextAnimation());
    }

    private IEnumerator TextAnimation()
    {
        locationName.rectTransform.localScale = Vector3.one;
        Color color = locationName.color;

        // 0에서 1로 투명도 증가
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / duration);
            locationName.color = color;
            yield return null;
        }

        // 2초 대기
        yield return new WaitForSeconds(1f);

        // 1에서 0으로 투명도 감소
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1 - (elapsedTime / duration));
            locationName.color = color;
            yield return null;
        }

        // 코루틴 종료 시 참조 초기화
        currentCoroutine = null;
        locationName.rectTransform.localScale = Vector3.zero;
    }
}