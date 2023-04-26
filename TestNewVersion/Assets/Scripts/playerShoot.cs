using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShoot : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 40;

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(gameObject.name);
        if (gameObject.name == "PlayerOne" && Input.GetKeyDown(KeyCode.Tab))
        {
            TempFire();
        }
        else if (gameObject.name == "PlayerTwo" && Input.GetKeyDown(KeyCode.Space))
        {
            TempFire();
        }
    }
    void TempFire()
    {
        GameObject proGO = Instantiate<GameObject>(projectilePrefab);

        //  player position + speed * direction * time.deltaTime
        proGO.transform.position = transform.position +
            PlayerMove.speedStatic * new Vector3(Mathf.Cos(PlayerMove.ToCartestian(transform.eulerAngles.y) * Mathf.Deg2Rad), 0,
            Mathf.Sin(PlayerMove.ToCartestian(transform.eulerAngles.y) * Mathf.Deg2Rad)) * Time.deltaTime * 2;

        Rigidbody rigidB = proGO.GetComponent<Rigidbody>();
        rigidB.velocity = transform.forward * projectileSpeed;
    }
}
