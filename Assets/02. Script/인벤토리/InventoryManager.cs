using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    #region Singleton
    public static InventoryManager instance;
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
    
    [Tooltip("인벤토리에서 보여줄 프리팹")]
    [SerializeField] private GameObject invenPrefab;
    [Tooltip("GridLayoutGroup이 있는 부모 오브젝트")]
    [SerializeField] private Transform invenGridParent; // GridLayoutGroup이 있는 부모 오브젝트

    [Tooltip("인벤토리")]
    [SerializeField] GameObject iv;

    private Dictionary<ItemInfo, int> inventory = new();

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.I))
        {
            iv.SetActive(!iv.activeSelf);
        }*/
    }
    public void AddItem(ItemInfo itemName)
    {
        if (inventory.ContainsKey(itemName))
        {
            Debug.Log(itemName + "이(가) 인벤토리에 추가되었습니다.");
            inventory[itemName] += 1;
        }
        else
        {
            Debug.Log(itemName + "을 처음으로 습득했습니다.");
            inventory[itemName] = 1; // 처음 추가될 때 0으로 초기화 후 1을 더함
        }
        //PrintInventory();
    }

    public void RemoveItem(ItemInfo itemName)
    {
        if (inventory.ContainsKey(itemName))
        {
            Debug.Log(itemName + "이(가) 인벤토리에서 1개 제거되었습니다.");
            inventory[itemName] -= 1;

            // 아이템 수가 0이 되면 딕셔너리에서 제거
            if (inventory[itemName] <= 0)
            {
                inventory.Remove(itemName);
            }
        }
        //PrintInventory();
    }

    public void RemoveAllItems()
    {
        // inventory의 모든 키(아이템 종류)를 리스트로 저장
        List<ItemInfo> keys = new List<ItemInfo>(inventory.Keys);

        // for 문을 사용하여 모든 아이템의 개수를 0으로 설정
        for (int i = 0; i < keys.Count; i++)
        {
            ItemInfo item = keys[i];
            inventory[item] = 0;
            Debug.Log(item + "의 수량이 0으로 설정되었습니다.");
        }

        // 0이 된 모든 아이템을 인벤토리에서 제거
        inventory.Clear();
        PrintInventory();
        Debug.Log("인벤토리에서 모든 아이템이 제거되었습니다.");
    }

    public bool HasItem(ItemInfo itemName)
    {
        Debug.Log(itemName + "을(를) 소지하고 있는지 확인합니다.");
        return inventory.ContainsKey(itemName);
    }

    public void PrintInventory()
    {
        // 딕셔너리의 키와 값을 각각 리스트로 변환
        List<ItemInfo> keys = new List<ItemInfo>(inventory.Keys);
        List<int> values = new List<int>(inventory.Values);

        // 자식 오브젝트들을 순회하면서 제거
        for (int i = invenGridParent.childCount - 1; i >= 0; i--)
        {
            GameObject child = invenGridParent.GetChild(i).gameObject;
            Destroy(child);
        }

        for (int i = 0; i < keys.Count; i++)
        {
            ItemInfo item = keys[i];
            int quantity = values[i];

            // 슬롯을 인스턴스화
            GameObject inven = Instantiate(invenPrefab, invenGridParent);
            Debug.Log(item.ItemName + item.ItemDesc);
            // Name 텍스트 설정
            TextMeshProUGUI nameText = inven.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            nameText.text = item.ItemName;

            // Image 컴포넌트 설정
            Image itemImage = inven.transform.Find("Image").GetComponent<Image>();
            itemImage.sprite = item.ItemImg; // 아이템에 맞는 스프라이트 할당

            // Count 텍스트 설정
            TextMeshProUGUI countText = inven.transform.Find("Count").GetComponent<TextMeshProUGUI>();
            countText.text = quantity.ToString() + "개";
        }
    }
}
