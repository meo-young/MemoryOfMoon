using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="Item Data", menuName = "Scriptable Object/Item Data")]
public class ItemInfo : ScriptableObject
{
    [SerializeField] string itemName;
    public string ItemName { get { return itemName; } }


    [TextArea][SerializeField] string itemDesc;
    public string ItemDesc { get { return itemDesc; } }


    [SerializeField] Sprite itemImg;
    public Sprite ItemImg { get { return itemImg; } }

}
