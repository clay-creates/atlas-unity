using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public Camera mainCamera;
    public Camera cutsceneCamera;
    public PlayerController playerController;
    public Canvas timerCanvas;

    void Start()
    {
        Debug.Log("CutsceneController Start: Initializing...");

        playerController.enabled = false;
        timerCanvas.gameObject.SetActive(false);

        mainCamera.enabled = false;
        Debug.Log("Main Camera disabled");

        cutsceneCamera.enabled = true;
        Debug.Log("Cutscene Camera enabled");

    }

    public void OnCutsceneEnd()
    {
        Debug.Log("Cutscene ended");

        // Enable the main camera
        mainCamera.gameObject.SetActive(true);
        mainCamera.enabled = true;
        Debug.Log("Main Camera enabled");

        // Disable the cutscene camera
        cutsceneCamera.gameObject.SetActive(false);
        cutsceneCamera.enabled = false;
        Debug.Log("Cutscene Camera disabled");

        // Enable PlayerController
        playerController.enabled = true;
        Debug.Log("PlayerController enabled");

        // Enable TimerCanvas but timer should not start yet
        timerCanvas.gameObject.SetActive(true);
        Debug.Log("TimerCanvas enabled");

        // Disable CutsceneController
        this.enabled = false;
    }
}
