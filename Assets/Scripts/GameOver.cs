using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI deathReason;
    public Slider scoreSlider;
    public Button restart;
    public Button enterButton;
    public TMP_InputField inputName;
    public TMP_InputField inputFeedback;

    private ScoreSlider scoreText;
    private string filePath;
    private string playerName;
    private string feedback;
    private float startTime;
    private float playDuration;

    void Start()
    {
        // Record the start time when the game starts
        startTime = Time.realtimeSinceStartup;
    }
    
    void Update()
    {
        // Calculate the play duration
        playDuration = Time.realtimeSinceStartup - startTime;
    }

    // Called when you run out of lives or the time runs out, also when you beat the game
    // reason argument is the text at top of screen giving why the game over screen shows up
    public void SetUp(string reason)
    {
        scoreText = scoreSlider.GetComponent<ScoreSlider>();
        finalScoreText.text = "Score: " + ScoreSlider.score;
        deathReason.text = reason;
        gameObject.SetActive(true);
    }

    void SaveGame()
    {
        filePath = Path.Combine(Application.persistentDataPath, "playerStats.txt");

        string dataToSave = $"Player Name: {playerName}\nPlay time: {DateTime.Now}\nPlay Duration: {playDuration} Seconds\nScore: {ScoreSlider.score}\nFeedback: {feedback}\n\n";

        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine(dataToSave);
        }
        Debug.Log("Stats saved to " + filePath);
    }

    public void OnButtonPress()
    {
        feedback = inputFeedback.text;
        playerName = inputName.text;
        inputFeedback.gameObject.SetActive(false);
        inputName.gameObject.SetActive(false);
        enterButton.gameObject.SetActive(false);
        restart.gameObject.SetActive(true);
        SaveGame();
    }
}
