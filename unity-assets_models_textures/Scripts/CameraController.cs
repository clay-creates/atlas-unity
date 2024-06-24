using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public GameObject Player; // Reference to the Player GameObject
    private Vector3 offset; // Distance between player and camera


    private Vector2 _delta;
    private bool _isMoving;
    private bool _isRotating;
    private float _xRotation;

    private Transform playerTransform;
    private float yOffset = 10.0f;
    private float zOffset = 10.0f;

    [SerializeField] private float cameraMoveSpeed = 10.0f;
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

    /*public void OnMove(InputAction.CallbackContext context)
    {
        _isMoving = context.started || context.performed;
    }*/

    public void OnRotate(InputAction.CallbackContext context)
    {
        _isRotating = context.started || context.performed;
    }

    // LateUpdate is called once per frame at the end of each frame
    void LateUpdate()
    {
        transform.position = Player.transform.position + offset; // Maintain the same offset as the player travels

        /*if ( _isMoving )
        {
            var position = transform.right * (_delta.x * -cameraMoveSpeed);
            position += transform.up * (_delta.y * -cameraMoveSpeed);
            transform.position += position * Time.deltaTime;
        }*/

        if ( _isRotating )
        {
            transform.LookAt(playerTransform.position);
            offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up) * offset;
            transform.position = playerTransform.position + offset;
        }
    }
}
