using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Assign this in the inspector
    public static float timeRemaining = 240; // Set your countdown time in seconds here

    private static bool timer = true;

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

    public static void StopTimer()
    {
        timer = false;
    }

    public static void StartTimer()
    {
        timer = true;
    }

}
