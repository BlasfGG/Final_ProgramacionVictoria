using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform thePlayer;  // Referencia al jugador

    [Header("Explosi�n")]
    [SerializeField] private float radio = 3f;  // Radio de detecci�n del jugador
    public LayerMask playerMask;               // Capa para detectar al jugador
    public int damageToPlayer = 20;            // Da�o que inflige al explotar
    public ParticleSystem explosionEffect;     // Efecto visual de explosi�n

    [Header("Vida")]
    public int maxHealth = 100;                // Vida m�xima del enemigo
    private int currentHealth;                 // Vida actual
    public float pushBackForce = 5f;           // Fuerza de retroceso al recibir da�o

    private PlayerHealth playerHealth;         // Referencia a la salud del jugador
    private bool hasExploded = false;          // Para que no explote m�s de una vez

    private void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = thePlayer.GetComponent<PlayerHealth>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (hasExploded) return;

        // Detecta si el jugador est� dentro del radio para explotar
        Collider[] hits = Physics.OverlapSphere(transform.position, radio, playerMask);
        if (hits.Length > 0)
        {
            Explode();
        }
    }

    public void TakeDamage(int damage)
    {
        if (hasExploded) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            PushBack();
        }
    }

    private void PushBack()
    {
        Vector3 direction = transform.position - thePlayer.position;
        direction.y = 0;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(direction.normalized * pushBackForce, ForceMode.Impulse);
        }
    }

    private void Die()
    {
        Debug.Log("El enemigo ha muerto.");
        Destroy(gameObject);
    }

    private void Explode()
    {
        hasExploded = true;

        // Da�a al jugador y lo empuja
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageToPlayer);

            // Empuja al jugador
            Rigidbody playerRb = thePlayer.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                Vector3 direction = thePlayer.position - transform.position;
                direction.y = 0; // No lo empuja hacia arriba
                playerRb.AddForce(direction.normalized * pushBackForce, ForceMode.Impulse);
            }
        }

        // Efecto de explosi�n
        if (explosionEffect != null)
        {
            ParticleSystem explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            explosion.Play();
            Destroy(explosion.gameObject, 2f);
        }

        Destroy(gameObject);
    }

    // Para visualizar el radio de explosi�n en la escena
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radio);
    }
}
