using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Timer : MonoBehaviour
{
    public Slider timerSlider;
    public TMP_Text timerText;
    public float gameTime;

    private bool stopTimer;
    // Start is called before the first frame update
    void Start()
    {
        stopTimer = false;
        timerSlider.maxValue = gameTime;
        timerSlider.value = gameTime;
    }

    // Update is called once per frame
    void Update()
    {
        float time = gameTime - Time.time;

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time - minutes * 60f);

        string textTime = string.Format("Time Remaining: {0:0}:{1:00}", minutes, seconds);

        if (time <= 0)
        {
            stopTimer = true;
        }

        if (stopTimer == false)
        {
            timerText.text = textTime;
            timerSlider.value = time;
        }
    }
}
