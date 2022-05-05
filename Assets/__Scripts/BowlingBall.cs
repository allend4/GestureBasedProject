using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBall : MonoBehaviour
{
    public float force; // speed of ball
    // initialization
    private List<Vector3> pinPositions;
    private List<Quaternion> pinRotations;
    private Vector3 ballPosition;

    // Start is called before the first frame update
    void Start()
    {
        var pins = GameObject.FindGameObjectsWithTag("Pin");
        pinPositions = new List<Vector3>();
        pinRotations = new List<Quaternion>();
        foreach (var pin in pins)
        {
            pinPositions.Add(pin.transform.position);
            pinRotations.Add(pin.transform.rotation);
        }

        ballPosition = GameObject.FindGameObjectWithTag("Ball").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // ball straight - force
        if (Input.GetKeyDown(KeyCode.Space))
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, force));
        // ball left - impulse gradual
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            GetComponent<Rigidbody>().AddForce(new Vector3(1, 0, 0), ForceMode.Impulse);
        // ball right - impulse gradual
        if (Input.GetKeyDown(KeyCode.RightArrow))
            GetComponent<Rigidbody>().AddForce(new Vector3(-1, 0, 0), ForceMode.Impulse);
        // resets pins
        if (Input.GetKeyDown(KeyCode.R))
        {
            var pins = GameObject.FindGameObjectsWithTag("Pin");

            for (int i = 0; i < pins.Length; i++)
            {
                var pinPhysics = pins[i].GetComponent<Rigidbody>();
                pinPhysics.velocity = Vector3.zero;
                pinPhysics.position = pinPositions[i];
                pinPhysics.rotation = pinRotations[i];
                pinPhysics.velocity = Vector3.zero;
                pinPhysics.angularVelocity = Vector3.zero;

                var ball = GameObject.FindGameObjectWithTag("Ball");
                ball.transform.position = ballPosition;
                ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
                ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }
        }

        // reset ball
        if (Input.GetKeyDown(KeyCode.B))
        {
            var ball = GameObject.FindGameObjectWithTag("Ball");
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            ball.transform.position = ballPosition;
        }
    }

}
