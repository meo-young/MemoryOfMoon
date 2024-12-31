using System;

public interface IInteraction
{
    public void Interact(Action action);

    void CanInteraction();

    void StopInteraction();
}
