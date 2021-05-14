using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownItemController : MonoBehaviour, IPooledObject
{
    public int itemIndex;
    public bool reactable;
    private int damage;
    private int range;
    //public SidescrollItem ssItem;
    Rigidbody2D myRigidBody;
    public Equipment item;
    CircleCollider2D myCollider;
    public float distanceToClearPlayer = 3f;
    private float distanceTraveled;
    private Vector2 lastPosition;
    private Transform playerTransform;
    private int animationState;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void OnObjectSpawn()
    {
        animator = GetComponent<Animator>();
        ResetAnimation();
        playerTransform = FindObjectOfType<PlayerSidescrollController>().GetComponent<Transform>();
        distanceTraveled = 0;
        lastPosition = transform.position;
        reactable = false;
        myCollider = GetComponent<CircleCollider2D>();
        myCollider.isTrigger = true;
        item = (Equipment)FindObjectOfType<ItemDB>().GetItem(itemIndex);
        //ssItem = FindObjectOfType<SidescrollItemDB>().GetSidescrollItem(item.stats["SidescrollIndex"]);
        myRigidBody = GetComponent<Rigidbody2D>();
        /*myRigidBody.velocity = new Vector2(0, 0);
        myRigidBody.velocity += (myRigidBody.GetRelativeVector(Vector2.up * ssItem.stats["Range"]));*/
        if(playerTransform.localScale.x > 0)
        {
            myRigidBody.AddForce(new Vector2(item.range * 40, item.range * 40));
        }
        else
        {
            myRigidBody.AddForce(new Vector2(item.range * -40, item.range * 40));
        }
    }
    void Update()
    {
        distanceTraveled = CalculateDistanceToClearPlayer();
        lastPosition = transform.position;
        if(distanceTraveled >= distanceToClearPlayer)
        {
            myCollider.isTrigger = false;
        }
        if(reactable)
        {
            print("Reactable");
        }
    }

    private float CalculateDistanceToClearPlayer()
    {
        return Vector2.Distance(lastPosition, transform.position) + distanceTraveled;
    }
    public void AdvanceAnimation()
    {
        animationState++;
        animator.SetInteger("AnimationState", animationState);
    }
    public void ResetAnimation()
    {
        animationState = 0;
        animator.SetInteger("AnimationState", animationState);
    }
    public void Kill()
    {
        //ResetAnimation();
        reactable = false;
        this.gameObject.SetActive(false);
    }
    public void ActivateReaction()
    {
        reactable = true;
    }
}
