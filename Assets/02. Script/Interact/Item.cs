using UnityEngine;

public class Item : MonoBehaviour, IInteractable, IInteraction
{
    [SerializeField] private ItemInfo itemInfo;
    
    private GameObject arrow;
    
    protected void Awake()
    {
        arrow = transform.GetChild(0).gameObject;
        
        if (arrow.activeSelf)
        {
            arrow.SetActive(false);   
        }
    }
    
    public void Interact()
    {
        SoundManager.instance.PlaySFX(SFX.KEY);
        InventoryManager.instance.AddItem(itemInfo);
        IllustManager.instance.ShowIllust(itemInfo.ItemImg, itemInfo.ItemDesc);
        Destroy(gameObject);
    }

    public void CanInteraction()
    {
        arrow.SetActive(true);
    }

    public void StopInteraction()
    {
        arrow.SetActive(false);
    }
}
