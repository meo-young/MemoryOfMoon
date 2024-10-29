﻿using UnityEngine;

public class SimpleInteractable : MonoBehaviour, IInteractable
{
    public string objectID;
    private MonologueManager _monologueManager;

    void Start()
    {
        _monologueManager = FindObjectOfType<MonologueManager>();
    }
    public void Interact()
    {
        _monologueManager.ShowMonologue(objectID);
    }

}
