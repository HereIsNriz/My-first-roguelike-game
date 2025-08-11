using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    public bool bossDead;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Slider bossHealthBar;
    [SerializeField] private GameObject player;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private int damage;
    [SerializeField] private int maxLives; 
    private int currentLives;

    // Start is called before the first frame update
    void Start()
    {
        currentLives = maxLives;

        bossDead = false;

        bossHealthBar.maxValue = maxLives;
        bossHealthBar.value = currentLives;
        bossHealthBar.fillRect.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLives <= 0)
        {
            Destroy(gameObject);
            bossDead = true;
            gameManager.enemyDeathCount++;
        }

        if (!gameManager.isGameRunning)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (Vector2)player.transform.position - (Vector2)rb.transform.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * rotateSpeed;

        rb.velocity = transform.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BulletController bullet = collision.gameObject.GetComponent<BulletController>();

        currentLives -= bullet.damage;
        bossHealthBar.value = currentLives;
    }
}
