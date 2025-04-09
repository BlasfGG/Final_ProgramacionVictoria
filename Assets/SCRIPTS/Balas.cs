using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balas : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            Destroy(collision.gameObject); // Destruye la collision
            Destroy(this.gameObject); // Destruye la bala 
            
        }
    }
}
