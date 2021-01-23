using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidescrollEnemy : MonoBehaviour
{
    #region Public Variables
    public Transform rayCast, leftLimit, rightLimit;
    public LayerMask rayCastMask;
    public float rayCastLength;
    public float attackDistance;
    public float moveSpeed;
    public float attackTimer;
    #endregion
    #region Private Variables
    private RaycastHit2D hit;
    private Transform target;
    private Animator anim;
    private float distance;
    private bool attackMode;
    private bool inRange;
    private bool inCooldown;
    private float intTimer;
    private Rigidbody2D myRigidBody;
    #endregion
    private void Awake()
    {
        SelectTarget();
        myRigidBody = GetComponent<Rigidbody2D>();
        intTimer = attackTimer;
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if(!attackMode)
        {
            Move();
        }
        if(!InsideLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Minotaur_Attack"))
        {
            SelectTarget();
        }
        if(inRange)
        {
            hit = Physics2D.Raycast(rayCast.position, transform.right, rayCastLength, rayCastMask);
            RayCastDebugger();
        }
        if(hit.collider != null)
        {
            EnemyLogic();
        }
        else /*if (hit.collider == null)*/
        {
            print("Called!");
            inRange = false;
        }
        if(inRange == false)
        {
            StopAttack();
        }
    }

    private void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);
        if(distance > attackDistance)
        {
            StopAttack();
        } else if(attackDistance >= distance && !inCooldown)
        {
            Attack();
        }
        if(inCooldown)
        {
            Cooldown();
            anim.SetBool("isAttacking", false);
        }
    }
    void Move()
    {
        anim.SetBool("isWalking", true);
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Minotaur_Attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
    void Attack()
    {
        attackTimer = intTimer;
        attackMode = true;
        anim.SetBool("isWalking", false);
        anim.SetBool("isAttacking", true);
    }
    void Cooldown()
    {
        attackTimer -= Time.deltaTime;
        if(attackTimer <= 0 && inCooldown && attackMode)
        {
            inCooldown = false;
            attackTimer = intTimer;
        }
    }
    void StopAttack()
    {
        inCooldown = false;
        attackMode = false;
        anim.SetBool("isAttacking", false);
    }
    private void RayCastDebugger()
    {
        if(distance > attackDistance)
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.red);
        }
        else if(attackDistance > distance)
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.green);
        }
    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if(trig.gameObject.tag == "Player")
        {
            target = trig.transform;
            inRange = true;
            FLip();
        }
    }
    public void TriggerCooling()
    {
        inCooldown = true;
    }
    private bool InsideLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }
    private void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if(distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        } else
        {
            target = rightLimit;
        }
        FLip();
    }

    private void FLip()
    {
        /*bool enemyHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (enemyHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }*/

        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f;
        }
        transform.eulerAngles = rotation;
    }
}
