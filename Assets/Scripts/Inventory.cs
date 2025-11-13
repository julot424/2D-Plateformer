using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int CoinsCount;

    public static Inventory Instance;
    public Text coinsText;

    public void Awake()
    {
        if(Instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de Inventory !");
            return;
        }

        Instance = this;
    }

    public void addCoins(int Count)
    {
        CoinsCount += Count;
        coinsText.text = CoinsCount.ToString();
    }

    public void removeCoins(int count)
    {
        CoinsCount -= count;
        coinsText.text = CoinsCount.ToString();
    }
}
