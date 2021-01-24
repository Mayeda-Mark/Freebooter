using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSidescrollController : MonoBehaviour
{
    [SerializeField] float runSpeed = 8f;
    [SerializeField] float jumpSpeed = 15f;
    [SerializeField] float climbSpeed = 8f;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    [SerializeField] BoxCollider2D myFeet, ledgeCatcher;
    //[SerializeField] BoxCollider2D frontOfBody;
    [HideInInspector] public bool isRunning, isJumping, isFalling, isBlocking, isCrouching, isAttacking, isSliding, isHanging, isClimbingLedge;

    internal void Death()
    {
        throw new NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
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
        /*CatchLedge();
        ClimbLedge();*/
    }


    private void SetAnimationBools()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        bool playerIsFalling = (myRigidBody.velocity.y) < 0;
        /*isClimbingLedge = !myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) && ledgeCatcher.IsTouchingLayers(LayerMask.GetMask("Ledge")) && Input.GetButton("Climb Ledge");
        isHanging = !myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) && ledgeCatcher.IsTouchingLayers(LayerMask.GetMask("Ledge")) && !isClimbingLedge;*/
        isAttacking = Input.GetButton("Attack") && myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        isBlocking = Input.GetButton("Block") && myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) && !isAttacking;
        isCrouching = Input.GetButton("Crouch") && myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        isRunning = playerHasHorizontalSpeed && myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) && !isAttacking;
        isSliding = Input.GetButton("Crouch") && isRunning;
        isFalling = playerIsFalling && !myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));;
    }

    private void EndFall()
    {
        myAnimator.SetBool("isFalling", isFalling);
    }

    private void Run()
    {
        if (!isAttacking && !isCrouching && !isHanging)
        {
            float controlThrow = Input.GetAxis("Horizontal");
            Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
            myRigidBody.velocity = playerVelocity;
        }
    }
    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
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
    }
    private void SetFalling()
    {
        myAnimator.SetBool("isJumping", false);
        bool playerIsFalling = (myRigidBody.velocity.y) < 0;
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")));
        myAnimator.SetBool("isFalling", playerIsFalling);
    }
    
    /*private void CatchLedge()
    {
        if(isHanging)
        {
            myRigidBody.gravityScale = 0;
            StopMovement();
            Vector2 playerVelocity = new Vector2(0, 0);
            myRigidBody.velocity = playerVelocity;
        }
        myAnimator.SetBool("isHanging", isHanging);
    }
    private void ClimbLedge()
    {
        if(isClimbingLedge)
        {
            myAnimator.SetBool("isClimbingLedge", isClimbingLedge);
        }
    }
    private void EndCLimbLedge()
    {
        myAnimator.SetBool("isClimbingLedge", false);
        myRigidBody.gravityScale = 1;
    }*/
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
