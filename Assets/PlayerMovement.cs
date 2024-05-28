using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private Vector3 moveDir;
    private Vector3 lastMoveDir;
    private float moveSpeed = 10.0f;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Transform HealthBar;
    private Transform playerTransform;
    public Weapon weapon;
    public Vector3 mousePosition;
    public AudioSource audioSource;
    private HealthSystem healthSystem;
    private HealthBar healthBar;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        weapon = GetComponentInChildren<Weapon>();
        audioSource = GetComponent<AudioSource>();
        healthSystem = new HealthSystem(100);
        healthBar = GetComponentInChildren<HealthBar>();


        if (healthBar != null)
        {
            healthBar.Setup(healthSystem);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            weapon.Fire();
            audioSource.Play();
        }
        float moveX = 0;
        float moveY = 0;
        if (Input.GetKey(KeyCode.W))
        {
            moveY = 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveY = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX = 1f;
        }
        moveDir = new Vector3(moveX, moveY).normalized;

   
        if (moveX < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveX > 0)
        {
            spriteRenderer.flipX = false;
        }

        bool isIdle = moveX == 0 && moveY == 0;
        if (isIdle)
        {
            myRigidbody.velocity = Vector3.zero;
            animator.SetBool("isMoving", false);
        }
        else
        {
            lastMoveDir = moveDir;
            myRigidbody.velocity = moveDir * moveSpeed;
            animator.SetFloat("Speed", moveDir.magnitude);
            animator.SetBool("isMoving", true);
        }
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    public void TakeDamage(int damageAmount)
    {
        healthSystem.Damage(damageAmount);
        if (healthSystem.IsDead())
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        healthSystem.Heal(healAmount);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            int damage = 10;
            TakeDamage(damage);
        }
    }
}
