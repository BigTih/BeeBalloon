using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Bee : MonoBehaviour
{

    [Header("Set These")]
    public float moveSpeed = 3;
    public static int lives = 3;

    public TextMeshProUGUI livesText;
    public GameObject GameOverScreen;
    public GameObject Balloons;

    [Header("Already Set")]
    private Vector3 offset;
    private Vector3 targetPosition;
    private Vector3 bounds;
    private Vector3 spawnPoint;
    private bool isDragging = false;
    private Vector3 boundsCheck;
    private int layerMask;
    private static int level = 1;

    private LivesScript livesScript;
    private GameOver gameOver;

    public GameObject explosionPrefab;
    public GameObject explosionWithRedPrefab;
    // To track how many balloons are on screen
    private HashSet<Transform> BalloonSet = new HashSet<Transform>();

    void Awake()
    {
        foreach(Transform balloon in Balloons.transform)
        {
            BalloonSet.Add(balloon);
        }
    }

    void Start()
    {
        // Initialize the target position to the object's current position.
        targetPosition = transform.position;
        spawnPoint = transform.position;
        livesScript = livesText.GetComponent<LivesScript>();
        gameOver = GameOverScreen.GetComponent<GameOver>();

        // For detecting when the cursor is on the other side of the wall
        layerMask = 1 << 6;
    }

    private void Update()
    {
        livesScript.SetLives(lives);
        Timer.StartTimer();
        if (lives > 0 && Timer.timeRemaining > 0)
        {
            if (isDragging)
            {
                // Smoothly move towards the target position.
                transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

                // Update the target position based on the mouse position.
                targetPosition = GetMouseWorldPosition();

                RaycastHit hit;
                if (Physics.Raycast(transform.position, targetPosition - transform.position, out hit, Vector3.Distance(transform.position, targetPosition), layerMask))
                {
                    // If a wall is hit, adjust the target position to the hit point
                    targetPosition = hit.point;
                }
                else
                {
                    targetPosition = GetMouseWorldPosition();
                }
            }
        }
        else
        {
            // Different prompts for different kind of game over possibilities
            if (lives == 0)
            {
                gameOver.SetUp("You ran out of lives");
            }
            else if (Timer.timeRemaining < 1)
            {
                gameOver.SetUp("You ran out of time");
            }
            Time.timeScale = 0f;
        }

        // Changes level to correct one following all balloons being popped
        if(BalloonSet.Count == 0)
        {
            switch(level)
            {
                case 1:
                    SceneManager.LoadScene("Level2");
                    level++;
                    break;

                case 2:
                    SceneManager.LoadScene("Level3");
                    level++;
                    break;

                case 3:
                    SceneManager.LoadScene("Level4");
                    level++;
                    break;

                case 4:
                    gameOver.SetUp("You won the game!");
                    Time.timeScale = 0f;
                    break;
            }
        }
    }

    // Click on the bee to get it moving
    void OnMouseDown()
    {
        isDragging = true;
    }

    private Vector3 GetMouseWorldPosition()
    {
        // Get the mouse position in the world space.
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane + 9.7f;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject collider = collision.gameObject;

        if(collider.tag == "redboxofdoom" )
        {
            // Bee will lose one life and also stop moving when hit by either the boxes or bombs
            isDragging = false;
            lives--;

            ExplodeWithRed();

            transform.position = spawnPoint;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        if ( collider.tag == "bomb")
        {
            // Bee will lose one life and also stop moving when hit by either the boxes or bombs
            isDragging = false;
            lives--;

            Explode();

            transform.position = spawnPoint;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        else if(collider.tag == "balloon")
        {
            ScoreSlider.score++;
            BalloonSet.Remove(collider.transform);
            Destroy(collider);
        }
    }
    private void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }

    private void ExplodeWithRed()
    {
        Instantiate(explosionWithRedPrefab, transform.position, Quaternion.identity);
    }
    // Time scale is set to zero when pause button is clicked, everything in game freezes
    public void pauseGame()
    {
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    // Used for both start game and after game over
    public void restart()
    {
        SceneManager.LoadScene("Level1");
        lives = 3;
        ScoreSlider.score = 0;
        Timer.timeRemaining = 240;
        Time.timeScale = 1f;
    }
}
