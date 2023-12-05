using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bee : MonoBehaviour
{

    [Header("Set These")]
    public float moveSpeed = 3;
    public GameObject Walls;

    [Header("Already Set")]
    //private bool isDragging = false;
    private Vector3 offset;
    private Vector3 targetPosition;
    private Vector3 bounds;

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
    }

    private void Update()
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
}
