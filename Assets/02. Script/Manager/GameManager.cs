using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Player")]
    [HideInInspector] public GameObject player;

    private void Awake()
    {
        if(instance == null)
            instance = this;

        InitVariables();
    }

    private void InitVariables()
    {
        player = GameObject.FindWithTag("Player");
    }
}
