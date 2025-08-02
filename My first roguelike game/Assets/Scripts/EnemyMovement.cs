using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public float rotateSpeed;
    public int damage;
    public int lives;

    private GameManager gameManager;
    private Rigidbody2D rb;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (lives <= 0)
        {
            Destroy(gameObject);
        }

        if (!gameManager.isGameRunning)
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
            lives -= bullet.damage;
        }
    }
}
