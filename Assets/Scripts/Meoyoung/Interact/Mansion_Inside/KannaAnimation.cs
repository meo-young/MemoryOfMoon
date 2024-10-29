using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KannaAnimation : MonoBehaviour, IInteractable
{
    public string objectID;
    public string itemName = "KannaInteract";
    public Sprite image;

    private bool isSearched = false;
    private IllustManager _illustManager;
    private PlayerController _playerController;
    private PlayerInventory playerInventory;

    [SerializeField] GameObject particle;


    void Start()
    {
        _illustManager = FindObjectOfType<IllustManager>();
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerInventory = FindObjectOfType<PlayerInventory>();
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
            _playerController.PaperSound();
            _illustManager.ShowIllust(image, objectID);
            _playerController.kannaAnim = true;
            playerInventory.AddItem(itemName);
            particle.SetActive(false);
        }
        else
        {
            _playerController.PaperSound();
            _illustManager.ShowIllust(image, objectID);
        }
    }

}
