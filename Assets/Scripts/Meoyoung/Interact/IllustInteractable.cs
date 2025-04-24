using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllustInteractable : MonoBehaviour, IInteractable
{
    public string objectID;
    public Sprite image;
    private IllustManager2 _illustManager2;

    void Start()
    {
        _illustManager2 = FindObjectOfType<IllustManager2>();
    }
    public void Interact()
    {
        _illustManager2.ShowIllust(image, objectID);
    }
}
