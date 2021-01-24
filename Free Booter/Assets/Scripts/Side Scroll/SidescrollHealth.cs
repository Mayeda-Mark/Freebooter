using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidescrollHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;
    void Start()
    {
        ResetHealth();
    }
    public void DealDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            if(GetComponent<PlayerSidescrollController>())
            {
                GetComponent<PlayerSidescrollController>().Death();
            }
            else if (GetComponent<SidescrollEnemy>())
            {
                GetComponent<SidescrollEnemy>().Kill();
            }
        }
    }
    public int GetHealth() { return health; }
    public void ResetHealth()
    {
        health = maxHealth;
    }
    public bool isHealthFull() { return health == maxHealth; }
}
