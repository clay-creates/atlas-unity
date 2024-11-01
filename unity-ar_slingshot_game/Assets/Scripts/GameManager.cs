using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject selectPlaneButton;
    [SerializeField] private GameObject planeSearchText;
    [SerializeField] private GameObject ammoGroup;
    [SerializeField] private GameObject scoreText;
    // [SerializeField] private GameObject pauseButton;

    public void InitializeUI()
    {
        // Initial state for the UI
        startButton.SetActive(false);
        selectPlaneButton.SetActive(true);
        planeSearchText.SetActive(true);
        ammoGroup.SetActive(false);
        scoreText.SetActive(false);
        // pauseButton.SetActive(false);
    }

    public void ShowSelectPlaneButton()
    {
        // Called when a plane is selected, shows the Select Plane button
        selectPlaneButton.SetActive(true);
    }

    public void ActivateGameUI()
    {
        // Hides plane search and select plane button, activates game UI
        planeSearchText.SetActive(false);
        selectPlaneButton.SetActive(false);
        startButton.SetActive(true);
    }

    public void StartGame()
    {
        // Activates game-specific UI elements when Start is pressed
        startButton.SetActive(false);
        ammoGroup.SetActive(true);
        scoreText.SetActive(true);
        // pauseButton.SetActive(true);
    }
}
