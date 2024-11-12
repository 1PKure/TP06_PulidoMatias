using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isGrounded;
    private Vector2 movement;
    private float currentHealth;
    private GameManager gameManager;
    [SerializeField] private PauseManager pauseManager;
    [SerializeField] private Transform healthBarScale;
    [SerializeField] private Transform floorPoint;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource attackSound;
    [SerializeField] private AudioSource loseSound;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject projectilePrefab;  
    [SerializeField] private Transform spawner;            
    [SerializeField] private Image healthBar;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject loseMessage;
    public PlayerData playerData;
    private float projectileSpeed = 10f;  
    private float fireRate = 1f;           
    private float nextFireTime = 0f;
    private bool facingRight = true;
    void Start()
    {
        loseMessage.SetActive(false);
        playerData.currentHealth = playerData.maxHealth;
        UpdateHealthBar();
        rb = GetComponent<Rigidbody2D>();
        
    }

    public void Set(PlayerData playerData)
    {
        this.playerData = playerData;
    }

    void Update()
    {
        if (loseMessage.activeSelf && Input.GetKeyDown(KeyCode.Y))
        {
            RestartGame();
        }
        else if (loseMessage.activeSelf && Input.GetKeyDown(KeyCode.N))
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        }
#endif
        float moveInput = Input.GetAxisRaw("Horizontal");
        Flip(rb.velocity.x);
        rb.velocity = new Vector2(moveInput, rb.velocity.y);

        float currentSpeed = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("speed", currentSpeed);
        if (Input.GetKeyDown(KeyCode.X) && Time.time >= nextFireTime)
        {
            Shoot();
            attackSound.Play();
            nextFireTime = Time.time + fireRate;
        }
        if (Mathf.Abs(rb.velocity.x) < playerData.maxSpeed)
        {
            rb.AddForce(new Vector2(moveInput * playerData.moveSpeed, 0), ForceMode2D.Force);
        }

        isGrounded = CheckIfGrounded();
        animator.SetBool("isGrounded", isGrounded);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isGrounded = false;
            rb.AddForce(Vector2.up * playerData.jumpForce, ForceMode2D.Impulse);
            animator.SetTrigger("Jumped");
            animator.SetBool("isGrounded", isGrounded);
            jumpSound.Play();
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, spawner.position, spawner.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(projectileSpeed * transform.localScale.x, 0);
    }

    void Flip(float velocityX)
    {
        if (velocityX > 0 && !facingRight)
        {
            facingRight = true;
            FlipPlayer();
        }
        else if (velocityX < 0 && facingRight)
        {
            // Voltear
            facingRight = false;
            FlipPlayer();
        }
    }

    void FlipPlayer()
    {
        transform.Rotate(Vector3.up * 180);
        canvas.transform.Rotate(Vector3.up * 180);
    }
    public void TakeDamage(float damage)
    {
        playerData.currentHealth -= damage;
        playerData.currentHealth = Mathf.Clamp(playerData.currentHealth, 0, playerData.maxHealth);
        UpdateHealthBar();

        if (playerData.currentHealth <= 0)
        {
            Lose();
        }
    }

    private void UpdateHealthBar()
    {
        healthBar.fillAmount = playerData.currentHealth / playerData.maxHealth;
    }
    public void AddHealth(float health)
    {
        playerData.currentHealth = Mathf.Min(playerData.currentHealth + health, playerData.maxHealth);
        UpdateHealthBar();
    }

    void Lose()
    {
        if (pauseManager != null)
        {
            pauseManager.enabled = false;
        }
        loseSound.Play();
        loseMessage.SetActive(true);
        Time.timeScale = 0f;
    }

    void RestartGame()
    {
        gameManager.ResetCoins();
        Time.timeScale = 1f;
        playerData.ResetValue();
        loseMessage.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetMovementSpeed(float newSpeed)
    {
        if (newSpeed > 1)
        {
            return;
        }
        playerData.moveSpeed = newSpeed * Time.deltaTime * 1000;
    }

    public float GetMovementSpeed()
    {
        return playerData.moveSpeed;
    }

    private bool CheckIfGrounded()
    {
        float rayLength = 0.1f;
        Vector2 position = floorPoint.position;
        Vector2 direction = Vector2.down;
        Debug.DrawRay(position, direction * rayLength, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, rayLength, LayerMask.GetMask("Ground"));
        return hit.collider != null;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}