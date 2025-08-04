using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public bool notDamaged;
    public float speed;
    public int maxLives;
    public GameObject bullet;
    public GameObject bulletShoot;

    [SerializeField] private Slider playerHealthBar;
    private GameManager gameManager;
    private Rigidbody2D rb;
    private float horizontalInput;
    private float verticalInput;
    private float delayAfterDamaged = 3;
    private float playerRotation;
    private int currentLives;

    // Start is called before the first frame update
    void Start()
    {
        notDamaged = true;
        currentLives = maxLives;

        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        playerHealthBar.maxValue = maxLives;
        playerHealthBar.value = currentLives;
        playerHealthBar.fillRect.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameRunning)
        {
            PlayerShoot();
        }

        if (currentLives <= 0)
        {
            currentLives = 0;
            speed = 0;
            rb.velocity = Vector3.zero;
            gameManager.GameOver();
            playerHealthBar.gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (gameManager.isGameRunning)
        {
            MovePlayer();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyMovement enemy = collision.gameObject.GetComponent<EnemyMovement>();

        if (collision.gameObject.CompareTag("Enemy") && notDamaged)
        {
            currentLives -= enemy.damage;
            playerHealthBar.value = currentLives;

            notDamaged = false;
            StartCoroutine(livesCountdownRoutine());
        }
    }

    IEnumerator livesCountdownRoutine()
    {
        yield return new WaitForSeconds(delayAfterDamaged);
        notDamaged = true;
    }

    // Move the player move based on WASD and arrow keys input
    void MovePlayer()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector2 moveAround = new Vector2(horizontalInput, verticalInput);

        Vector2 movePlayer = moveAround.normalized;

        rb.velocity = movePlayer * speed;

        if (movePlayer != Vector2.zero)
        {
            playerRotation = Mathf.Atan2(movePlayer.x, movePlayer.y) * Mathf.Rad2Deg * -1;
            transform.rotation = Quaternion.Euler(0, 0, playerRotation);
        }
    }

    // Make the player can shoot a bullet based on left click input
    void PlayerShoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(bullet, bulletShoot.transform.position, transform.rotation);
        }
    }
}
