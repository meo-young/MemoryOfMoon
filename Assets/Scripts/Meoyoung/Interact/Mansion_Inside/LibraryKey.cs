using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryKey : MonoBehaviour, IInteractable
{
    public  string keyItemName = "LibraryDoorKey";
    private bool   isSearched = false;
    public string afterObjectID;
    public string objectID;
    public Sprite image;
    private IllustManager2 _illustManager2;
    private PlayerInventory playerInventory;
    private MonologueManager2 _monologueManager;
    private PlayerController _playerController;
    private InventoryManager inventoryManager;

    [Tooltip("서재 열쇠 아이템")]
    [SerializeField] ItemInfo libraryKey;

    [SerializeField] GameObject particle;

    void Start()
    {
        _monologueManager = FindObjectOfType<MonologueManager2>();
        playerInventory = FindObjectOfType<PlayerInventory>();
        _illustManager2 = FindObjectOfType<IllustManager2>();
        inventoryManager = FindObjectOfType<InventoryManager>();
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (!particle.activeSelf)
        {
            particle.SetActive(true);
        }
    }

    public void Interact()
    {
        if (!isSearched)
        {
            isSearched = true;
            playerInventory.AddItem(keyItemName);
            inventoryManager.AddItem(libraryKey);
            _playerController.GetKeySound();
            _illustManager2.ShowIllust(image, afterObjectID);
            particle.SetActive(false);
        }
        else
        {
            _monologueManager.ShowMonologue(objectID);
        }
    }
}
