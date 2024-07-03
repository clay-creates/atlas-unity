using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static OptionsMenu;

public class PauseMenu : MonoBehaviour
{

    public Button RestartButton;
    public Button MenuButton;
    public Button OptionsButton;
    public Button ResumeButton;

    public GameObject PauseCanvas;
    public Timer PlayTimer;

    private bool isPaused = false;

    private void Start()
    {
        RestartButton.onClick.AddListener(Restart);
        MenuButton.onClick.AddListener(MainMenu);
        OptionsButton.onClick.AddListener(Options);
        ResumeButton.onClick.AddListener(Resume);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
                
        }
    }

    public void Pause()
    {
        isPaused = true;
        PauseCanvas.SetActive(true);
        PlayTimer.StopTimer();
    }

    public void Resume()
    {
        isPaused = false;
        PauseCanvas.SetActive(false);
        // Time.timeScale = 1f;
        PlayTimer.StartTimer();
    }

    public void Restart()
    {
        PlayTimer.ResetTimer();
        SceneManagementHelper.SaveCurrentSceneIndex();
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex);
    }

    public void MainMenu()
    {
        PlayTimer.ResetTimer();
        SceneManagementHelper.SaveCurrentSceneIndex();
        SceneManager.LoadScene("MainMenu");
    }

    public void Options()
    {
        PlayTimer.ResetTimer();
        SceneManagementHelper.SaveCurrentSceneIndex();
        SceneManager.LoadScene("Options");
    }
}
