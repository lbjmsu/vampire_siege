using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float speed = 1;
    public float rotationSmoothness = 1;
    public float velocitySmoothness = 1;
    public Camera mainCamera;

    [Header("Set dynamically")]
    public Vector3 velocityVector;

    private bool movingVertically = false;
    private bool movingHorizontally = false;

    private Rigidbody playerRB;

    private float toRotateTo;
    private float xRotation;
    private float yRotation;
    string directionString;

    private void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //  If the player is pressing a button...
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            //  Gets input velocity
            velocityVector.x = Input.GetAxis("Horizontal");
            velocityVector.z = Input.GetAxis("Vertical");

            //  Determine which direction the player is moving
            CheckMovementOfPlayer();

            //  Instantiate local variables for rotation
            xRotation = velocityVector.x * 90;
            yRotation = 180 - (velocityVector.z * 90 + 90);

            //  If moving in the negative x direction, make sure that the rotation in the z direction is also negative
            //      such that averaging between them is possible.
            if (xRotation < 0)
            {
                yRotation *= -1;
            }

            //  If the player is moving both vertically and horizontally, average the rotation
            if(movingVertically && movingHorizontally)
            {
                toRotateTo = (xRotation + yRotation) / 2;
                /*directionString = "Diagonal";*/
            }
            //  If the player is only moving vertically, rotate to vertical rotation
            else if (movingVertically)
            {
                toRotateTo = yRotation;
                /*directionString = "Vertical";*/
            }
            //  If the player is only moving horizontally, rotate to horizontal rotation
            else if (movingHorizontally)
            {
                toRotateTo = xRotation;
                /*directionString = "Horizontal";*/
            }
            //  Otherwise, return because the player is not moving
            else
            {
                return;
            }

            //  Reset movement variables
            movingHorizontally = false;
            movingVertically = false;

            //  Debug
            /*Debug.Log("Horizontal: " + xRotation + " | Vertical: " + yRotation +
                " | Direction: " + toRotateTo + " | Moving " + directionString);*/

            //  Rotate the player to this new rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, toRotateTo, 0),
                rotationSmoothness * Time.deltaTime * (Input.GetKey(KeyCode.DownArrow) ? 0.7f : 1));
        }

        //  else, velocity vector lerp towards 0
        else
        {
            velocityVector = Vector3.Lerp(velocityVector, Vector3.zero, velocitySmoothness * Time.deltaTime);
        }

        //  Either way, move by velocity vector
        playerRB.MovePosition(transform.position + velocityVector * speed * Time.deltaTime);
        mainCamera.transform.position = transform.position + new Vector3(0, 17, -4);
    }

    private void CheckMovementOfPlayer()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            movingVertically = true;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            movingHorizontally = true;
        }
    }
}
