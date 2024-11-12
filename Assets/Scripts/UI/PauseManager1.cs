using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseManager1 : MonoBehaviour
{
    [Header("UIPanel")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button backButtonCredits;
    [SerializeField] private Button backButtonSettings;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject settingsPanel;



    private void Awake()
    {
        settingsButton.onClick.AddListener(OpenSettings);
        backButtonCredits.onClick.AddListener(OnExitCredits);
        playButton.onClick.AddListener(OnPlayButtonClicked);
        creditsButton.onClick.AddListener(OpenCredits);
        exitButton.onClick.AddListener(ExitGame);
        backButtonSettings.onClick.AddListener(ExitSettings);
        creditsPanel.SetActive(false);
    }
    private void OnDestroy()
    {
        playButton.onClick.RemoveListener(OnPlayButtonClicked);
        creditsButton.onClick.RemoveListener(OpenCredits);
        backButtonCredits.onClick.RemoveListener(OnExitCredits);
        exitButton.onClick.RemoveListener(ExitGame);
        settingsButton.onClick.RemoveListener(OpenSettings);
        backButtonSettings.onClick.RemoveListener(ExitSettings);

    }

    public void OpenCredits()
    {
        pauseMenuUI.SetActive(false);
        creditsPanel.SetActive(true);

    }

    public void OpenSettings()
    {
        pauseMenuUI.SetActive(false);
        settingsPanel.SetActive(true);

    }


    public void ExitCredits()
    {
        creditsPanel.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
#if     UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#endif
    }

    private void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("Platformer 2D");
    }

    private void OnExitCredits()
    {
        ExitCredits();
    }

    public void ExitSettings()
    {
        settingsPanel.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    private void OnExitSettings()
    {
        ExitSettings();
    }



}
