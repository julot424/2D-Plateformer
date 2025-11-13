using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSpecificScene : MonoBehaviour
{
    public string sceneName;
    public Animator animator;
    public bool isInrange = false;
    private Text interactionText;


    private void Awake()
    {
        animator = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
        interactionText = GameObject.FindGameObjectWithTag("Interact").GetComponent<Text>();
        interactionText.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            interactionText.enabled  = true;
            isInrange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            interactionText.enabled = false;
            isInrange = false;
        }
    }

    private void Update()
    {
        if(isInrange && Input.GetKeyDown(KeyCode.E))
        {
            PlayerMovement.instance.enabled = false;
            PlayerMovement.instance.freezeMovement();
            StartCoroutine(loadNextScene());
        }
    }

    public IEnumerator loadNextScene()
    {
        animator.SetTrigger("fadeIn");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(sceneName);
        PlayerMovement.instance.enabled = true;
    }
}
