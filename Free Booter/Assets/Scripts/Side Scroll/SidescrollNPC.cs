using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidescrollNPC : MonoBehaviour
{
    public float moveSpeed = 3f;
    public bool isStationary, isShop = default;
    public Transform leftLimit, rightLimit = default;
    [HideInInspector] public Transform target;
    private Collider2D myCollider;
    public int[] dialogueIds;
    private float intervalTimer;
    private bool canWalk;
    private Animator anim;
    //private float startingTimer = 1.5f;
    void Start()
    {
        RollInterval();
        myCollider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        SelectTarget();
    }

    // Update is called once per frame
    void Update()
    {
        RunTimer();
        Move();
        Interact();
    }
    #region Timer
    private void RunTimer()
    {
        intervalTimer -= Time.deltaTime;
        if(intervalTimer <= 0)
        {
            canWalk = !canWalk;
            RollInterval();
        }
    }
    private void RollInterval()
    {
        intervalTimer = Random.Range(0.5f, 2.5f);
    }
    #endregion
    #region movement
    void Move()
    {
        anim.SetBool("Walking", canWalk);
        if(canWalk)
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            CheckForEnd();
        }
    }
    private void CheckForEnd()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if (distanceToLeft <= 0)
        {
            target = rightLimit;
        }
        else if(distanceToRight <= 0)
        {
            target = leftLimit;
        }
        Flip();

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
    #endregion
    #region Interact
    private void Interact()
    {
        if(myCollider.IsTouchingLayers(LayerMask.GetMask("Player")) && Input.GetButton("Action"))
        {
            FindObjectOfType<DialogueController>().ActivateDialogue((int)dialogueIds[Random.Range(0, dialogueIds.Length - 1)]);
        }
    }
    #endregion
}
