using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Include the TextMeshPro namespace

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; // Singleton pattern to easily access the UIManager

    public TextMeshProUGUI coinsText; // Reference to the coins TextMeshPro element
    public TextMeshProUGUI waveNumberText; // Reference to the wave number TextMeshPro element

    private int coins = 0; // Initialize coins to 0

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateUI();
    }

    public void UpdateWaveNumber(int waveNumber)
    {
        waveNumberText.text = "Wave: " + waveNumber.ToString();
    }

    void UpdateUI()
    {
        coinsText.text = "Coins: " + coins.ToString();
    }
}
