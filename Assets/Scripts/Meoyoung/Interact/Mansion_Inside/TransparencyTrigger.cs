using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyTrigger : MonoBehaviour
{
    public GameObject targetObject; // ������ ������ ������Ʈ
    public GameObject targetObject2;
    public float fadeDuration = 1.0f; // ���� ��ȭ�� �ɸ��� �ð�
    private Renderer targetRenderer;
    private Renderer targetRenderer2;
    private PlayerController playerController;

    private void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        if (targetObject != null)
        {
            targetRenderer = targetObject.GetComponent<Renderer>();
            targetRenderer2 = targetObject2.GetComponent<Renderer>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && targetRenderer != null) // �÷��̾ Ʈ���ſ� �������� ��
        {
            if (playerController.movement.y < 0)
            {
                //�÷��̾ �Ʒ� �������� �̵� ���̸�
                StopAllCoroutines();
                StartCoroutine(FadeTo(targetRenderer, 1f, fadeDuration));
                StartCoroutine(FadeTo(targetRenderer2, 1f, fadeDuration));
            }
            else if (playerController.movement.y > 0)
            {
                //�÷��̾ �� �������� �̵� ���̸�
                StopAllCoroutines();
                StartCoroutine(FadeTo(targetRenderer, 0.7f, fadeDuration));
                StartCoroutine(FadeTo(targetRenderer2, 0.7f, fadeDuration));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && targetRenderer != null) // �÷��̾ Ʈ���ſ� �������� ��
        {
            if (playerController.movement.y < 0)
            {
                //�÷��̾ �Ʒ� �������� �̵� ���̸�
                StopAllCoroutines();
                StartCoroutine(FadeTo(targetRenderer, 1f, fadeDuration));
                StartCoroutine(FadeTo(targetRenderer2, 1f, fadeDuration));
            }
            else if (playerController.movement.y > 0)
            {
                //�÷��̾ �� �������� �̵� ���̸�
                StopAllCoroutines();
                StartCoroutine(FadeTo(targetRenderer, 0.7f, fadeDuration));
                StartCoroutine(FadeTo(targetRenderer2, 0.7f, fadeDuration));
            }
        }
    }

    IEnumerator FadeTo(Renderer render, float targetAlpha, float duration)
    {
        Color color = render.material.color;
        float startAlpha = color.a;
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            render.material.color = color;
            yield return null;
        }

        color.a = targetAlpha;
        render.material.color = color;
    }
}
