using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class CurrentSceneManager : MonoBehaviour
{
    public static CurrentSceneManager instance;

    public bool isPlayerPresentDefault = false;
    public int coinsPickedUp;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Il ya plus d'une instance de CurrentSceneManager");
            return;
        }

        instance = this;
    }

}
