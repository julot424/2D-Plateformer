using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Ladder : MonoBehaviour
{
    private bool isInRange;
    private PlayerMovement playerMovement;
    public BoxCollider2D plateform;
    private Text interactionText;
    
    
    void Awake()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        interactionText = GameObject.FindGameObjectWithTag("Interact").GetComponent<Text>();
        interactionText.enabled = false;
    }

    void Update()
    {
        if (isInRange && playerMovement.isClimbing && Input.GetKeyDown(KeyCode.E)) 
        {
            playerMovement.isClimbing = false;
            plateform.isTrigger = false;
            return;
        }

        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            playerMovement.isClimbing = true;
            plateform.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isInRange = true;
            interactionText.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactionText.enabled = false;
            isInRange = false;
            playerMovement.isClimbing = false;
            plateform.isTrigger = false;
        }
    }

}
    