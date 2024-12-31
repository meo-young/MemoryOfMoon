using System;
using UnityEngine;

public class Door : MonoBehaviour, IInteraction
{
    private GameObject arrow;

    private void Awake()
    {
        arrow = transform.GetChild(0).gameObject;

        if(arrow.activeSelf)
            arrow.SetActive(false);
    }
    public void Interact(Action action)
    {

    }

    public void CanInteraction()
    {
        arrow.SetActive(true);
    }

    public void StopInteraction()
    {
        arrow.SetActive(false);
    }
}
