using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidescrollEnemy : MonoBehaviour
{
    #region Public Variables
    public string projectile;
    public Transform leftLimit, rightLimit, shooter;
    public float meleeAttackDistance;
    public float rangedAttackDistance;
    public float moveSpeed;
    public float attackTimer;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange;
    public GameObject hotZone, triggerArea;
    public float deathKickHorizontal, deathKickVertical;
    public bool hasDeathAnimation, ranged, melee;
    public string meleeSound, rangedSound, deathSound;
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
    private Pooler pooler;
    private SoundManager soundManager;
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
    private void Start()
    {
        pooler = FindObjectOfType<Pooler>();
        soundManager = FindObjectOfType<SoundManager>();
    }
    void Update()
    {
        if (isAlive)
        {
            if (!attackMode)
            {
                Move();
            }
            if (!InsideLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack_Melee") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack_Ranged"))
            {
                SelectTarget();
            }
            if (inRange)
            {
                EnemyLogic();
            }
        }
    }

    private void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);
        if (distance > rangedAttackDistance && distance > meleeAttackDistance)
        {
            StopAttack();
        }
        else if ((rangedAttackDistance >= distance || meleeAttackDistance >= distance) && !inCooldown)
        {
            Attack();
        }
        if (inCooldown)
        {
            Cooldown();
            anim.SetBool("isAttackingMelee", false);
            anim.SetBool("isAttackingRanged", false);
        }
    }
    void Move()
    {
        anim.SetBool("isWalking", true);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack_Melee") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack_Ranged"))
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
        if (melee && ranged)
        {
            print(1);
            if (meleeAttackDistance >= distance && !inCooldown)
            {
                print(2);
                anim.SetBool("isAttackingMelee", true);
                anim.SetBool("isAttackingRanged", false);
            }
            else
            {
                print(3);
                anim.SetBool("isAttackingMelee", false);
                anim.SetBool("isAttackingRanged", true);
            }
        }
        else if (!melee && ranged)
        {
            print(4);
            anim.SetBool("isAttackingMelee", false);
            anim.SetBool("isAttackingRanged", true);
        }
        else if (melee && !ranged)
        {
            print(5);
            anim.SetBool("isAttackingMelee", true);
            anim.SetBool("isAttackingRanged", false);
        }

    }


    void Cooldown()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0 && inCooldown && attackMode)
        {
            inCooldown = false;
            attackTimer = intTimer;
        }
    }
    void StopAttack()
    {
        inCooldown = false;
        attackMode = false;
        anim.SetBool("isAttackingMelee", false);
        anim.SetBool("isAttackingRanged", false);
    }
    public void TriggerCooling()
    {
        inCooldown = true;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack_Melee"))
        {
            soundManager.PlaySound(meleeSound);
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack_Ranged"))
        {
            soundManager.PlaySound(rangedSound);
        }
    }
    private bool InsideLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }
    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
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
    public void FireProjectile()
    {
        Vector2 dir = target.position - transform.position;
        float shootAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        pooler.SpawnFromPool(projectile, shooter.position, Quaternion.Euler(0, 0, shootAngle + 270));
    }
    internal void Kill()
    {
        StopAttack();
        isAlive = false;
        soundManager.PlaySound(deathSound);
        if (!hasDeathAnimation)
        {
            myRigidBody.AddForce(new Vector2(deathKickHorizontal, deathKickVertical));
            mySprite.color = new Color(0.7294118f, 0.2784314f, 0.2784314f);
        }
        else
        {
            anim.SetBool("isDead", true);
            GetComponent<Rigidbody2D>().gravityScale = 0;
        }
        myCollider.isTrigger = true;
        hotZone.SetActive(false);
        triggerArea.SetActive(false);
    }
}