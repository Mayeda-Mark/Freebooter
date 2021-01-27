using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidescrollHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;
    public SpriteRenderer sprite;
    void Start()
    {
        ResetHealth();
    }
    public void DealDamage(int damage)
    {
        health -= damage;
        if(health > 0)
        {
            ShowDamage();
        } else if (health <= 0)
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
    public void ShowDamage()
    {
        StartCoroutine(ChangeSpriteColorForDamage());
    }
    IEnumerator ChangeSpriteColorForDamage()
    {
        sprite.color = new Color(0.7294118f, 0.2784314f, 0.2784314f);
        yield return new WaitForSeconds(0.5f);
        sprite.color = Color.white;
    }
}
