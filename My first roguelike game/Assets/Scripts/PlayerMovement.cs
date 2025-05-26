using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public int lives;
    public GameObject bullet;
    public Transform bulletShoot;

    private Rigidbody2D rb;
    private float horizontalInput;
    private float verticalInput;
    private float delayAfterDamaged = 3;

    // Start is called before the first frame update
    void Start()
    {  
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        PlayerShoot();

        if (lives <= 0)
        {
            lives = 0;
            Debug.Log("Game Over!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            lives--;
            StartCoroutine(livesCountdownRoutine());
        }
    }

    IEnumerator livesCountdownRoutine()
    {
        yield return new WaitForSeconds(delayAfterDamaged);
    }

    // Move the player move based on WASD and arrow keys input
    void MovePlayer()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector2.right * speed * Time.deltaTime * horizontalInput);
        transform.Translate(Vector2.up * speed * Time.deltaTime * verticalInput);
    }

    // Make the player can shoot a bullet based on left click input
    void PlayerShoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(bullet, bulletShoot.position, transform.rotation);
        }
    }
}
