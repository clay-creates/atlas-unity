using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Toggle InvertYToggle;
    public Button ApplyButton;
    public Button BackButton;

    private bool isInverted = false;

    private void Start()
    {
        // Load the current state of Y-axis inversion from PlayerPrefs
        isInverted = PlayerPrefs.GetInt("InvertY", 0) == 1;
        InvertYToggle.isOn = isInverted;

        // Subscribe to the onValueChanged event of the toggle
        InvertYToggle.onValueChanged.AddListener(value => ToggleInvertY(value));

        // Add listeners to buttons
        ApplyButton.onClick.AddListener(Apply);
        BackButton.onClick.AddListener(Back);
    }

    public static class SceneManagementHelper
    {
        public static void SaveCurrentSceneIndex()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.SetInt("currentSceneIndex", currentSceneIndex);
        }
    }

    public void Back()
    {
        int previousLevel = PlayerPrefs.GetInt("currentSceneIndex", -1);
        if (previousLevel != -1)
        {
            SceneManager.LoadScene(previousLevel);
            PlayerPrefs.DeleteKey("currentSceneIndex");
        }
    }

    public void Apply()
    {
        // Save the state of Y-axis inversion to PlayerPrefs
        PlayerPrefs.SetInt("InvertY", isInverted ? 1 : 0);
        PlayerPrefs.Save();

        Back();
    }

    public void ToggleInvertY(bool value)
    {
        isInverted = value;
        InvertYToggle.isOn = isInverted;
    }
}
