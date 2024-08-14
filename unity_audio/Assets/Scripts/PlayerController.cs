using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading.Tasks;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 5f;
    public float jumpForce = 7f;

    private bool isRunning = false;
    private bool isJumping = false;
    private bool isFalling = false;
    private bool isGrounded = true;
    private bool wasGrounded = true; // Track the previous grounded state

    public Camera mainCamera;

    public InputActionAsset inputActionAsset;
    private InputAction moveAction;
    private InputAction jumpAction;

    private Vector3 startPosition;
    public float fallThreshold = -10f;

    private PlayerAnimationController animController;

    private bool canPlayFootstep = true;

    private void OnEnable()
    {
        if (inputActionAsset == null)
        {
            Debug.LogError("InputActionAsset is not assigned in the Inspector.");
            return;
        }

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
        if (moveAction != null) moveAction.Disable();
        if (jumpAction != null) jumpAction.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                Debug.LogError("Main Camera is not assigned and could not be found automatically.");
            }
        }

        startPosition = transform.position;
        animController = GetComponent<PlayerAnimationController>();
    }

    private async void Update()
    {
        if (moveAction == null || jumpAction == null || mainCamera == null) return;

        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        if (jumpAction.triggered && isGrounded && !isJumping)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
            isGrounded = false;
            animController.TriggerJump();
            animController.SetJumping(true);
            AudioManager.Instance.StopFootstep(); // Stop footstep audio when jumping
        }

        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 movement = (cameraRight * moveInput.x + cameraForward * moveInput.y) * speed;

        if (moveInput.magnitude > 0)
        {
            animController.SetRunning(true);
            Vector3 direction = new Vector3(movement.x, 0, movement.z);
            if (direction != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.deltaTime);
            }
            isRunning = true;
        }
        else
        {
            animController.SetRunning(false);
            isRunning = false;
        }

        rb.MovePosition(rb.position + movement * Time.deltaTime);

        // Footstep Sound Logic
        if (isGrounded && isRunning && canPlayFootstep)
        {
            AudioManager.Instance.PlayFootstep();
            await FootstepDelay(0.45f);
        }

        if (!isRunning || !isGrounded)
        {
            AudioManager.Instance.StopFootstep();
        }

        if (transform.position.y < fallThreshold) ResetPlayerPosition();
    }

    private void FixedUpdate()
    {
        // Check if the player has landed after jumping or falling
        if (Mathf.Abs(rb.velocity.y) < 0.001f && !isGrounded)
        {
            isGrounded = true;
            isJumping = false;
            isFalling = false;
            animController.SetJumping(false);
            animController.SetFalling(false);

            // Only play landing sound if the player was previously not grounded
            if (!wasGrounded)
            {
                AudioManager.Instance.PlayLanding();
            }
        }
        else if (rb.velocity.y < -10f)
        {
            isFalling = true;
            isGrounded = false;
            animController.SetFalling(true);
        }
        else if (rb.velocity.y > 0.001f)
        {
            isGrounded = false;
        }

        // Track the previous grounded state for the next frame
        wasGrounded = isGrounded;
    }

    private void ResetPlayerPosition()
    {
        transform.position = new Vector3(startPosition.x, startPosition.y + 10f, startPosition.z);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = true;
        animController.TriggerFallingImpact();
    }

    private async Task FootstepDelay(float delayTime)
    {
        canPlayFootstep = false;
        await Task.Delay((int)(delayTime * 1000));
        canPlayFootstep = true;
    }
}
