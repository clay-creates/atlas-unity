using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 5f;
    public float jumpForce = 7f;
    private bool isJumping = false;
    private bool isFalling = false;
    public Camera mainCamera;

    public InputActionAsset inputActionAsset;
    private InputAction moveAction;
    private InputAction jumpAction;

    private Vector3 startPosition;
    public float fallThreshold = -10f;

    private PlayerAnimationController animController;

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

    private void Update()
    {
        if (moveAction == null || jumpAction == null || mainCamera == null) return;

        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        if (jumpAction.triggered && !isJumping && Mathf.Abs(rb.velocity.y) < 0.001f)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
            animController.TriggerJump();
            animController.SetJumping(true);
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
        }
        else
        {
            animController.SetRunning(false);
        }

        rb.MovePosition(rb.position + movement * Time.deltaTime);

        if (transform.position.y < fallThreshold) ResetPlayerPosition();
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(rb.velocity.y) < 0.001f)
        {
            if (isJumping)
            {
                isJumping = false;
                animController.SetJumping(false);
            }
            if (isFalling)
            {
                isFalling = false;
                animController.SetFalling(false);
                animController.TriggerFallingImpact();
            }
        }
        else if (rb.velocity.y < -10f)
        {
            isFalling = true;
            animController.SetFalling(true);
        }
    }

    private void ResetPlayerPosition()
    {
        transform.position = new Vector3(startPosition.x, startPosition.y + 10f, startPosition.z);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = true;
        animController.TriggerFallingImpact();
    }
}
