using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb; // RigidBody of the Player
    private float movementX; // Movement along X Axis
    private float movementY; // Movement along Y axis
    public float speed = 20; // Speed of player
    private int score = 0; // Amount of coins that player has collected
    public int health = 5; // Player health
    public Text scoreText; // ScoreText to update as player collects coins
    public Text healthText; // HealthText to update when player loses health
    public GameObject WinLoseBG;
    public Text WinLoseText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get and store RigidBody component

        scoreText = GameObject.Find("ScoreText").GetComponent<Text>(); // get scoreText gameObject
        healthText = GameObject.Find("HealthText").GetComponent<Text>(); // get HealthText gameObject
    }

    void OnMove(InputValue movementValue) // Called when movement is detected
    {
        Vector2 movementVector = movementValue.Get<Vector2>(); // Convert input value into Vector2 for movement
        movementX = movementVector.x; // Store X component of movement
        movementY = movementVector.y; // Store Y component of movement
    }

    private void FixedUpdate() // Called once per fixed frame-rate frame
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY); // Create a 3d movement vectore using X and Y inputs
        rb.AddForce(movement * speed); // Apply force to the Rigidbody to move the player
    }

    void Update() // Called once per frame
    {
        if (health == 0)
        {
            if (WinLoseText != null)
            {
                WinLoseText.GetComponent<Text>().text = "Game Over!"; // Set Victory text to display
                WinLoseText.GetComponent<Text>().color = Color.white; // Change Text Color
            }
            else
            {
                Debug.Log("WinLoseText not found.");
            }

            if (WinLoseBG != null)
            {
                WinLoseBG.GetComponent<Image>().color = Color.red;
                WinLoseBG.SetActive(true);
            }
            else
            {
                Debug.Log("WinLoseBG not found.");
            }
            // Debug.Log("Game Over!");
            // SceneManager.LoadScene("maze");
            score = 0;
            health = 5;

            StartCoroutine(LoadScene(3));
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("menu");
        }
    }

    void OnTriggerEnter(Collider other) // Called when player colides with trigger item
    {
        

        if (other.gameObject.CompareTag("Pickup")) // Checks collision item tag (Pickup)
        {
            other.gameObject.SetActive(false); // Disables / Destroys object
            score++; // Increment score
            SetScoreText();
            //  Debug.Log($"Score: {score}"); // Print updated score to console
        }

        if (other.gameObject.CompareTag("Trap")) // Checks for Trap tag
        {
            health--; // Decrement health
            SetHealthText();
            // Debug.Log($"Health: {health}"); // Print new health
        }

        if (other.gameObject.CompareTag("Goal")) // Checks for Goal tag
        {
            // Debug.Log("You win!"); // Prints victory message

            if (WinLoseText != null)
            {
                WinLoseText.GetComponent<Text>().text = "You win!"; // Set Victory text to display
                WinLoseText.GetComponent<Text>().color = Color.black; // Change Text Color
            }
            else
            {
                Debug.Log("WinLoseText not found.");
            }

            if (WinLoseBG != null)
            {
                WinLoseBG.GetComponent<Image>().color = Color.green;
                WinLoseBG.SetActive(true);
            }
            else
            {
                Debug.Log("WinLoseBG not found.");
            }

            StartCoroutine(LoadScene(3));

        }
    }

    void SetScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}"; // Set ScoreText
        }
    }

    void SetHealthText()
    {
        if (healthText != null)
        {
            healthText.text = $"Health: {health}"; // Set HealthText
        }
    }

    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("maze");
    }
}
