using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreSlider : MonoBehaviour
{
    public Slider scoreSlider;
    public TextMeshProUGUI scoreText;
    public static int score = 0;

    void Start()
    {
        scoreSlider.maxValue = 100;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score + " points";
        scoreSlider.value = score;
    }
}
