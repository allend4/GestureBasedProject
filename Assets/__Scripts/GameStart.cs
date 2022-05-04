using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    private Vector3 ballPos;

    //Move the ball
    // Score

    // Start is called before the first frame update
    void Start()
    {
        ballPos = GameObject.FindGameObjectWithTag("Icosphere").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
