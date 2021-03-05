using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidescrollEnemy : MonoBehaviour
{
    #region Public Variables
    public Transform leftLimit, rightLimit;
    public float attackDistance;
    public float moveSpeed;
    public float attackTimer;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange;
    public GameObject hotZone, triggerArea;
    public float deathKickHorizontal, deathKickVertical;
    #endregion
    #region Private Variables
    private Animator anim;
    private float distance;
    private bool attackMode;
    private bool inCooldown;
    private bool isAlive;
    private float intTimer;
    private Rigidbody2D myRigidBody;
    private BoxCollider2D myCollider;
    private SpriteRenderer mySprite;
    #endregion
    private void Awake()
    {
        mySprite = GetComponent<SpriteRenderer>();
        isAlive = true;
        SelectTarget();
        myRigidBody = GetComponent<Rigidbody2D>();
        intTimer = attackTimer;
        anim = GetComponent<Animator>();
        myCollider = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        if(isAlive)
        {
            if(!attackMode)
            {
                Move();
            }
            if(!InsideLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack"))
            {
                SelectTarget();
            }
            if(inRange)
            {
                EnemyLogic();
            }
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
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack"))
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
    public void TriggerCooling()
    {
        print("Called Cooling");
        inCooldown = true;
    }
    private bool InsideLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }
    public void SelectTarget()
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
        Flip();
    }

    public void Flip()
    {
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
    /*public void ShowDamage()
    {
        StartCoroutine(ChangeSpriteColorForDamage());
    }
    IEnumerator ChangeSpriteColorForDamage()
    {
        mySprite.color = new Color(0.7294118f, 0.2784314f, 0.2784314f);
        yield return new WaitForSeconds(0.5f);
        mySprite.color = Color.white;
    }*/
    internal void Kill()
    {
        isAlive = false;
        myRigidBody.AddForce(new Vector2(deathKickHorizontal, deathKickVertical));
        myCollider.isTrigger = true;
        mySprite.color = new Color(0.7294118f, 0.2784314f, 0.2784314f);
    }
}
