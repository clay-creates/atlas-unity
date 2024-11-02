using TMPro.Examples;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject selectPlaneButton;
    [SerializeField] private GameObject planeSearchText;
    [SerializeField] private GameObject ammoGroup;
    [SerializeField] private GameObject scoreText;

    [SerializeField] private TargetSpawner targetSpawner;
    [SerializeField] private Camera arCamera;

    private bool gameStarted = false;

    public void InitializeUI()
    {
        // Initial state for the UI
        startButton.SetActive(false);
        selectPlaneButton.SetActive(false);
        planeSearchText.SetActive(true);
        ammoGroup.SetActive(false);
        scoreText.SetActive(false);
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
        if (gameStarted)
        {
            Debug.LogWarning("Game already started. Ignoring additional input");
            return;
        }

        gameStarted = true;
        startButton.SetActive(false);
        ammoGroup.SetActive(true);
        scoreText.SetActive(true);

        if (PlaneSelector.selectedPlane != null && targetSpawner != null && arCamera != null)
        {
            targetSpawner.SetSelectedPlane(PlaneSelector.selectedPlane);
            targetSpawner.SetARCamera(arCamera);
            targetSpawner.SpawnTargets();
        }
        else
        {
            Debug.LogError("Selected plane or target spawner is not set");
        }
    }
}
