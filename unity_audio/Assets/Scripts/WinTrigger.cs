using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public Timer TimerScript;

    void Start()
    {
        if (TimerScript == null)
        {
            Debug.LogError("Timer Script is not assigned in inspector");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TimerScript.Win(); // Call Win method when the player touches the flag
            AudioManager.Instance.StopBGM();
            AudioManager.Instance.PlayVictory();
        }
    }
}
