using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStepType : MonoBehaviour
{
    [Header("Controller")]
    public PlayerController _playerController;
    public KannaController _kannaController;
    public KentaController _kentaController;
    public KimsinController _kimsinController;

    [Header("StepType")]
    public bool isAfter = false;
    public StepType _stepType;
    public StepType _afterStepType;

    void Start()
    {
        _playerController = FindFirstObjectByType<PlayerController>();
        _kannaController = FindFirstObjectByType<KannaController>();
        _kentaController = FindFirstObjectByType<KentaController>();
        _kimsinController = FindFirstObjectByType<KimsinController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerController.stepType = _stepType;
        }
        else if (collision.CompareTag("Kanna"))
        {
            _kannaController.stepType = _stepType;
        }
        else if (collision.CompareTag("Kenta"))
        {
            _kentaController.stepType = _stepType;
        }
        else if (collision.CompareTag("Kimsin"))
        {
            _kimsinController.stepType = _stepType;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isAfter)
        {
            if (collision.CompareTag("Player"))
            {
                _playerController.stepType = _afterStepType;
            }
            else if (collision.CompareTag("Kanna"))
            {
                _kannaController.stepType = _afterStepType;
            }
            else if (collision.CompareTag("Kenta"))
            {
                _kentaController.stepType = _afterStepType;
            }
            else if (collision.CompareTag("Kimsin"))
            {
                _kimsinController.stepType = _afterStepType;
            }
        }
    }
}
