using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;
    private Animator animator;

    public static GameOverManager instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de GameOverManager");
            return;
        }
        instance = this;

        animator = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

    public void OnPlayerDeath()
    {
        if(CurrentSceneManager.instance.isPlayerPresentDefault)
        {
            DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        }

        gameOverUI.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void MainMenuButton()
    {
        DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        SceneManager.LoadScene(0);
    }

    public void RetryButton()
    {
        StartCoroutine(reloadScene());
    }

    public IEnumerator reloadScene()
    {
        animator.SetTrigger("fadeIn");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOverUI.SetActive(false);

        PlayerMovement.instance.capsuleCollider.enabled = true;
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Dynamic;
        PlayerMovement.instance.spriteRenderer.enabled = true;
        PlayerMovement.instance.enabled = true;
        PlayerMovement.instance.animator.SetTrigger("Respawn");

        PlayerHealth.instance.currentHealth = PlayerHealth.instance.maxHealth;
        HealthBar.instance.setHealth(PlayerHealth.instance.currentHealth);

        Inventory.Instance.removeCoins(CurrentSceneManager.instance.coinsPickedUp);
    }
}
