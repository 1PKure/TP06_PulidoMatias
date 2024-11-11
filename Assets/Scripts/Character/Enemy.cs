using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float chaseSpeed = 4f;
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float meleeAttackRange = 1.5f;
    [SerializeField] private float rangedAttackRange = 3f;
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 targetPoint;
    [SerializeField] private bool movingToB = true;
    [SerializeField] private bool isChasing = false;
    [SerializeField] private Animator animator;
    [SerializeField] private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        targetPoint = pointB.position; 
    }

    void Update()
    {
        if (isAttacking) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
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
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, patrolSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
        {
            movingToB = !movingToB;
            targetPoint = movingToB ? pointB.position : pointA.position;
        }
    }

    IEnumerator MeleeAttack()
    {
        isAttacking = true;
        animator.SetBool("attack", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("attack", false);
        isAttacking = false;
    }

    IEnumerator RangedAttack()
    {
        isAttacking = true;
        animator.SetBool("throw", true); 
        yield return new WaitForSeconds(1f); 
        animator.SetBool("throw", false); 
        isAttacking = false;
    }
}

