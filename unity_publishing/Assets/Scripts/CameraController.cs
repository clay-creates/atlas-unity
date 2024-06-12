using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player; // Reference to the Player GameObject
    private Vector3 offset; // Distance between player and camera

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - Player.transform.position; // Calculate the initial offset
    }

    // LateUpdate is called once per frame at the end of each frame
    void LateUpdate()
    {
        transform.position = Player.transform.position + offset; // Maintain the same offset as the player travels
    }
}
