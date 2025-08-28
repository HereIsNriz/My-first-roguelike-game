using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyWAbilityController : MonoBehaviour
{
    public int damage;
    public int enemyScore;

    [SerializeField] private Slider enemyHealthBar;
    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private GameObject enemyBulletLocation;
    [SerializeField] private GameObject enemyXp;
    [SerializeField] private int maxLives;
    [SerializeField] private int speed;
    [SerializeField] private int rotateSpeed;

    private GameManager gameManager;
    private EnemySpawn enemySpawner;
    private Rigidbody2D rb;
    private GameObject player;
    private int currentLives;
    private int delayAfterShoot = 2;

    // Start is called before the first frame update
    void Start()
    {
        currentLives = maxLives;

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        enemySpawner = GameObject.Find("Enemy Spawner").GetComponent<EnemySpawn>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");

        enemyHealthBar.maxValue = maxLives;
        enemyHealthBar.value = currentLives;
        enemyHealthBar.fillRect.gameObject.SetActive(true);

        StartCoroutine(EnemyShoot());
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLives <= 0)
        {
            Instantiate(enemyXp, transform.position, Quaternion.identity);
            Destroy(gameObject);

            gameManager.enemyDeathCount++;
            gameManager.UpdateScore(enemyScore);
        }

        if (enemySpawner.bossTurn || !gameManager.isGameRunning)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
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

    IEnumerator EnemyShoot()
    {
        while (gameManager.isGameRunning)
        {
            yield return new WaitForSeconds(delayAfterShoot);
            Instantiate(enemyBullet, enemyBulletLocation.transform.position, transform.rotation);
        }
    }
}
