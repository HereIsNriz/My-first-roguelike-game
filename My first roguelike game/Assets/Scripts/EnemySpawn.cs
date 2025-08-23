using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemy;
    public bool bossTurn;

    [SerializeField] private GameObject boss;
    private GameManager gameManager;
    private float xySpawnLocation = 50.0f;
    private float spawnDelay = 2.0f;
    private float enemySpawnRate;
    private bool bossSpawned;
    private int enemyIndex;

    // Start is called before the first frame update
    void Start()
    {
        bossTurn = false;
        bossSpawned = false;

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        // Randomly spawn enemy
        Invoke("RandomEnemySpawnLocation", spawnDelay);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEnemySpawnRate();

        SpawnTheBoss();
    }

    private void UpdateEnemySpawnRate()
    {
        if (gameManager.timeAmount < 121)
        {
            bossTurn = true;
        }
        else if (gameManager.timeAmount < 301)
        {
            enemySpawnRate = 1;
            enemyIndex = Random.Range(0, enemy.Length);
        }
        else if (gameManager.timeAmount < 481)
        {
            enemySpawnRate = 2;
            enemyIndex = Random.Range(0, enemy.Length - 1);
        }
        else if (gameManager.timeAmount < 601)
        {
            enemySpawnRate = 3;
            enemyIndex = Random.Range(0, enemy.Length - 2);
        }
    }

    private void RandomEnemySpawnLocation()
    {
        // Randomly spwan enemy from everywhere
        if (gameManager.isGameRunning && !bossTurn)
        {
            Vector2 enemySpawnLoc = new Vector2(Random.Range(-xySpawnLocation, xySpawnLocation), Random.Range(-xySpawnLocation, xySpawnLocation));

            Instantiate(enemy[enemyIndex], enemySpawnLoc, Quaternion.identity);

            Invoke("RandomEnemySpawnLocation", enemySpawnRate);
        }
    }
    
    private void SpawnTheBoss()
    {
        if (bossTurn && !bossSpawned)
        {
            boss.gameObject.SetActive(true);
            bossSpawned = true;
        }
    }
}
