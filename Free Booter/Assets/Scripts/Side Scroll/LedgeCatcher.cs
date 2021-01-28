using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeCatcher : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;

    private void Start()
    {
        playerRigidBody = GetComponentInParent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ledge"))
        {
            if(playerRigidBody.transform.position.x > transform.position.x)
            {
                Vector2 newPlayerPosition = 
                playerRigidBody.transform.position
            }
        }
    }
}
