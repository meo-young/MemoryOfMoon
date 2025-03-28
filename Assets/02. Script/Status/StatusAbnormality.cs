using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class StatusAbnormality : MonoBehaviour
{
    [System.Serializable]
    public enum AbnormalityType
    {
        Stun = 0,
        Glare = 1,
        Noise = 2
    }

    public enum KeyType
    {
        None = 0,
        Right = 1,
        Left = 2,
    }
    [SerializeField] AbnormalityType type;

    [SerializeField] bool statusFlag = false;


    [Header("# Stun")]
    [SerializeField] float maxGague = 100f;
    [SerializeField] float currentGague = 20f;
    [SerializeField] float descentSpeed = 5f;
    [SerializeField] float increaseQuantity = 5f;
    [SerializeField] KeyType keyType;
    [SerializeField] bool successFlag = false;

    [Header("# Noise")]
    [SerializeField] Renderer2DData rendererData;
    [SerializeField] float activationTime = 3.0f;
    [SerializeField] GameObject globalVolume;

    [Header("# Glare")]
    [SerializeField] SpriteRenderer glareSprite;
    [SerializeField] float glareActivationTime = 2.0f;
    [SerializeField] float glareStartDelay = 0.3f;
    [SerializeField] float glareEndDelay = 3.0f;

    private void Start()
    {
        keyType = KeyType.None;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
            statusFlag = true;

        if (!statusFlag)
            return;

        switch (type)
        {
            case AbnormalityType.Stun:
                Stun();
                break;

            case AbnormalityType.Glare:
                Glare();
                break;

            case AbnormalityType.Noise:
                Noise();
                break;
        }
    }

    void Stun()
    {
        currentGague -= descentSpeed * Time.deltaTime;

        switch (keyType)
        {
            case KeyType.None:
                GetRightInput();
                GetLeftInput();
                break;
            case KeyType.Left:
                GetRightInput();
                break;
            case KeyType.Right:
                GetLeftInput();
                break;
        }

        if (currentGague > maxGague)
        {
            successFlag = true;
            statusFlag = false;
            Debug.Log("����");
        }

        if (currentGague <= 0)
        {
            Debug.Log("����");
        }
    }


    #region Stun Method
    void GetRightInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            PlusCurrentGague();
            keyType = KeyType.Right;
            return;
        }
    }

    void GetLeftInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PlusCurrentGague();
            keyType = KeyType.Left;
            return;
        }
    }


    void PlusCurrentGague()
    {
        currentGague += increaseQuantity;
        Debug.Log(currentGague);
    }

    #endregion


    #region Noise Method
    void Noise()
    {
        for (int i = 0; i < rendererData.rendererFeatures.Count; i++)
            rendererData.rendererFeatures[i].SetActive(true);

        globalVolume.SetActive(true);
        Invoke(nameof(EndNoise), activationTime);
    }

    void EndNoise()
    {
        for (int i = 0; i < rendererData.rendererFeatures.Count; i++)
            rendererData.rendererFeatures[i].SetActive(false);

        globalVolume.SetActive(false);
        statusFlag = false;
    }
    #endregion
    void Glare()
    {
        statusFlag = false;
        FadeIn();
        Invoke(nameof(FadeOut), glareStartDelay + glareActivationTime);
    }

    void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine(glareSprite, glareEndDelay));
    }

    void FadeIn()
    {
        StartCoroutine(FadeInCoroutine(glareSprite, glareStartDelay));
    }


    IEnumerator FadeOutCoroutine(SpriteRenderer _sprite, float _delay)
    {
        Color color = _sprite.color;
        float startAlpha = color.a;
        float elapsedTime = 0f;

        while (elapsedTime < _delay)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / _delay);
            _sprite.color = new Color(color.r, color.g, color.b, newAlpha);
            yield return null;
        }
        color.a = 0;
        _sprite.color = new Color(color.r, color.g, color.b, 0);
    }

    IEnumerator FadeInCoroutine(SpriteRenderer _sprite, float _delay)
    {
        Color color = _sprite.color;
        float startAlpha = color.a;
        float elapsedTime = 0f;

        while (elapsedTime < _delay)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, 1f, elapsedTime / _delay);
            _sprite.color = new Color(color.r, color.g, color.b, newAlpha);
            yield return null;
        }
        color.a = 1;
        _sprite.color = new Color(color.r, color.g, color.b, 1);
    }
}
