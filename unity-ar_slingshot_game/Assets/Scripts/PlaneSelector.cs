using UnityEngine;
using UnityEngine.XR.ARFoundation;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class PlaneSelector : MonoBehaviour
{
    public static ARPlane selectedPlane = null;
    public GameManager gameManager;

    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;
    private bool gameStarted = false;

    public Material defaultMaterial;
    public Material highlightMaterial;

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();

        if (raycastManager == null || planeManager == null)
        {
            Debug.LogError("PlaneSelector: Missing ARRaycastManager or ARPlaneManager.");
        }

        EnhancedTouch.EnhancedTouchSupport.Enable();
    }

    private void Start()
    {
        Debug.Log("PlaneSelector: Start method called.");
        gameManager?.InitializeUI();
    }

    private void OnEnable()
    {
        Debug.Log("PlaneSelector: Enabling touch input.");
        EnhancedTouch.Touch.onFingerDown += OnFingerDown;
    }

    private void OnDisable()
    {
        Debug.Log("PlaneSelector: Disabling touch input.");
        EnhancedTouch.Touch.onFingerDown -= OnFingerDown;
    }

    private void OnFingerDown(EnhancedTouch.Finger finger)
    {
        if (gameStarted) return;

        Vector2 touchPosition = finger.screenPosition;
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        Debug.Log("PlaneSelector: Finger down, attempting plane selection.");

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            ARPlane plane = hit.collider.GetComponent<ARPlane>();
            if (plane != null)
            {
                Debug.Log("PlaneSelector: Plane detected on finger down.");
                SelectPlane(plane);
            }
            else
            {
                Debug.Log("PlaneSelector: No plane detected at touch position.");
            }
        }
    }

    private void SelectPlane(ARPlane plane)
    {
        Debug.Log("PlaneSelector: Plane selected.");
        if (selectedPlane != null && selectedPlane != plane)
        {
            ResetPlaneAppearance(selectedPlane);
        }

        selectedPlane = plane;
        gameManager?.ShowSelectPlaneButton();
        HighlightSelectedPlane(plane);
        DisableOtherPlanes();
    }

    private void ResetPlaneAppearance(ARPlane plane)
    {
        var planeRenderer = plane.GetComponent<MeshRenderer>();
        if (planeRenderer != null && defaultMaterial != null)
        {
            Debug.Log("PlaneSelector: Resetting plane appearance");
            planeRenderer.material = defaultMaterial;
        }
    }

    private void HighlightSelectedPlane(ARPlane plane)
    {
        var planeRenderer = plane.GetComponent<MeshRenderer>();
        if (planeRenderer != null && highlightMaterial != null)
        {
            Debug.Log("PlaneSelector: Highlighting selected plane.");
            planeRenderer.material = highlightMaterial;
        }
    }

    public void ConfirmPlaneSelection()
    {
        Debug.Log("PlaneSelector: Confirming plane selection.");
        gameManager?.ActivateGameUI();
        gameStarted = true;
    }

    private void DisableOtherPlanes()
    {
        foreach (var plane in planeManager.trackables)
        {
            if (plane != selectedPlane)
            {
                ResetPlaneAppearance(plane);
                plane.gameObject.SetActive(false);
                Debug.Log("PlaneSelector: Disabling other planes.");
            }
        }
    }
}
