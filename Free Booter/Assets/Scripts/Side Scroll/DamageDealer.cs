using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damage;
    public bool knockBack = false;
    public float horizontalKnockback;
    public float verticalKnockBack;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SidescrollHealth targetHealth = collision.GetComponent<SidescrollHealth>();
        PlayerSidescrollController player = collision.GetComponent<PlayerSidescrollController>();
        if(targetHealth)
        {
            if(player == null || !player.isBlocking)
            {
                targetHealth.DealDamage(damage);
                if(knockBack)
                {
                    if(transform.position.x > collision.transform.position.x)
                    {
                        player.KnockBack(horizontalKnockback * -1, verticalKnockBack);
                    } else if(transform.position.x <= collision.transform.position.x)
                    {
                        player.KnockBack(horizontalKnockback, verticalKnockBack);
                    }
                }
            }
        }
    }
}
