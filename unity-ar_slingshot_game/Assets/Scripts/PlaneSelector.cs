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

        EnhancedTouch.EnhancedTouchSupport.Enable();
    }

    private void Start()
    {
        gameManager?.InitializeUI();
    }

    private void OnEnable()
    {
        EnhancedTouch.Touch.onFingerDown += OnFingerDown;
    }

    private void OnDisable()
    {
        EnhancedTouch.Touch.onFingerDown -= OnFingerDown;
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
        gameManager?.ShowSelectPlaneButton();
        HighlightSelectedPlane(plane);
        DisableOtherPlanes();
    }

    private void ResetPlaneAppearance(ARPlane plane)
    {
        var planeRenderer = plane.GetComponent<MeshRenderer>();
        if (planeRenderer != null && defaultMaterial != null)
        {
            planeRenderer.material = defaultMaterial;
        }
    }

    private void HighlightSelectedPlane(ARPlane plane)
    {
        var planeRenderer = plane.GetComponent<MeshRenderer>();
        if (planeRenderer != null && highlightMaterial != null)
        {
            planeRenderer.material = highlightMaterial;
        }
    }

    public void ConfirmPlaneSelection()
    {
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
            }
        }
    }
}
