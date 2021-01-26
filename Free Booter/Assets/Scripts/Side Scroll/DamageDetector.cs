using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDetector : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public float knockBackH, knockBackV;
    public PlayerSidescrollController player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer dealer = collision.GetComponent<DamageDealer>();
        if(dealer != null)
        {
            /*Vector2 knockBackVelocity = new Vector2(knockBackH, knockBackV);
            myRigidBody.velocity = knockBackVelocity;*/
            player.SetKnockBack();
            myRigidBody.AddForce(new Vector2(knockBackH, knockBackV));
        }
    }
}
