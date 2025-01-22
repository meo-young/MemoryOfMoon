using UnityEngine;

public class FadeLayerSetting : MonoBehaviour
{
    private SpriteRenderer targetRenderer;

    private int originSortingOrder;

    private void Awake() {
        targetRenderer = GetComponentInParent<SpriteRenderer>();        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (MainController.instance.movement.y < 0)
            FadeManager2.instance.FadeOut(targetRenderer);
        else if (MainController.instance.movement.y > 0)
            FadeManager2.instance.FadeIn(targetRenderer);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (MainController.instance.movement.y < 0)
            FadeManager2.instance.FadeOut(targetRenderer);
        else if (MainController.instance.movement.y > 0)
            FadeManager2.instance.FadeIn(targetRenderer);

    }
}
