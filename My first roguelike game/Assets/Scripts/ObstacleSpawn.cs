using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawn : MonoBehaviour
{
    public GameObject[] obstacles;
    public int numOfObstacles = 10;
    public float xySpawnLocation = 50.0f;
    public float minDistanceBetweenObstacles = 1.0f;
    
    private List<Vector2> spawnedObstacleLocations = new List<Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        ObstacleSpawnRandomly();
    }

    // Spawn Obstacles At Start Randomly
    void ObstacleSpawnRandomly()
    {
        for (int i = 0; i < numOfObstacles; i++)
        {
            Vector2 obstacleLocation = Vector2.zero;
            bool locationFound = false;
            int attemps = 0;
            int maxAttemps = 100;

            while (!locationFound && attemps < maxAttemps)
            {
                obstacleLocation = new Vector2(Random.Range(-xySpawnLocation, xySpawnLocation), Random.Range(-xySpawnLocation, xySpawnLocation));
                locationFound = true;

                foreach (Vector2 existingLocation in spawnedObstacleLocations)
                {
                    if (Vector2.Distance(obstacleLocation, existingLocation) < minDistanceBetweenObstacles)
                    {
                        locationFound = false;
                        break;
                    }
                }

                if (locationFound)
                {
                    Collider2D[] hitColliders = Physics2D.OverlapCircleAll(obstacleLocation, minDistanceBetweenObstacles / 2f);
                    foreach (Collider2D hitCollider in hitColliders)
                    {
                        if (hitCollider.CompareTag("Obstacle") || hitCollider.CompareTag("Wall") || hitCollider.CompareTag("Player"))
                        {
                            locationFound = false;
                            break;
                        }
                    }
                }

                attemps++;
            }

            if (locationFound)
            {
                int obstacleIndex = Random.Range(0, obstacles.Length);
                Quaternion obstacleRotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));

                Instantiate(obstacles[obstacleIndex], obstacleLocation, obstacleRotation);
                spawnedObstacleLocations.Add(obstacleLocation);
            }
            else
            {
                Debug.LogWarning($"Could not find a unique location for an obstacle after {maxAttemps} attemps.");
            }
        }
    }
}
