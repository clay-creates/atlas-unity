using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject selectPlaneButton;
    [SerializeField] private GameObject planeSearchText;
    [SerializeField] private GameObject ammoGroup;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject leaderboardButton;
    [SerializeField] private GameObject leaderboardCanvas;
    [SerializeField] private GameObject[] ammoImages;
    [SerializeField] private GameObject ammoPrefab;
    [SerializeField] private TargetSpawner targetSpawner;
    [SerializeField] private Camera arCamera;

    private bool gameStarted = false;
    private int score = 0;
    private int targetCount;
    private GameObject currentAmmoInstance;

    private void Start()
    {
        Debug.Log("GameManager: Start method called.");
        InitializeUI();
    }

    public void InitializeUI()
    {
        Debug.Log("GameManager: Initializing UI elements.");
        startButton.SetActive(false);
        selectPlaneButton.SetActive(false);
        planeSearchText.SetActive(true);
        ammoGroup.SetActive(false);
        scoreText.gameObject.SetActive(false);
        restartButton.SetActive(false);
        leaderboardButton.SetActive(false);
        leaderboardCanvas.SetActive(false);

        UpdateScore();
        Debug.Log("GameManager: UI elements initialized.");
    }

    public void ShowSelectPlaneButton()
    {
        Debug.Log("GameManager: Displaying Select Plane button");
        selectPlaneButton.SetActive(true);
    }

    public void ActivateGameUI()
    {
        Debug.Log("GameManager: Displaying game UI elements.");
        planeSearchText.SetActive(false);
        selectPlaneButton.SetActive(false);
        startButton.SetActive(true);
    }

    public void StartGame()
    {
        Debug.Log("GameManager: StartGame method called.");
        if (gameStarted)
        {
            Debug.LogWarning("GameManager: Game already started, ignoring additional call.");
            return;
        }

        gameStarted = true;
        startButton.SetActive(false);
        ammoGroup.SetActive(true);
        scoreText.gameObject.SetActive(true);
        score = 0;
        UpdateScore();

        if (PlaneSelector.selectedPlane != null && targetSpawner != null && arCamera != null)
        {
            Debug.Log("GameManager: Setting up target spawner with selected plane and camera.");
            targetSpawner.SetSelectedPlane(PlaneSelector.selectedPlane);
            targetSpawner.SetARCamera(arCamera);
            targetSpawner.SpawnTargets();

            targetCount = targetSpawner.GetTargetCount();
            Debug.Log($"GameManager: {targetCount} targets spawned.");
        }
        else
        {
            Debug.LogError("GameManager: Selected plane or target spawner is not set.");
        }

        InstantiateAmmo();
    }

    public void AmmoThrown()
    {
        Debug.Log("GameManager: Ammo thrown, decrementing ammo.");

        if (currentAmmoInstance != null)
        {
            Debug.Log("GameManager: Destroying previous ammo instance");
            Destroy(currentAmmoInstance);
        }

        InstantiateAmmo();
    }

    public void TargetHit()
    {
        score += 100;
        targetCount--;
        Debug.Log($"GameManager: Target hit. New score {score}. Remaining targets: {targetCount}");
        UpdateScore();

        if (targetCount <= 0)
        {
            Debug.Log("GameManager: All targets eliminated. Ending game.");
            EndGame();
        }
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;
        Debug.Log($"GameManager: Score updated to {score}.");
    }

    private void EndGame()
    {
        Debug.Log("GameManager: Ending game and displaying end UI.");
        ammoGroup.SetActive(false);
        scoreText.gameObject.SetActive(false);
        restartButton.SetActive(true);
        leaderboardButton.SetActive(true);
    }

    public void RestartGame()
    {
        Debug.Log("GameManager: Restarting game.");
        gameStarted = false;
        InitializeUI();
        StartGame();
    }

    public void ShowLeaderboard()
    {
        Debug.Log("GameManager: Displaying leaderboard.");
        leaderboardCanvas.SetActive(true);
    }

    private void InstantiateAmmo()
    {
        Debug.Log("GameManager: Instantiating ammo.");
        if (currentAmmoInstance != null)
        {
            Debug.Log("GameManager: Destroying existing ammo before creating new one");
            Destroy(currentAmmoInstance);
        }

        if (ammoPrefab != null && arCamera != null)
        {
            Debug.Log("GameManager: Instantiating ammo.");

            Vector3 spawnPosition = arCamera.transform.position + arCamera.transform.forward * 1f;
            currentAmmoInstance = Instantiate(ammoPrefab, spawnPosition, Quaternion.identity);

            float scaleFactor = Screen.width * .0025f;
            currentAmmoInstance.transform.localScale = Vector3.one * scaleFactor;
            Debug.Log($"GameManager: Ammo instantiated at {spawnPosition} with scale factor {scaleFactor}");

            AmmoBehavior ammoBehavior = currentAmmoInstance.GetComponent<AmmoBehavior>();
            if (ammoBehavior != null)
            {
                Debug.Log("GameManager: Setting GameManager reference in AmmoBehavior");
                ammoBehavior.SetGameManager(this);
            }
            else
            {
                Debug.LogWarning("GameManager: AmmoBehavior component missing on ammo instance");
            }
        }
        else
        {
            Debug.LogError("GameManager: Ammo prefab or AR Camera is not assigned.");
        }
    }
}
