using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyWAbilityController : MonoBehaviour
{
    public int damage;
    public int enemyScore;

    [SerializeField] private Slider enemyHealthBar;
    [SerializeField] private GameObject bulletShoot;
    [SerializeField] private GameObject enemyXp;
    [SerializeField] private int maxLives;
    [SerializeField] private int speed;
    [SerializeField] private int rotateSpeed;

    private GameManager gameManager;
    private EnemySpawn enemySpawner;
    private Rigidbody2D rb;
    private GameObject player;
    private int currentLives;

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
    }

    // Update is called once per frame
    void Update()
    {
        // Shoot
    }

    private void FixedUpdate()
    {
        // Movement
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Bullet
    }

    private void EnemyShoot()
    {

    }
}
