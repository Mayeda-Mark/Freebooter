﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSidescrollController : MonoBehaviour
{
    [SerializeField] float runSpeed = 8f;
    [SerializeField] float jumpSpeed = 15f;
    [SerializeField] float climbSpeed = 8f;
    [SerializeField] float knockBackTimer = 0.25f;
    [SerializeField] float xClimb = 0.25f;
    [SerializeField] float yClimb = 0.25f;
    float startingKnockBackTimer;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    [SerializeField] BoxCollider2D myFeet/*, ledgeCatcher*/;
    private bool MovingForClimb = false;
    //[SerializeField] BoxCollider2D frontOfBody;
    [HideInInspector] public bool isRunning, isJumping, isFalling, isBlocking, isCrouching, isAttacking, isSliding, isHanging, isClimbingLedge, knockBack, canMove, canCatchLedge, canClimb;
    private LedgeCatcher ledgeCatcher;
    //private SpriteRenderer mySprite;

    internal void Death()
    {
        throw new NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        ledgeCatcher = GetComponentInChildren<LedgeCatcher>();
        canCatchLedge = true;
        //mySprite = GetComponent<SpriteRenderer>();
        startingKnockBackTimer = knockBackTimer;
        knockBack = false;
        ResetKnockBackTimer();
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimationBools();
        Attack();
        Crouch();
        Run();
        FlipSprite();
        Jump();
        EndFall();
        Block();
        KnockBackTimer();
        ClimbLedge();
        CatchLedge();
    }


    private void SetAnimationBools()
    {
        isRunning = false;
        isFalling = false;
        isBlocking = false;
        isCrouching = false;
        isAttacking = false;
        isSliding = false;
        isHanging = false;
        isClimbingLedge = false;
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        bool playerIsFalling = (myRigidBody.velocity.y) < 0;
        //isClimbingLedge = (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) && ledgeCatcher.IsTouchingLayers(LayerMask.GetMask("Ledge")) && Input.GetButton("Climb Ledge")) || myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Player_Climb_Ledge");
        //isHanging = (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) && canCatchLedge &&  ledgeCatcher.IsTouchingLayers(LayerMask.GetMask("Ledge"))) || myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Player_Hang_Idle")/*&& !isClimbingLedge*/;
        //isAttacking = Input.GetButton("Attack") && myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        //isBlocking = Input.GetButton("Block") && myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) && !isAttacking;
        //isCrouching = Input.GetButton("Crouch") && myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        //isRunning = playerHasHorizontalSpeed && myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) && !isAttacking;
        //isSliding = Input.GetButton("Crouch") && isRunning;
        //isFalling = playerIsFalling && !myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));;
        //print(isHanging);
        if(myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
            if(Input.GetButton("Attack")) { isAttacking = true; }
            else if(Input.GetButton("Block")) { isBlocking = true; }
            else if(Input.GetButton("Crouch")) { isCrouching = true; }
            if(playerHasHorizontalSpeed && !isAttacking)
            {
                if(Input.GetButton("Crouch")) { isSliding = true; }
                else { isRunning = true; }
            }
        } else if(!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            if(playerIsFalling) { isFalling = true; }
            else if(ledgeCatcher.touchingLedge/*ledgeCatcher.IsTouchingLayers(LayerMask.GetMask("Ledge"))*/ || myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Player_Hang_Idle"))
            {
                if(Input.GetButton("Climb Ledge")) { isClimbingLedge = true; }
                else { isHanging = true; }
            }
        }
        canMove = !isAttacking && !isBlocking && !knockBack && !isCrouching && !isHanging && !myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack");
        canClimb = myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Player_Hang_Idle");
    }

    private void EndFall()
    {
        myAnimator.SetBool("isFalling", isFalling);
    }

    private void Run()
    {
        if (canMove)
        {
            float controlThrow = Input.GetAxis("Horizontal");
            Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
            myRigidBody.velocity = playerVelocity;
        }
    }
    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed && !knockBack)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
        myAnimator.SetBool("isRunning", isRunning);
    }
    private void Jump()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocity;
            myAnimator.SetBool("isJumping", true);
        }
        else if (!myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Player_Jump"))
        {
            myAnimator.SetBool("isJumping", false);
        }
    }
    private void SetFalling()
    {
        myAnimator.SetBool("isJumping", false);
        bool playerIsFalling = (myRigidBody.velocity.y) < 0;
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")));
        myAnimator.SetBool("isFalling", playerIsFalling);
    }

    private void CatchLedge()
    {
        print("isHanging: " + isHanging);
        if (isHanging)
        {
            myRigidBody.gravityScale = 0;
            StopMovement();
            Vector2 playerVelocity = new Vector2(0, 0);
            myRigidBody.velocity = playerVelocity;
        } /*else if(!isHanging)
        {
            myRigidBody.gravityScale = 1;
        }*/
        myAnimator.SetBool("isHanging", isHanging);
        print(myAnimator.GetBool("isHanging"));
    }
    private void ClimbLedge()
    {
        if (canClimb && Input.GetButton("Climb Ledge"))
        {
            isHanging = false;
            print("CLimbing!");
            canCatchLedge = false;
            myAnimator.SetBool("isHanging", false);
            myAnimator.SetBool("isClimbingLedge", true);
            StartCoroutine(MovePlayerUpLedge(transform.position));
        }
    }
    public IEnumerator MovePlayerUpLedge(Vector2 position)
    {
        MovingForClimb = true;
        float currentTime = 0;
        float startX = transform.position.x;
        float startY = transform.position.y;
        while(currentTime < 0.4f && MovingForClimb)
        {
            float xSpot;
            float ySpot = position.y + yClimb;
            if(transform.localScale.x > 0) { xSpot = position.x - xClimb; }
            else { xSpot = position.x + xClimb; }
            currentTime += Time.deltaTime;
            float currentX = Mathf.Lerp(startX, xSpot, currentTime / 2f);
            float currentY = Mathf.Lerp(startY, ySpot, currentTime / 2f);
            transform.position = new Vector2(currentX, currentY);
            yield return null;
        }
        MovingForClimb = false;
        yield break;
    }
    private void EndCLimbLedge()
    {
        canCatchLedge = true;
        myAnimator.SetBool("isClimbingLedge", false);
        myRigidBody.gravityScale = 1;
    }
    private void Crouch()
    {
        if(isRunning)
        {
            myAnimator.SetBool("isSliding", isSliding);
        } else
        {
            myAnimator.SetBool("isCrouching", isCrouching);
        }
    }
    private void StopSlide()
    {
        Vector2 playerVelocity = new Vector2(0, 0);
        myRigidBody.velocity = playerVelocity;
        StopMovement();
        myAnimator.SetBool("isSliding", false);
    }
    private void Attack()
    {
        if(isAttacking)
        {
            Vector2 playerVelocity = new Vector2(0, 0);
            myRigidBody.velocity = playerVelocity;
            StopMovement();
        }
        myAnimator.SetBool("isAttacking", isAttacking);
    }
    private void Block()
    {
        if (isBlocking)
        {
            Vector2 playerVelocity = new Vector2(0, 0);
            myRigidBody.velocity = playerVelocity;
            StopMovement();
        }
        myAnimator.SetBool("isBlocking", isBlocking);
    }
    private void StopMovement()
    {
        Vector2 playerVelocity = new Vector2(0, 0);
        myRigidBody.velocity = playerVelocity;
    }
    public void SetKnockBack()
    {
        knockBack = true;
        ResetKnockBackTimer();
    }
    private void ResetKnockBackTimer()
    {
        knockBackTimer = startingKnockBackTimer;
    }
    private void KnockBackTimer()
    {
        if(knockBack)
        {
            knockBackTimer -= Time.deltaTime;
            if(knockBackTimer <= 0)
            {
                knockBack = false;
                ResetKnockBackTimer();
            }
        }
        
    }
    public void KnockBack(float horizontal, float vertical)
    {
        SetKnockBack();
        myRigidBody.AddForce(new Vector2(horizontal, vertical));
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
    /*
* public class Player : MonoBehaviour {
   //Config
   [SerializeField] float runSpeed = 8f;
   [SerializeField] float jumpSpeed = 15f;
   [SerializeField] float climbSpeed = 8f;
   float deathVertical = 25f;
   float deathHorizontal = -100f;
   //State
   bool isAlive = true;
   //Cached vars
   Rigidbody2D myRigidBody;
   Animator myAnimator;
   CapsuleCollider2D myBodyCollider;
   BoxCollider2D myFeet;
   GameSession gameSession;
   float playerGravity;

   void Start () {
       myFeet = GetComponent<BoxCollider2D>();
       myRigidBody = GetComponent<Rigidbody2D>();
       myAnimator = GetComponent<Animator>();
       myBodyCollider = GetComponent<CapsuleCollider2D>();
       playerGravity = myRigidBody.gravityScale;
       gameSession = FindObjectOfType<GameSession>();
   }

   void Update () {
       if(!isAlive) { return; }
       Run();
       Jump();
       Climb();
       FlipSprite();
       Die();
   }
   private void Run() {
       float controlThrow = Input.GetAxis("Horizontal");
       Vector2 playerVelocity = new Vector2( controlThrow * runSpeed, myRigidBody.velocity.y);
       myRigidBody.velocity = playerVelocity;
   }

   private void Jump() {
       if(!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
       bool playerHasVerticalSpeed  = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
       if(Input.GetButtonDown("Jump")) {
           Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
           myRigidBody.velocity += jumpVelocity;
       }
       // myAnimator.SetBool("isJumping", playerHasVerticalSpeed);
   }
   private void Climb() {
       if(!myFeet.IsTouchingLayers(LayerMask.GetMask("Climbable"))) { 
           myAnimator.SetBool("isClimbing", false);
           myRigidBody.gravityScale = playerGravity;
           return; 
       }
       myRigidBody.gravityScale = 0;
       float controlThrow = Input.GetAxis("Vertical");
       Vector2 playerVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed);
       myRigidBody.velocity = playerVelocity;
       bool playerHasVerticalSpeed  = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
       myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
   }
   private void FlipSprite() {
       bool playerHasHorizontalSpeed  = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
       if (playerHasHorizontalSpeed) {
           transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
       }
       myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
   }
   private void Die() {
       if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards"))){
           Vector2 DeathVelocity = new Vector2(deathHorizontal, deathVertical);
           myRigidBody.velocity = DeathVelocity;
           myAnimator.SetBool("hasDied", true);
           isAlive = false;
           gameSession.ProcessPlayerDeath();
       }
   }
}

*/
}
