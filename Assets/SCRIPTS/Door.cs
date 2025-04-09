using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int requiredItems = 8;
    private bool isUnlocked = false; 
    public GameObject door;

    
    private InventoryHandler playerInventory;

    void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryHandler>();
    }
        

    void Update()
    {
        if (playerInventory._Inventario.Count == 8)
        {
            UnlockDoor();
        }
    }
        

    void UnlockDoor()
    {
        if (!isUnlocked)
        {
            isUnlocked = true;
            door.SetActive(false); 
            Debug.Log("La puerta ha sido desbloqueada");
        }
    }
}
