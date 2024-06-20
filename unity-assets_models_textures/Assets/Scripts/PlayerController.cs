using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb; // RigidBody of the Player
    public float speed = 5f; // Speed of player
    public float jumpForce = 7f; // Force applied to jump
    private bool isJumping = false; // Flag to check if player is already jumping
    public Camera mainCamera;

    public InputActionAsset inputActionAsset;
    private InputAction moveAction;
    private InputAction jumpAction;

    private Vector3 startPosition; // Start position of the player
    public float fallThreshold = -10f; // Threshold below which the player is reset

    private void OnEnable()
    {
        if (inputActionAsset == null)
        {
            Debug.LogError("InputActionAsset is not assigned in the Inspector.");
            return;
        }

        // Enable the input actions
        moveAction = inputActionAsset.FindAction("Move");
        jumpAction = inputActionAsset.FindAction("Jump");

        if (moveAction == null || jumpAction == null)
        {
            Debug.LogError("Move or Jump action is not defined in the InputActionAsset.");
            return;
        }

        moveAction.Enable();
        jumpAction.Enable();
    }

    private void OnDisable()
    {
        if (moveAction != null)
        {
            moveAction.Disable();
        }

        if (jumpAction != null)
        {
            jumpAction.Disable();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get and store RigidBody component

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                Debug.LogError("Main Camera is not assigned and could not be found automatically.");
            }
        }

        startPosition = transform.position; // Store the initial position
    }

    private void Update()
    {
        if (moveAction == null || jumpAction == null || mainCamera == null)
        {
            return;
        }

        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        // Check for jump input
        if (jumpAction.triggered && !isJumping && !rb.isKinematic)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
        }

        // Update movement direction based on camera orientation
        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;

        // Flatten the vectors on the y-axis to ensure movement is parallel to the ground
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 movement = (cameraRight * moveInput.x + cameraForward * moveInput.y) * speed;

        // Apply movement
        rb.MovePosition(rb.position + movement * Time.deltaTime);

        // Check if the player has fallen below the threshold
        if (transform.position.y < fallThreshold)
        {
            ResetPlayerPosition();
        }
    }

    private void FixedUpdate() // Called once per fixed frame-rate frame
    {
        // Check if the player has landed
        if (Mathf.Abs(rb.velocity.y) < 0.001f)
        {
            isJumping = false;
        }
    }

    private void ResetPlayerPosition()
    {
        // Reset position slightly above the start position
        transform.position = new Vector3(startPosition.x, startPosition.y + 10f, startPosition.z);
        rb.velocity = Vector3.zero; // Reset velocity
        rb.angularVelocity = Vector3.zero; // Reset angular velocity

        // Ensure gravity is applied
        rb.useGravity = true;
    }
}
