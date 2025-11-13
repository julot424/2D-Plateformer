using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerFall : MonoBehaviour
{
    private GameObject playerSpawm;

    private void Awake()
    {
        playerSpawm = GameObject.FindGameObjectWithTag("PlayerSpawn");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerHealth.instance.takeDamage(10);
            if(PlayerHealth.instance.currentHealth <= 0)
            {
                return;
            }
            collision.transform.position = playerSpawm.transform.position;
        }
    }
}
