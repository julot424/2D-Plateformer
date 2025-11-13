using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public bool isInvicible = false;

    public float flashDelay;

    public SpriteRenderer spriteRenderer;

    public HealthBar healthBar;

    public static PlayerHealth instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerHealth");
            return;
        }

        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);  
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)) 
        {
            takeDamage(10);
        }
    }

    public void takeDamage(int damage)
    {
        if(!isInvicible)
        {
            currentHealth -= damage;
            healthBar.setHealth(currentHealth);

            if(currentHealth <= 0)
            {
                Die();
                return;
            }

            isInvicible = true;
            StartCoroutine(invicibilityFlash());
            StartCoroutine(handleInvicibility());
        }   
    }

    public void Die()
    {
        PlayerMovement.instance.enabled = false;

        PlayerMovement.instance.animator.SetTrigger("Die");
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Kinematic;
        PlayerMovement.instance.capsuleCollider.enabled = false;
        PlayerMovement.instance.freezeMovement();
        GameOverManager.instance.OnPlayerDeath();
        
    }

    public void heal(int heal)
    {
        if(currentHealth + heal > maxHealth)
        {
            currentHealth = maxHealth;
            healthBar.setHealth(currentHealth);
        }

        else
        {
            currentHealth += heal;
            healthBar.setHealth(currentHealth);
        }
    }
    
    

    public IEnumerator invicibilityFlash()
    {
        while(isInvicible)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(flashDelay);

            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(flashDelay);

        }
    }

    public IEnumerator handleInvicibility()
    {
        yield return new WaitForSeconds(2f);
        isInvicible = false;
    }
}
