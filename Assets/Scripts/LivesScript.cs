using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivesScript : MonoBehaviour
{
    public int lives = 3;
    public TextMeshProUGUI livesText;

    // Update is called once per frame
    void Update()
    {
        livesText.text = "Lives: " + lives;
    }

    public void SetLives(int input)
    {
        lives = input;
    }
}
