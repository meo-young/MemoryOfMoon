using System;
using UnityEngine;

public class Monologue : MonoBehaviour, IInteraction
{
    private GameObject arrow;

    public void Interact()
    {
        MonologueManager.instance.ShowMonologue(this.gameObject.name);
    }

    public void CanInteraction()
    {
        if (arrow)
            return;

        arrow.SetActive(true);
    }

    public void StopInteraction()
    {
        if (arrow)
            return;

        arrow.SetActive(false);
    }


}
