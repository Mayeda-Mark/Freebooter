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
        Health targetHealth = collision.GetComponent<Health>();
        PlayerSidescrollController player = collision.GetComponent<PlayerSidescrollController>();
        SidescrollEnemy enemy = collision.GetComponent<SidescrollEnemy>();
        if(targetHealth)
        {
            if(player == null || !player.isBlocking)
            {
                /*if(player)
                {
                    player.ShowDamage();
                }
                if(enemy)
                {
                    enemy.ShowDamage();
                }*/
                targetHealth.DealDamage(damage);
                if(knockBack)
                {
                    if(player)
                    {
                        if (transform.position.x > collision.transform.position.x)
                        {
                            player.KnockBack(horizontalKnockback * -1, verticalKnockBack);
                        }
                        else if (transform.position.x <= collision.transform.position.x)
                        {
                            player.KnockBack(horizontalKnockback, verticalKnockBack);
                        }
                    }
                }
            }
        }
    }
}
