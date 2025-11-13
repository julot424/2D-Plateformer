using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    private Animator animator;

    private void Awake()
    {
        animator = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);

            if (pauseMenu.activeSelf)
            {
                Time.timeScale = 0.0f;
            }

            else
            {
                Time.timeScale = 1.0f;
            }
        }
    }

    public void quitGame()
    {
        SceneManager.LoadScene(0);
        DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        Time.timeScale = 1.0f;
    }

    public void retryButton()
    {
        if (CurrentSceneManager.instance.isPlayerPresentDefault)
        {
            DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        }

        PlayerMovement.instance.freezeMovement();
        Time.timeScale = 1.0f;
        StartCoroutine(reloadScene());

    }

    public void settingButton()
    {
        settingsMenu.SetActive(true);
    }
    public void exitSettings()
    {
        settingsMenu.SetActive(false);
    }



public IEnumerator reloadScene()
    {
        animator.SetTrigger("fadeIn");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        PlayerMovement.instance.capsuleCollider.enabled = true;
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Dynamic;
        PlayerMovement.instance.spriteRenderer.enabled = true;
        PlayerMovement.instance.enabled = true;
        PlayerMovement.instance.animator.SetTrigger("Respawn");
        pauseMenu.SetActive(false);
        PlayerHealth.instance.currentHealth = PlayerHealth.instance.maxHealth;
        HealthBar.instance.setHealth(PlayerHealth.instance.currentHealth);

        Inventory.Instance.removeCoins(CurrentSceneManager.instance.coinsPickedUp);
    }
}
    
