using UnityEngine;

public class LayerSetting : MonoBehaviour
{
    private SpriteRenderer targetRenderer;
    private SpriteRenderer playerRenderer;
    void Start()
    {
        targetRenderer = GetComponent<SpriteRenderer>();
        playerRenderer = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (MainController.instance.movement.y < 0)
            targetRenderer.sortingOrder = playerRenderer.sortingOrder - 3;
        else if (MainController.instance.movement.y > 0)
            targetRenderer.sortingOrder = playerRenderer.sortingOrder + 3;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (MainController.instance.movement.y < 0)
            targetRenderer.sortingOrder = playerRenderer.sortingOrder - 3;
        else if (MainController.instance.movement.y > 0)
            targetRenderer.sortingOrder = playerRenderer.sortingOrder + 3;

    }
}
