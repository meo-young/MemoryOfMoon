using System;
using UnityEngine;

public class Monologue : MonoBehaviour, IInteraction
{
    private GameObject arrow;

    public void Interact(Action action)
    {
        MonologueManager.instance.ShowMonologue(this.gameObject.name, action);
    }

    public void CanInteraction()
    {
        if (arrow == null)
            return;

        arrow.SetActive(true);
    }

    public void StopInteraction()
    {
        if (arrow == null)
            return;

        arrow.SetActive(false);
    }


}
