using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    static public Balloon S;
    
    private Rigidbody rb;
    
    void Start()
    {
        S = this;
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision coll) {
        if(coll.gameObject.tag == "bee") {
            Destroy(this.gameObject);
        }
    }
}
