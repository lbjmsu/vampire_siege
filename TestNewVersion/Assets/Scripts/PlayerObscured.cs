using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObscured : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Camera playerCam;
    public Material opaqueMaterial;
    public Material translucentMaterial;

    [Header("Set dynamically")]
    public Vector3 targetDirection;
    public GameObject previousObjectHit;

    private RaycastHit rayCollision;

    // Start is called before the first frame update
    void Start()
    {
        targetDirection = playerCam.transform.position - gameObject.transform.position;
        targetDirection.Normalize();

        previousObjectHit = playerCam.gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //  It hit an object
        if(Physics.Raycast(transform.position, targetDirection, out rayCollision, 30f))
        {
            Debug.Log(rayCollision.transform.gameObject.name + " Hit");
            //  If the ray hits a different object than before
            if(rayCollision.transform.gameObject != previousObjectHit)
            {
                //  If gameObject hit is not the camera, make the gameObject translucent
                rayCollision.transform.gameObject.GetComponent<MeshRenderer>().material = translucentMaterial;

                //  Set the new previous hit object
                previousObjectHit = rayCollision.transform.gameObject;
            }
        }
        //  It did not
        else
        {
            Debug.Log("Camera Hit");
            //  Restore opacity to previous gameobject when camera is hit
            if(previousObjectHit != playerCam.gameObject)
                previousObjectHit.GetComponent<MeshRenderer>().material = opaqueMaterial;

            previousObjectHit = playerCam.gameObject;
        }
    }
}
