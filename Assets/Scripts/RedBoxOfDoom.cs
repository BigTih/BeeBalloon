using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBoxOfDoom : MonoBehaviour
{
    [Header("Set Me")]
    public float maxX = 10.0f;
    public float maxY = 10.0f;
    public float minX = 10.0f;
    public float minY = 10.0f;
    public float speed = 0.1f;

    private int directionX = 1;
    private int directionY = 1;

    public Vector3 pos{
        get { return(this.transform.position); }
        set { this.transform.position = value; }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move(){
        if (pos.x > maxX){
            directionX = -1;
        }
        else if (pos.x < minX){
            directionX = 1;
        }

        if (pos.y > maxY){
            directionY = -1;
        }
        else if (pos.y < minY){
            directionY = 1;
        }

        if (minX != maxX){
            Vector3 tempPos = pos;
            tempPos.x += directionX * speed;
            pos = tempPos;
        }
        if (minY != maxY){
            Vector3 tempPos = pos;
            tempPos.y += directionY * speed;
            pos = tempPos;
        }
    }
}
