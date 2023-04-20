using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireSpawn : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject[] prefabEnemies;
    public float enemySpawnPerSecond = .5f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnEnemy()
    {
        int ndx = Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);

        Vector3 pos = Vector3.zero;
        float xMin = 475;
        float xMax = 525;
        float zMin = 475;
        float zMax = 525;
        pos.x = Random.Range(xMin, xMax);
        pos.y = 1;
        pos.z = Random.Range(zMin, zMax);
        go.transform.position = pos;

        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
    }
}
