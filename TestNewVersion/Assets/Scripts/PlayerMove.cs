using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float speed = 1;
    public float velocitySmoothness = 1;
    public float rotationSpeed = 1;
    public float rotationSmoothness = 1;
    public Camera playerCamera;

    [Header("Set dynamically")]
    public Vector3 movementDirection;
    public float rotationAmount;

    private Rigidbody playerRB;

    public static float speedStatic;

    private void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody>();
        speedStatic = speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //  If player one is pressing a button...
        if (gameObject.name == "PlayerOne")
        {
            MovePlayer();
        }

        //  If player two is pressing a button...
        else if (gameObject.name == "PlayerTwo")
        {
            MovePlayer(false);
        }

        //  Move the camera if player one
        playerCamera.transform.position = transform.position + new Vector3(0, 17, -4);
    }

    /// <summary>
    ///     Allows for the user to move a certain player based on the playerOne boolean. "true" means that playerOne is being controlled.
    ///     "false" means that playerTwo is being controlled.
    /// </summary>
    private void MovePlayer(bool playerOne = true)
    {
        //      SECTION: Creation of the movement/rotation vectors

        //  If player one or player two are moving forward...
        if ((playerOne && Input.GetKey(KeyCode.W)) || (!playerOne && Input.GetKey(KeyCode.UpArrow)))
        {
            //  Movement direction is in the direction the player is facing
            movementDirection = new Vector3(Mathf.Cos(ToCartestian(transform.eulerAngles.y) * Mathf.Deg2Rad), 0,
                Mathf.Sin(ToCartestian(transform.eulerAngles.y) * Mathf.Deg2Rad));

            //  Normalize the vector so that its magnitude is one -- as such it only functions as a direction vector, and the
            //      speed variable dictates its magnitude.
            movementDirection.Normalize();
        }    

        //  If player one or player two are moving backward...
        else if((playerOne && Input.GetKey(KeyCode.S)) || (!playerOne && Input.GetKey(KeyCode.DownArrow)))
        {
            //  Same thing as above, but backwards
            movementDirection = new Vector3(Mathf.Cos(ToCartestian(transform.eulerAngles.y) * Mathf.Deg2Rad), 0,
                Mathf.Sin(ToCartestian(transform.eulerAngles.y) * Mathf.Deg2Rad));

            movementDirection.Normalize();

            //  Makes the player do the opposite movement
            movementDirection *= -1;
        }

        //  Otherwise, movement direction lerps towards zero. Now it will affect the speed of the character.
        else
        {
            movementDirection = Vector3.Lerp(movementDirection, Vector3.zero, velocitySmoothness);
        }

        //  If player one or two are turning left...
        if ((playerOne && Input.GetKey(KeyCode.A)) || (!playerOne && Input.GetKey(KeyCode.LeftArrow)))
        {
            //  Player rotation amount is speed * 1 revolution/second clockwise
            rotationAmount = rotationSpeed * 360 * Time.deltaTime * -1;
        }

        //  If player one or two are turning right...
        else if ((playerOne && Input.GetKey(KeyCode.D)) || (!playerOne && Input.GetKey(KeyCode.RightArrow)))
        {
            //  Player rotation amount is speed * 1 revloution/second counter-clockwise
            rotationAmount = rotationSpeed * 360 * Time.deltaTime;
        }

        //  If player one or two are not turning...
        else
        {
            //  Rotation amount lerps towards zero to smoothly stop rotating.
            rotationAmount = Mathf.Lerp(rotationAmount, 0, rotationSmoothness);
        }

        //      SECTION: Application of the movement/rotation vectors
        
        //  Move towards the movement direction
        playerRB.MovePosition(transform.position + speed * Time.deltaTime * movementDirection);

        //  Rotate the player to this new rotation
        transform.Rotate(new Vector3(0, rotationAmount, 0));
    }

    /// <summary>
    ///     Translates from Unity angles to Cartesian angles.
    /// </summary>
    public static float ToCartestian(float inAngle)
    {
        return (inAngle * -1 + 450) % 360;
    }

    /// <summary>
    ///     Translates from Cartesian angles to Unity angles.
    /// </summary>
    public static float ToUnityAngle(float inAngle)
    {
        return ((inAngle + 90) % -1 - 180) * -1;
    }
}
