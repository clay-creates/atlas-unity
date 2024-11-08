using UnityEngine;
using UnityEngine.InputSystem;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class AmmoBehavior : MonoBehaviour
{
    private GameManager gameManager;
    private bool isDragging = false;
    private Vector2 initialTouchPosition;
    private Vector2 dragDirection;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetGameManager(GameManager manager)
    {
        gameManager = manager;
    }

    private void OnEnable()
    {
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += OnFingerDown;
        EnhancedTouch.Touch.onFingerUp += OnFingerUp;
        EnhancedTouch.Touch.onFingerMove += OnFingerMove;
    }

    private void OnDisable()
    {
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.Touch.onFingerDown -= OnFingerDown;
        EnhancedTouch.Touch.onFingerUp -= OnFingerUp;
        EnhancedTouch.Touch.onFingerMove -= OnFingerMove;
    }

    private void OnFingerDown(EnhancedTouch.Finger finger)
    {
        Vector2 touchPosition = finger.screenPosition;
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);

        if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform == transform)
        {
            Debug.Log("AmmoBehavior: Ammo touched.");
            isDragging = true;
            initialTouchPosition = touchPosition;
        }
    }

    private void OnFingerMove(EnhancedTouch.Finger finger)
    {
        if (isDragging)
        {
            dragDirection = finger.screenPosition - initialTouchPosition;
            Debug.Log("AmmoBehavior: Dragging with direction:" + dragDirection);
        }
    }

    private void OnFingerUp(EnhancedTouch.Finger finger)
    {
        if (isDragging)
        {
            isDragging = false;
            Debug.Log("AmmoBehavior: Ammo released.");
            LaunchAmmo();
        }
    }

    private void LaunchAmmo()
    {
        Vector3 launchDirection = new Vector3(dragDirection.x, dragDirection.y, 1f).normalized;
        float launchForce = dragDirection.magnitude * 0.1f;

        rb.AddForce(launchDirection * launchForce, ForceMode.Impulse);
        gameManager.AmmoThrown();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            Debug.Log("AmmoBehavior: Hit target.");
            gameManager?.TargetHit();
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Plane"))
        {
            Debug.Log("AmmoBehavior: Hit the ground plane.");
            Destroy(gameObject);
            gameManager.AmmoThrown();
        }
    }
}
