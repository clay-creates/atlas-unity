using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public GameObject Player; // Reference to the Player GameObject
    private Vector3 offset; // Distance between player and camera

    private Vector2 _delta;
    private bool _isRotating;
    private float _xRotation;
    private bool isInverted;

    private Transform playerTransform;

    [SerializeField] private float rotationSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - Player.transform.position; // Calculate the initial offset
        playerTransform = Player.transform;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _delta = context.ReadValue<Vector2>();
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        _isRotating = context.started || context.performed;
    }

    // Method to toggle Y-axis inversion
    public void SetYAxisInversion(bool isInverted)
    {
        this.isInverted = isInverted;
    }

    // LateUpdate is called once per frame at the end of each frame
    void LateUpdate()
    {
        transform.position = Player.transform.position + offset; // Maintain the same offset as the player travels

        if (_isRotating)
        {
            transform.LookAt(playerTransform.position);

            // Handle Y-axis inversion
            float yAxisInput = isInverted ? -Input.GetAxis("Mouse Y") : Input.GetAxis("Mouse Y");

            // Rotate around the player on the Y-axis and X-axis
            offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up) * offset;
            offset = Quaternion.AngleAxis(yAxisInput * rotationSpeed, Vector3.right) * offset;

            transform.position = playerTransform.position + offset;
        }
    }
}
