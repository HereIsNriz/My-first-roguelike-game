using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public float rotateSpeed;
    public int damage;
    public int maxLives;

    [SerializeField] private Slider enemyHealthBar;
    private GameManager gameManager;
    private Rigidbody2D rb;
    private GameObject player;
    private EnemySpawn enemySpawner;
    private int currentLives;

    // Start is called before the first frame update
    void Start()
    {
        currentLives = maxLives;

        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        enemySpawner = GameObject.Find("Enemy Spawner").GetComponent<EnemySpawn>();

        enemyHealthBar.maxValue = maxLives;
        enemyHealthBar.value = currentLives;
        enemyHealthBar.fillRect.gameObject.SetActive(true);
    }

    // Update is called once per frame
    private void Update()
    {
        if (currentLives <= 0)
        {
            Destroy(gameObject);
            gameManager.enemyDeathCount++;
        }

        if (!gameManager.isGameRunning || enemySpawner.bossTurn)
        {
            Destroy(gameObject);
        }
    }

    // FixedUpdate for physic
    void FixedUpdate()
    {
        if (gameManager.isGameRunning)
        {
            Vector2 direction = (Vector2)player.transform.position - (Vector2)rb.transform.position;

            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.up).z;

            rb.angularVelocity = -rotateAmount * rotateSpeed;

            rb.velocity = transform.up * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BulletController bullet = collision.gameObject.GetComponent<BulletController>();

        if (collision.gameObject.CompareTag("Bullet"))
        {
            currentLives -= bullet.damage;
            enemyHealthBar.value = currentLives;
        }
    }
}
