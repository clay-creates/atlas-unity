using UnityEngine;

public class Timer : MonoBehaviour
{
    public UnityEngine.UI.Text TimerText;
    private float elapsedTime = 0f;

    private bool isRunning = false;
    private bool isPaused = false;

    void Start()
    {
        if (TimerText == null)
        {
            Debug.LogError("TimerText is not assigned in the inspector");
        }
        else
        {
            TimerText.text = "0:00.00";
        }
    }

    void Update()
    { 
        if (isRunning)
        {
           isPaused = false;
           elapsedTime += Time.deltaTime;
           UpdateTimerText(elapsedTime);
           Debug.Log($"Timer running: {elapsedTime}");
        }

        if (isPaused)
        {
            isRunning = false;
            UpdateTimerText(elapsedTime);
            Debug.Log("Timer is paused.");
        }
        
    }

    private void UpdateTimerText(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time % 60F);
        float fraction = (time * 100) % 100;

        TimerText.text = $"{minutes}:{seconds:00}.{fraction:00}";
    }

    public void StartTimer()
    {
        isRunning = true;
        isPaused = false;
        Debug.Log("Timer started");
    }

    public void StopTimer()
    {
        isRunning = false;
        isPaused = true;
        Debug.Log("Timer stopped");
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
        TimerText.text = "0:00.00";
        isRunning = false;
        Debug.Log("Timer reset");
    }
}
