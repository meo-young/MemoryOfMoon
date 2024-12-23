using System;
using UnityEngine;

public class Monologue : MonoBehaviour, IInteraction
{
    public void Interact(Action action)
    {
        MonologueManager.instance.ShowMonologue(this.gameObject.name, action);
    }
}
