using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemy;

    private int enemyToSpawn = 3;
    private float xySpawnLocation = 50.0f;
    private float spawnDelay = 2.0f;
    private float enemyTimeDelayFirst = 1.0f;
    private float enemyTimeDelaySecond = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Randomly spawn enemy
        Invoke("RandomEnemySpawnLocation", spawnDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RandomEnemySpawnLocation()
    {
        // Randomly spwan enemy from everywhere
        int enemyIndex = Random.Range(0, enemy.Length);
        Vector2 enemySpawnLoc = new Vector2(Random.Range(-xySpawnLocation, xySpawnLocation), Random.Range(-xySpawnLocation, xySpawnLocation));

        for (int i = 0; i < enemyToSpawn; i++)
        {
            Instantiate(enemy[Random.Range(0, enemy.Length)], new Vector2(Random.Range(-xySpawnLocation, xySpawnLocation), Random.Range(-xySpawnLocation, xySpawnLocation)), transform.rotation);
        }

        Invoke("RandomEnemySpawnLocation", Random.Range(enemyTimeDelayFirst, enemyTimeDelaySecond));
    }
}
