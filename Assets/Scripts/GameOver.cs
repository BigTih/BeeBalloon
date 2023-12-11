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

    // Called when you run out of lives or the time runs out, also when you beat the game
    // reason argument is the text at top of screen giving why the game over screen shows up
    public void SetUp(string reason)
    {
        scoreText = scoreSlider.GetComponent<ScoreSlider>();
        finalScoreText.text = "Score: " + ScoreSlider.score;
        deathReason.text = reason;
        gameObject.SetActive(true);
    }
}
