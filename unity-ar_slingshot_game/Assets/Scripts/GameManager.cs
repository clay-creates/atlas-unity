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
        InitializeUI();
    }

    public void InitializeUI()
    {
        startButton.SetActive(false);
        selectPlaneButton.SetActive(false);
        planeSearchText.SetActive(true);
        ammoGroup.SetActive(false);
        scoreText.gameObject.SetActive(false);
        restartButton.SetActive(false);
        leaderboardButton.SetActive(false);
        leaderboardCanvas.SetActive(false);
        UpdateScore();
    }

    public void ShowSelectPlaneButton()
    {
        selectPlaneButton.SetActive(true);
    }

    public void ActivateGameUI()
    {
        planeSearchText.SetActive(false);
        selectPlaneButton.SetActive(false);
        startButton.SetActive(true);
    }

    public void StartGame()
    {
        if (gameStarted)
        {
            Debug.LogWarning("Game already started.");
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
            targetSpawner.SetSelectedPlane(PlaneSelector.selectedPlane);
            targetSpawner.SetARCamera(arCamera);
            targetSpawner.SpawnTargets();

            targetCount = targetSpawner.GetTargetCount();
        }
        else
        {
            Debug.LogError("Selected plane or target spawner is not set.");
        }

        InstantiateAmmo();
    }

    public void AmmoThrown()
    {
        if (currentAmmoInstance != null)
        {
            Destroy(currentAmmoInstance);
        }
        InstantiateAmmo();
    }

    public void TargetHit()
    {
        score += 100;
        targetCount--;
        UpdateScore();

        if (targetCount <= 0)
        {
            EndGame();
        }
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    private void EndGame()
    {
        ammoGroup.SetActive(false);
        scoreText.gameObject.SetActive(false);
        restartButton.SetActive(true);
        leaderboardButton.SetActive(true);
    }

    public void RestartGame()
    {
        gameStarted = false;
        InitializeUI();
        StartGame();
    }

    public void ShowLeaderboard()
    {
        leaderboardCanvas.SetActive(true);
    }

    private void InstantiateAmmo()
    {
        if (ammoPrefab != null && arCamera != null)
        {
            Vector3 spawnPosition = arCamera.transform.position + arCamera.transform.forward * 1f;
            currentAmmoInstance = Instantiate(ammoPrefab, spawnPosition, Quaternion.identity);

            AmmoBehavior ammoBehavior = currentAmmoInstance.GetComponent<AmmoBehavior>();
            if (ammoBehavior != null)
            {
                ammoBehavior.SetGameManager(this);
            }
        }
        else
        {
            Debug.LogError("Ammo prefab or AR Camera is not assigned.");
        }
    }
}
