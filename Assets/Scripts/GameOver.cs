using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI deathReason;
    public Slider scoreSlider;

    private ScoreSlider scoreText;

    public void SetUp(string reason)
    {
        scoreText = scoreSlider.GetComponent<ScoreSlider>();
        finalScoreText.text = "Score: " + scoreText.score;
        deathReason.text = reason;
        gameObject.SetActive(true);
    }
}
