using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealPowerUp : MonoBehaviour
{
    PlayerHealth playerHealth;
    private void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(playerHealth.currentHealth != playerHealth.maxHealth)
            {
                playerHealth.heal(10);
                Destroy(gameObject);
            }
        }
    }
}
