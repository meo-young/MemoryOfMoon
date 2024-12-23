using UnityEngine;

public class LayerSetting : MonoBehaviour
{
    private SpriteRenderer targetRenderer;
    private SpriteRenderer playerRenderer;

    private int originSortingOrder;

    void Start()
    {
        targetRenderer = GetComponentInParent<SpriteRenderer>();
        playerRenderer = GameManager.instance.player.GetComponent<SpriteRenderer>();

        originSortingOrder = targetRenderer.sortingOrder;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (MainController.instance.movement.y < 0)
            targetRenderer.sortingOrder = originSortingOrder - playerRenderer.sortingOrder;
        else if (MainController.instance.movement.y > 0)
            targetRenderer.sortingOrder = originSortingOrder + playerRenderer.sortingOrder;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (MainController.instance.movement.y < 0)
            targetRenderer.sortingOrder = originSortingOrder - playerRenderer.sortingOrder;
        else if (MainController.instance.movement.y > 0)
            targetRenderer.sortingOrder = originSortingOrder + playerRenderer.sortingOrder;

    }
}
