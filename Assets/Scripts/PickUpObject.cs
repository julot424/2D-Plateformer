using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Inventory.Instance.addCoins(1);
            CurrentSceneManager.instance.coinsPickedUp++;
            Destroy(gameObject);
        }
    }
}
