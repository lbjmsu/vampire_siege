using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vampMove : MonoBehaviour
{
    public GameObject target;
    public GameObject target2;//the enemy's target
    public float moveSpeed = 5; //move speed
    public float rotationSpeed = 5; //speed of turning
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.Find("PlayerOne");
        target2 = GameObject.Find("PlayerTwo");
    }


    void Update()
    {
        if (Mathf.Abs(rb.transform.position.x - target.transform.position.x) < Mathf.Abs(rb.transform.position.x - target2.transform.position.x))
        {
            vTarget();
        }
        else
        {
            vTarget2();
        }

        //rotate to look at the player
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), rotationSpeed * Time.deltaTime);


        //move towards the player
        //transform.position += transform.forward * Time.deltaTime * moveSpeed;

    }
    public void vTarget()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), rotationSpeed * Time.deltaTime);
        transform.position += transform.forward * Time.deltaTime * moveSpeed;
    }
    public void vTarget2()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target2.transform.position - transform.position), rotationSpeed * Time.deltaTime);
        transform.position += transform.forward * Time.deltaTime * moveSpeed;
    }

}