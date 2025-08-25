using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public bool notDamaged;
    public float speed;
    public int maxLives;
    public int playerLevel;
    public bool upgradeSelection;
    public GameObject bullet;
    public GameObject bulletShoot;

    [SerializeField] private Slider playerHealthBar;
    [SerializeField] private Slider playerXpBar;
    [SerializeField] private TextMeshProUGUI playerLevelText;
    private GameManager gameManager;
    private Rigidbody2D rb;
    private float horizontalInput;
    private float verticalInput;
    private float delayAfterDamaged = 3;
    private float playerRotation;
    private int currentLives;
    private int maxXp;
    private int currentXp;
    private int xpExpanding = 20;
    private int livesIncreased = 5;
    private int maxLivesIncreased = 2;

    // Start is called before the first frame update
    void Start()
    {
        notDamaged = true;
        currentLives = maxLives;
        maxXp = 50;
        currentXp = 0;
        playerLevel = 1;
        upgradeSelection = false;

        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        playerHealthBar.maxValue = maxLives;
        playerHealthBar.value = currentLives;
        playerHealthBar.fillRect.gameObject.SetActive(true);

        playerXpBar.maxValue = maxXp;
        playerXpBar.value = currentXp;
        playerXpBar.fillRect.gameObject.SetActive(true);

        playerLevelText.text = $"Level: {playerLevel}";
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
            rb.velocity = Vector3.zero;
            gameManager.GameOver();
            playerHealthBar.gameObject.SetActive(false);
            playerXpBar.gameObject.SetActive(false);
            playerLevelText.gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (gameManager.isGameRunning)
        {
            MovePlayer();
        }
        else
        {
            rb.velocity = Vector3.zero;
            playerHealthBar.gameObject.SetActive(false);
            playerXpBar.gameObject.SetActive(false);
            playerLevelText.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyMovement enemy = collision.gameObject.GetComponent<EnemyMovement>();
        BossController boss = collision.gameObject.GetComponent<BossController>();

        if (collision.gameObject.CompareTag("Enemy") && notDamaged)
        {
            currentLives -= enemy.damage;
            playerHealthBar.value = currentLives;

            notDamaged = false;
            StartCoroutine(livesCountdownRoutine());
        }

        if (collision.gameObject.CompareTag("Boss") && notDamaged)
        {
            currentLives -= boss.damage;
            playerHealthBar.value = currentLives;

            notDamaged = false;
            StartCoroutine(livesCountdownRoutine());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        XpController xp = collision.gameObject.GetComponent<XpController>();

        if (collision.gameObject.CompareTag("XP"))
        {
            UpdateXp(xp.enemyXp);
            Destroy(collision.gameObject);
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

    private void UpdateXp(int xpToAdd)
    {
        currentXp += xpToAdd;
        playerXpBar.value = currentXp;

        if (currentXp >= maxXp)
        {
            currentXp = 0;
            playerXpBar.value = currentXp;
            maxXp += xpExpanding;
            playerXpBar.maxValue = maxXp;

            playerLevel++;
            playerLevelText.text = $"Level: {playerLevel}";
            upgradeSelection = true;
        }
    }

    public void HealthButton()
    {
        if (currentLives != maxLives)
        {
            for (int i = 0; i < livesIncreased; i++)
            {
                currentLives++;
                playerHealthBar.value = currentLives;

                if (currentLives == maxLives)
                {
                    break;
                }
            }
        }
    }

    public void MaxHealthButton()
    {
        maxLives += maxLivesIncreased;
        playerHealthBar.maxValue = maxLives;
    }

    public void FastButton()
    {
        speed++;
    }
}
