using System.Collections.Generic;
using UnityEngine;

public class UnlockDoor : MonoBehaviour
{
    [SerializeField] List<Door> doors;
    
    public void UnlockAllDoors()
    {
        foreach (Door door in doors)
        {
            if (door)
            {
                Debug.Log(door.name);
                door.UnlockDoor();
            }
        }
    }
}
