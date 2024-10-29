﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryKey : MonoBehaviour, IInteractable
{
    public  string keyItemName = "LibraryDoorKey";
    private bool   isSearched = false;
    public string afterObjectID;
    public string objectID;
    public Sprite image;
    private IllustManager _illustManager;
    private PlayerInventory playerInventory;
    private MonologueManager _monologueManager;
    private PlayerController _playerController;
    private InventoryManager inventoryManager;

    [Tooltip("서재 열쇠 아이템")]
    [SerializeField] ItemInfo libraryKey;

    [SerializeField] GameObject particle;

    void Start()
    {
        _monologueManager = FindObjectOfType<MonologueManager>();
        playerInventory = FindObjectOfType<PlayerInventory>();
        _illustManager = FindObjectOfType<IllustManager>();
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
            _illustManager.ShowIllust(image, afterObjectID);
            particle.SetActive(false);
        }
        else
        {
            _monologueManager.ShowMonologue(objectID);
        }
    }
}
