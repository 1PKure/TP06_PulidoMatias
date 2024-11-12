using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private int currentCoins = 0;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private GameObject winPanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        currentCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        UpdateCoinUI();
        winPanel.SetActive(false);
    }

    private void UpdateCoinUI()
    {
        coinText.text = "Coins: " + currentCoins.ToString();
    }


    public void AddCoin(int amount)
    {
        currentCoins += amount; 
        PlayerPrefs.SetInt("TotalCoins", currentCoins); 
        UpdateCoinUI(); //
    }

    public void ResetCoins()
    {
        PlayerPrefs.DeleteKey("TotalCoins");
        currentCoins = 0;
        UpdateCoinUI();
    }

    private void CheckWinCondition()
    {
        if (currentCoins >= 3) // Verifica si el jugador tiene al menos 3 monedas
        {
            WinGame();
        }
    }

    private void WinGame()
    {

        Time.timeScale = 0f;
        Debug.Log("¡Ganaste el juego!");
        winPanel.SetActive(true);

    }
}

