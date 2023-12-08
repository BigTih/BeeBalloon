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
    public GameObject Walls;
    public int lives = 3;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI livesText;
    public Slider scoreText;
    public GameObject GameOverScreen;

    [Header("Already Set")]
    //private bool isDragging = false;
    private Vector3 offset;
    private Vector3 targetPosition;
    private Vector3 bounds;
    private Vector3 spawnPoint;
    private bool isDragging = false;

    private Timer timeRemaining;
    private LivesScript livesScript;
    private ScoreSlider scoreSlider;
    private GameOver gameOver;
    
    private static readonly HashSet<Transform> WallSet = new HashSet<Transform>();

    void Awake()
    {
        foreach(Transform wall in Walls.transform)
        {
            WallSet.Add(wall);
        }
    }

    void OnDestroy()
    {
        foreach (Transform wall in Walls.transform)
        {
            WallSet.Remove(wall);
        }
    }

    void Start()
    {
        // Initialize the target position to the object's current position.
        targetPosition = transform.position;
        spawnPoint = transform.position;
        timeRemaining = timerText.GetComponent<Timer>();
        livesScript = livesText.GetComponent<LivesScript>();
        scoreSlider = scoreText.GetComponent<ScoreSlider>();
        gameOver = GameOverScreen.GetComponent<GameOver>();
    }

    private void Update()
    {
        livesScript.SetLives(lives);

        if (lives > 0 && timeRemaining.timeRemaining>0)
        {
            if (isDragging)
            {
                // If not dragging, smoothly move towards the target position.
                transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

                // Update the target position based on the mouse position.
                targetPosition = GetMouseWorldPosition();

                // Call function to find the closest wall to the bee object
                GameObject wall = ClosestWall();

                Vector3 boundsCheck = transform.position;

                // Only clamp the values that are given from the closest wall depending on if it is a vertical wall or horizontal
                if (wall.transform.eulerAngles.z == 90)
                {
                    if (transform.position.y < wall.transform.position.y)
                    {
                        boundsCheck.y = Mathf.Clamp(boundsCheck.y, -7, wall.transform.position.y);
                    }
                    else
                    {
                        boundsCheck.y = Mathf.Clamp(boundsCheck.y, wall.transform.position.y, 12);
                    }
                }
                else
                {
                    if (transform.position.x < wall.transform.position.x)
                    {
                        boundsCheck.x = Mathf.Clamp(boundsCheck.x, -9.5f, wall.transform.position.x);
                    }
                    else
                    {
                        boundsCheck.x = Mathf.Clamp(boundsCheck.x, wall.transform.position.x, 9.5f);
                    }
                }

                transform.position = boundsCheck;
            }
        }
        else
        {
            if(lives == 0)
            {
                gameOver.SetUp("You ran out of lives");
            }
            else if(timeRemaining.timeRemaining < 1)
            {
                gameOver.SetUp("You ran out of time");
            }
            timeRemaining.StopTimer();
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

    // Loops through wall hash set to find the closest current wall to the bee object
    private GameObject ClosestWall()
    {
        GameObject closest = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (Transform obj in WallSet)
        {
            float distance = Vector3.Distance(obj.position, currentPosition);
            if (distance < minDistance)
            {
                closest = obj.gameObject;
                minDistance = distance;
            }
        }
        return closest;
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject collider = collision.gameObject;

        if(collider.tag == "redboxofdoom" || collider.tag == "bomb")
        {
            // Bee will lose one life and also stop moving when hit by either the boxes or bombs
            isDragging = false;
            lives--;
            transform.position = spawnPoint;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        else if(collider.tag == "balloon")
        {
            scoreSlider.score++;
            Destroy(collider);
        }
    }

}
