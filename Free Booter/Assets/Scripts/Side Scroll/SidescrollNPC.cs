using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidescrollNPC : MonoBehaviour
{
    #region Public Variables
    public Transform rayCast;
    public LayerMask rayCastMask;
    public float rayCastLength;
    public float attackDistance;
    public float moveSpeed;
    public float attackTimer;
    #endregion
    #region Private Variables
    private RaycastHit2D hit;
    private GameObject target;
    private Animator anim;
    private float distance;
    private bool attackMode;
    private bool inRange;
    private bool inCooldown;
    private float intTimer;
    #endregion
    private void Awake()
    {
        intTimer = attackTimer;
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if(inRange)
        {
            hit = Physics2D.Raycast(rayCast.position, Vector2.left, rayCastLength, rayCastMask);
            RayCastDebugger();
        }else if(!inRange)        {
            anim.SetBool("isWalking", false);
            StopAttack();
        }
        if(hit.collider != null)
        {
            EnemyLogic();
        } else if (hit.collider == null)
        {
            inRange = false;
        }
    }

    private void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);
        if(distance > attackDistance)
        {
            Move();
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
            Vector2 targetPosition = new Vector2(target.transform.position.x, transform.position.y);
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
            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.red);
        }
        else if(attackDistance > distance)
        {
            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.green);
        }
    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if(trig.gameObject.tag == "Player")
        {
            target = trig.gameObject;
            inRange = true;
        }
    }
    public void TriggerCooling()
    {
        inCooldown = true;
    }
}
