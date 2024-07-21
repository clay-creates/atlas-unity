using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTrigger : MonoBehaviour
{

    public Timer TimerScript;

    // Start is called before the first frame update
    void Start()
    {
        if (TimerScript == null)
        {
            Debug.LogError("Timer Script is not assigned in inspector");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TimerScript.enabled = true;
            TimerScript.StartTimer();
        }
    }
}
