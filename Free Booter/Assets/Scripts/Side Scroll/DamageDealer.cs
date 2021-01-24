using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SidescrollHealth targetHealth = collision.GetComponent<SidescrollHealth>();
        PlayerSidescrollController player = collision.GetComponent<PlayerSidescrollController>();
        if(targetHealth)
        {
            if(player == null || !player.isBlocking)
            {
                targetHealth.DealDamage(damage);
            }
        }
    }
}
