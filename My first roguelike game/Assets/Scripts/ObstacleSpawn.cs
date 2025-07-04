using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawn : MonoBehaviour
{
    public GameObject[] obstacles;

    private int numOfObstacles = 20;
    private float xySpawnLocation = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        ObstacleSpawnRandomly();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Spawn Obstacles At Start Randomly
    void ObstacleSpawnRandomly()
    {
        for (int i = 0; i < numOfObstacles; i++)
        {
            int obstacleIndex = Random.Range(0, obstacles.Length);
            Vector2 obstacleLocation = new Vector2(Random.Range(-xySpawnLocation, xySpawnLocation), Random.Range(-xySpawnLocation, xySpawnLocation));
            Quaternion obstacleRotation = Quaternion.Euler(0, 0, Random.Range(0, 360.0f));

            Instantiate(obstacles[obstacleIndex], obstacleLocation, obstacleRotation);
        }
    }
}
