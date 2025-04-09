using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int requiredItems = 8; // Número de objetos necesarios para desbloquear la puerta
    private bool isUnlocked = false; // Estado de la puerta
    public GameObject door; // La puerta que se va a desbloquear

    // Asume que el inventario del jugador se controla con otro script
    private InventoryHandler playerInventory;

    void Start()
    {
        // Obtener la referencia al script del inventario del jugador
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryHandler>();
    }

    void Update()
    {
        // Verifica si el jugador presiona la tecla "E" y tiene suficientes objetos
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
            door.SetActive(false); // Desactiva la puerta (la desbloquea)
            Debug.Log("La puerta ha sido desbloqueada");
        }
    }
}
