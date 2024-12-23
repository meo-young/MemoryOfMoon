using UnityEngine;

public class SimpleInteractable : MonoBehaviour, IInteractable
{
    public string objectID;
    private MonologueManager2 _monologueManager;

    void Start()
    {
        _monologueManager = FindObjectOfType<MonologueManager2>();
    }
    public void Interact()
    {
        _monologueManager.ShowMonologue(objectID);
    }

}
