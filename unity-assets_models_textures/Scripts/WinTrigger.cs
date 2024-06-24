using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{

    public Timer timerScript;
    public UnityEngine.UI.Text timerText;

    private void OnTriggerEnter(Collider other)
    {
        if (timerScript != null)
        {
            timerScript.enabled = false;
            UpdateTimerTextAppearance();
        }
    }

    private void UpdateTimerTextAppearance()
    {
        if (timerText != null)
        {
            timerText.fontSize = 80;
            timerText.color = Color.green;
        }
    }
}
