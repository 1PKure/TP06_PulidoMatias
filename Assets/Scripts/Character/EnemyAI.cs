using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float chaseSpeed = 4f;
    [SerializeField] private float detectionRange = 7f;
    [SerializeField] private float meleeAttackRange = 1.5f;
    [SerializeField] private float rangedAttackRange = 5f;
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 targetPoint;
    [SerializeField] private bool movingToB = true;
    [SerializeField] private bool isChasing = false;
    [SerializeField] private Animator animator;
    [SerializeField] private bool isAttacking = false;
    [SerializeField] private GameObject projectilePrefab; 
    [SerializeField] private Transform firePoint; 
    [SerializeField] private float projectileSpeed = 5f;
    private Rigidbody2D rb;
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        targetPoint = pointB.position; 
    }

    void Update()
    {
        if (isAttacking) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);


        if (distanceToPlayer <= detectionRange)
        {
            FlipTowards(player.position);
            isChasing = true;
            targetPoint = player.position;
        }
        else
        {
            isChasing = false;
            targetPoint = movingToB ? pointB.position : pointA.position;
        }

        if (isChasing)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, chaseSpeed * Time.deltaTime);
            if (distanceToPlayer <= meleeAttackRange)
            {
                StartCoroutine(MeleeAttack());
            }
            else if (distanceToPlayer <= rangedAttackRange)
            {
                StartCoroutine(RangedAttack());
            }
            else
            {

                ChasePlayer();
            }
        }
        else
        {
            Patrol();
        }
    }

    void FlipTowards(Vector3 targetPosition)
    {
        float direction = targetPosition.x - transform.position.x;
        if ((direction > 0 && !facingRight) || (direction < 0 && facingRight))
        {
            facingRight = !facingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    void ChasePlayer()
    {
        FlipTowards(player.position);
        transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
        animator.SetFloat("speed", chaseSpeed);
    }

    void Patrol()
    {
        FlipTowards(targetPoint);
        animator.SetFloat("speed", 1);
        Vector3 direction = targetPoint - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, patrolSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint) < 0.2f)
        {
            movingToB = !movingToB;
            targetPoint = movingToB ? pointB.position : pointA.position;
        }
    }

    IEnumerator MeleeAttack()
    {

        isAttacking = true;
        animator.SetTrigger("attack");
        yield return new WaitForSeconds(1f);
        if (Vector3.Distance(transform.position, player.position) <= meleeAttackRange)
        {
            FlipTowards(player.position);
            player.GetComponent<PlayerController>().TakeDamage(10);
        }
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("attack");
        isAttacking = false;
    }

    IEnumerator RangedAttack()
    {
        isAttacking = true;
        animator.SetTrigger("throw"); 
        yield return new WaitForSeconds(0.5f);
        if (Vector3.Distance(transform.position, player.position) <= rangedAttackRange)
        {
            FlipTowards(player.position);
            Vector2 direction = (player.position - firePoint.position).normalized;
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * projectileSpeed;
            }
        }
        yield return new WaitForSeconds(0.7f);
        animator.SetTrigger("throw");
        isAttacking = false;
    }
}

