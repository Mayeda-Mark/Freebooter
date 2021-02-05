using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeCatcher : MonoBehaviour
{
    public float xCorrection, yCorrection;
    private Rigidbody2D playerRigidBody;
    public bool touchingLedge;

    private void Start()
    {
        playerRigidBody = GetComponentInParent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ledge"))
        {
            touchingLedge = true;
            if (playerRigidBody.transform.position.x > collision.transform.position.x)
            {
                Vector2 newPlayerPosition = new Vector2(transform.position.x - xCorrection, transform.position.y - yCorrection);
                playerRigidBody.transform.position = newPlayerPosition;
            }
            else if (playerRigidBody.transform.position.x < collision.transform.position.x)
            {
                Vector2 newPlayerPosition = new Vector2(transform.position.x + xCorrection, transform.position.y - yCorrection);
                playerRigidBody.transform.position = newPlayerPosition;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ledge"))
        {
            touchingLedge = false;
        }
    }
}
