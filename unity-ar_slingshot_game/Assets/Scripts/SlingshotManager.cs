using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using TMPro;

public class SlingshotManager : MonoBehaviour
{
    [Header("Ammo Configuration")]
    public GameObject ammoPrefab;
    public Transform ammoSpawnPoint;
    public float forceMultiplier = 500f;
    public float maxHoldTime = 2f;
    public LineRenderer trajectoryLine;

    [Header("UI Elements")]
    public TextMeshProUGUI ammoCountText;
    public TextMeshProUGUI scoreText;
    public Button playAgainButton;

    private GameObject currentAmmo;
    private Rigidbody ammoRigidbody;
    private Vector3 initialPosition;
    private Vector2 startTouchPosition;
    private bool isDragging = false;
    private float holdTime = 0f;
    private int maxAmmo = 7;
    private int currentAmmoCount;
    private int score = 0;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += HandleFingerDown;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerMove += HandleFingerMove;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp += HandleFingerUp;
    }

    private void OnDisable()
    {
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= HandleFingerDown;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerMove -= HandleFingerMove;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp -= HandleFingerUp;
        EnhancedTouchSupport.Disable();
    }

    private void Start()
    {
        currentAmmoCount = maxAmmo;
        UpdateUI();
        playAgainButton.gameObject.SetActive(false);
        SpawnAmmo();
    }

    private void HandleFingerDown(UnityEngine.InputSystem.EnhancedTouch.Finger finger)
    {
        Ray ray = Camera.main.ScreenPointToRay(finger.screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider != null && hit.collider.gameObject == currentAmmo)
            {
                isDragging = true;
                startTouchPosition = finger.screenPosition;
                holdTime = 0;

                ammoRigidbody.isKinematic = true;
                trajectoryLine.enabled = true;

                Debug.Log("Finger down detected on ammo.");
            }
            else
            {
                Debug.Log("Finger down detected, but not on ammo.");
            }
        }
        else
        {
            Debug.Log("Raycast did not hit anything.");
        }
    }

    private void HandleFingerMove(UnityEngine.InputSystem.EnhancedTouch.Finger finger)
    {
        if (isDragging)
        {
            holdTime += Time.deltaTime;
            holdTime = Mathf.Clamp(holdTime, 0, maxHoldTime);

            Vector2 touchPosition = finger.screenPosition;

            // Convert screen space position to world space for ammo placement
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, Camera.main.nearClipPlane));
            currentAmmo.transform.position = Vector3.Lerp(currentAmmo.transform.position, worldPosition, 0.1f);

            UpdateTrajectory();
        }
    }


    private void HandleFingerUp(UnityEngine.InputSystem.EnhancedTouch.Finger finger)
    {
        if (isDragging)
        {
            isDragging = false;
            ammoRigidbody.isKinematic = false;
            ammoRigidbody.useGravity = true; // Enable gravity after release

            Vector3 shootDirection = (initialPosition - currentAmmo.transform.position).normalized;
            float force = forceMultiplier * (holdTime / maxHoldTime);
            ammoRigidbody.AddForce(shootDirection * force);

            trajectoryLine.enabled = false;
            currentAmmoCount--;
            UpdateUI();

            if (currentAmmoCount > 0)
            {
                SpawnAmmo();
            }
            else
            {
                playAgainButton.gameObject.SetActive(true);
            }

            Debug.Log("Ammo released with force: " + force);
        }
    }

    private void SpawnAmmo()
    {
        if (ammoPrefab != null)
        {
            Canvas canvas = FindObjectOfType<Canvas>(); // Find the Canvas in the scene
            if (canvas == null)
            {
                Debug.LogError("Canvas not found in the scene. Please add a Canvas for UI.");
                return;
            }

            // Instantiate the ammo as a child of the Canvas
            currentAmmo = Instantiate(ammoPrefab, canvas.transform);

            // Position the ammo at the spawn point in screen space
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(ammoSpawnPoint.position);
            RectTransform ammoRect = currentAmmo.GetComponent<RectTransform>();
            if (ammoRect != null)
            {
                ammoRect.position = screenPosition;
                ammoRect.localScale = Vector3.one; // Adjust scale for UI
            }
            else
            {
                Debug.LogError("Ammo prefab does not have a RectTransform component.");
            }

            Debug.Log("Ammo spawned in screen space at: " + screenPosition);
        }
    }


    private void UpdateTrajectory()
    {
        if (trajectoryLine != null)
        {
            Vector3 startPoint = currentAmmo.transform.position;
            Vector3 velocity = (initialPosition - startPoint).normalized * (forceMultiplier * (holdTime / maxHoldTime) / ammoRigidbody.mass);

            int segmentCount = 30;
            trajectoryLine.positionCount = segmentCount;
            for (int i = 0; i < segmentCount; i++)
            {
                float time = i * 0.1f; // Adjust segment spacing
                trajectoryLine.SetPosition(i, CalculateTrajectoryPoint(startPoint, velocity, time));
            }
        }
    }

    private Vector3 CalculateTrajectoryPoint(Vector3 start, Vector3 velocity, float time)
    {
        Vector3 position = start + velocity * time;
        position.y += 0.5f * Physics.gravity.y * time * time;
        return position;
    }

    private void UpdateUI()
    {
        ammoCountText.text = "Ammo: " + currentAmmoCount;
        scoreText.text = "Score: " + score;
    }

    public void PlayAgain()
    {
        currentAmmoCount = maxAmmo;
        score = 0;
        UpdateUI();
        playAgainButton.gameObject.SetActive(false);
        SpawnAmmo();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            score++;
            UpdateUI();
            Destroy(collision.gameObject);
            Debug.Log("Enemy hit! Score: " + score);
        }
    }
}
