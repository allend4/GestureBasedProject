using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject ball;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        // get offset
        offset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // follow ball
        if (ball.transform.position.z < 5)
            transform.position = ball.transform.position + offset;
    }
}
