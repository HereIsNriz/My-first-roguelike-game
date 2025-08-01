using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemy;

    private GameManager gameManager;
    private int enemyToSpawn = 3;
    private float xySpawnLocation = 50.0f;
    private float spawnDelay = 2.0f;
    private float enemyTimeDelayFirst = 1.0f;
    private float enemyTimeDelaySecond = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
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

        if (gameManager.isGameRunning)
        {
            for (int i = 0; i < enemyToSpawn; i++)
            {
                int enemyIndex = Random.Range(0, enemy.Length);
                Vector2 enemySpawnLoc = new Vector2(Random.Range(-xySpawnLocation, xySpawnLocation), Random.Range(-xySpawnLocation, xySpawnLocation));

                Instantiate(enemy[enemyIndex], enemySpawnLoc, enemy[enemyIndex].gameObject.transform.rotation);
            }

            Invoke("RandomEnemySpawnLocation", Random.Range(enemyTimeDelayFirst, enemyTimeDelaySecond));
        }
    }
}
