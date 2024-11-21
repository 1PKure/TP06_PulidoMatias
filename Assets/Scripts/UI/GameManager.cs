using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private PauseManager pauseManager;
    [SerializeField] private AudioSource loseSound;
    [SerializeField] private AudioSource gameAudio;
    [SerializeField] private int currentCoins = 0;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    private PlayerController playerController;
    private bool gameEnded = false;
    private PlayerData playerData;


    private void Update()
    {
        if (gameEnded)
        {
            HandleRestartOrQuit();
            gameAudio.Stop();
        }
    }
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

        ResetCoins();
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
        UpdateCoinUI();
        CheckWinCondition();
    }

    private void CheckWinCondition()
    {
        if (currentCoins >= 3)
        {
            WinGame();
        }
    }

    public void WinGame()
    {
        gameEnded = true;
        Time.timeScale = 0f;
        winPanel.SetActive(true);
        Debug.Log("¡Has ganado el juego!");
    }

    public void LoseGame()
    {
        gameEnded = true;
        Time.timeScale = 0f;
        losePanel.SetActive(true);
        Debug.Log("¡Has perdido el juego!");
    }

    private void HandleRestartOrQuit()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            RestartGame();
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }

    public void Lose()
    {
        if (pauseManager != null)
        {
            pauseManager.enabled = false;
        }
        loseSound.Play();
        losePanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void RestartGame()
    {
        playerData.ResetValue();
        ResetCoins();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResetCoins()
    {
        PlayerPrefs.DeleteKey("TotalCoins");
        currentCoins = 0;
        UpdateCoinUI();
    }
}

