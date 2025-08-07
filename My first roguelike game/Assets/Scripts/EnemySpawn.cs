using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemy;
    // Boss
    public bool bossTurn;

    private GameManager gameManager;
    private float xySpawnLocation = 50.0f;
    private float spawnDelay = 2.0f;
    private float enemySpawnRate;

    // Start is called before the first frame update
    void Start()
    {
        bossTurn = false;

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        // Randomly spawn enemy
        Invoke("RandomEnemySpawnLocation", spawnDelay);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEnemySpawnRate();
    }

    private void UpdateEnemySpawnRate()
    {
        if (gameManager.timeAmount < 30)
        {
            bossTurn = true;
        }
        else if (gameManager.timeAmount < 60)
        {
            enemySpawnRate = 0.5f;
        }
        else if (gameManager.timeAmount < 120)
        {
            enemySpawnRate = 1;
        }
        else if (gameManager.timeAmount < 180)
        {
            enemySpawnRate = 2;
        }
        else if (gameManager.timeAmount < 240)
        {
            enemySpawnRate = 3;
        }
        else if (gameManager.timeAmount < 301)
        {
            enemySpawnRate = 4;
        }
    }

    private void RandomEnemySpawnLocation()
    {
        // Randomly spwan enemy from everywhere
        if (gameManager.isGameRunning && !bossTurn)
        {
            int enemyIndex = Random.Range(0, enemy.Length);
            Vector2 enemySpawnLoc = new Vector2(Random.Range(-xySpawnLocation, xySpawnLocation), Random.Range(-xySpawnLocation, xySpawnLocation));

            Instantiate(enemy[enemyIndex], enemySpawnLoc, enemy[enemyIndex].gameObject.transform.rotation);

            Invoke("RandomEnemySpawnLocation", enemySpawnRate);
        }
    }
    
    private void SpawnTheBoss()
    {
        if (bossTurn)
        {
            // Instantiate Boss
        }
    }
}
