using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Assign this in the inspector
    public float timeRemaining = 10; // Set your countdown time in seconds here

    private bool timer = true;

    private void Update()
    {
        if (timer)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay(timeRemaining);
            }
            else
            {
                timerText.text = "0:00";
                // Optionally add any actions that should happen when the timer ends
            }
        }
    }

    void UpdateTimerDisplay(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
    }

    public void StopTimer()
    {
        timer = false;
    }

}
