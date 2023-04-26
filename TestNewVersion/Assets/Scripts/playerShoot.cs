using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShoot : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 40;

    private Vector3 playerRotationVector;

    // Update is called once per frame
    void Update()
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
        playerRotationVector = new Vector3(Mathf.Cos(PlayerMove.ToCartestian(transform.eulerAngles.y) * Mathf.Deg2Rad), 0,
            Mathf.Sin(PlayerMove.ToCartestian(transform.eulerAngles.y) * Mathf.Deg2Rad));

        //  player position + speed * direction * time.deltaTime
        proGO.transform.position = transform.position + Time.deltaTime * 2 * PlayerMove.speedStatic * playerRotationVector;

        proGO.transform.rotation = Quaternion.Euler(90, transform.eulerAngles.y, 0);

        Rigidbody rigidB = proGO.GetComponent<Rigidbody>();
        rigidB.velocity = transform.forward * projectileSpeed;
    }
}
