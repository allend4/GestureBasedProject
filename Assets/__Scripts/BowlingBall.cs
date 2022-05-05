using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowlingBall : MonoBehaviour
{
    public float force; // speed of ball
    // initialization
    private List<Vector3> pinPositions;
    private List<Quaternion> pinRotations;
    private Vector3 ballPosition;
    public AudioClip noise;

    // Score
    public GameObject ball;
    int score = 0;
    int turnCounter = 0;
    GameObject[] pins;
    public Text scoreUI;

    Vector3[] positions;

    // Start is called before the first frame update
    void Start()
    {
        //stored pins in the array
        pins = GameObject.FindGameObjectsWithTag("Pin");
        //size of position = pin array
        positions = new Vector3[pins.Length];

        for(int i = 0; i < pins.Length; i++)
        {
            //save position in ney array
            positions[i] = pins[i].transform.position;
        }

        //var pins = GameObject.FindGameObjectsWithTag("Pin");
        pinPositions = new List<Vector3>();
        pinRotations = new List<Quaternion>();
        foreach (var pin in pins)
        {
            pinPositions.Add(pin.transform.position);
            pinRotations.Add(pin.transform.rotation);
        }

        ballPosition = GameObject.FindGameObjectWithTag("Ball").transform.position;

        noise = GetComponent<AudioClip>();
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

        if (Input.GetKeyDown(KeyCode.Space) || ball.transform.position.y < -20)
        {
            CountPinsDown();
            turnCounter++;
            //ResetPins();
        }

        
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
    void CountPinsDown()
    {
        for (int i = 0; i < pins.Length; i++)
        {
            //eulerAngles  is checking if its larger than 5 ansd smaller than 355
            if (pins[i].transform.eulerAngles.z > 5 && pins[i].transform.eulerAngles.z < 355 &&
                pins[i].activeSelf)
                
            {
                score++;
                pins[i].SetActive(false);
            }
        }

        //Debug.Log(score);
        scoreUI.text = score.ToString();
    }

    // play audio on collision with pins
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pin")
            GetComponent<AudioSource>().Play();
    }
 
}
