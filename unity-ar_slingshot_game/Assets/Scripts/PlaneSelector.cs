using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;
using System.Collections.Generic;

public class PlaneSelector : MonoBehaviour
{
    public static ARPlane selectedPlane = null;

    public GameManager gameManager;
    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;

    public Material defaultMaterial;
    public Material highlightMaterial;

    private TouchActions touchActions;

    private bool gameStarted = false;

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();

        if (raycastManager == null || planeManager == null )
        {
            Debug.LogError("ARRaycastManager or ARPlaneManager is missing");
        }

        touchActions = new TouchActions();
        EnhancedTouch.EnhancedTouchSupport.Enable();
    }

    private void Start()
    {
        if (gameManager != null)
        {
            gameManager.InitializeUI();
        }
    }

    private void OnEnable()
    {
        touchActions.Enable();
        EnhancedTouch.Touch.onFingerDown += OnFingerDown;
    }

    private void OnDisable()
    {
        EnhancedTouch.Touch.onFingerDown -= OnFingerDown;
        touchActions.Disable();
        EnhancedTouch.EnhancedTouchSupport.Disable();
    }

    private void OnFingerDown(EnhancedTouch.Finger finger)
    {
        if (gameStarted) return;

        Vector2 touchPosition = finger.screenPosition;
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            ARPlane plane = hit.collider.GetComponent<ARPlane>();
            if (plane != null)
            {
                SelectPlane(plane);
            }
        }
    }

    private void SelectPlane(ARPlane plane)
    {
        if (selectedPlane != null && selectedPlane != plane)
        {
            ResetPlaneAppearance(selectedPlane);
        }

        selectedPlane = plane;

        if (gameManager != null)
        {
            gameManager.ShowSelectPlaneButton();
        }

        HighlightSelectedPlane(plane);
        DisableOtherPlanes();
    }

    private void ResetPlaneAppearance(ARPlane plane)
    {
        MeshRenderer planeRenderer = plane.GetComponent<MeshRenderer>();
        if (planeRenderer != null && defaultMaterial != null)
        {
            planeRenderer.material = defaultMaterial;
        }
    }

    private void HighlightSelectedPlane(ARPlane plane)
    {
        MeshRenderer planeRenderer = plane.GetComponent<MeshRenderer>();
        if (planeRenderer != null && highlightMaterial != null)
        {
            planeRenderer.material = highlightMaterial;
        }
    }

    public void ConfirmPlaneSelection()
    {
        if (gameManager != null)
        {
            gameManager.ActivateGameUI();
        }

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
            }
        }
    }
}
