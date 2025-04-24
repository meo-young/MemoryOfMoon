using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<string> inventory = new List<string>();

    public void AddItem(string itemName)
    {
        Debug.Log(itemName + "이(가) 인벤토리에 추가되었습니다.");
        inventory.Add(itemName);
    }

    public void RemoveItem(string itemName)
    {
        Debug.Log(itemName + "이(가) 인벤토리에 제거되었습니다.");
        inventory.Remove(itemName);
    }

    public bool HasItem(string itemName)
    {
        Debug.Log(itemName + "을(를) 소지하고 있는지 확인합니다.");
        return inventory.Contains(itemName);
    }

    public void ClearInventory()
    {
        Debug.Log("인벤토리의 모든 아이템이 삭제되었습니다.");
        inventory.Clear();
    }
}
