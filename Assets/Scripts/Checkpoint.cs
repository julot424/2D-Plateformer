using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    GameObject playerSpawn;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn");

            playerSpawn.transform.position = transform.position;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
