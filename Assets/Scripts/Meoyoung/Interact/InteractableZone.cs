using UnityEngine;

public class InteractableZone : MonoBehaviour
{
    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log(gameObject.name);
            GetInteractScript();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RemoveInteractScript();
        }
    }

    private void GetInteractScript()
    {
        _playerController.interactable = this.GetComponent<IInteractable>();
        _playerController.interactRange = true;
    }

    private void RemoveInteractScript()
    {
        _playerController.interactRange = false;
        if(_playerController.interactable != null)
        {
            _playerController.interactable = null;
        }
    }
}
