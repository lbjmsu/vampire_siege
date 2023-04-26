using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pewPew : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 40;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TempFire();
        }
    }
    void TempFire()
    {
        GameObject proGO = Instantiate<GameObject>(projectilePrefab);
        proGO.transform.position = transform.position;
        Rigidbody rigidB = proGO.GetComponent<Rigidbody>();
        rigidB.velocity = transform.forward * projectileSpeed;
    }
}
