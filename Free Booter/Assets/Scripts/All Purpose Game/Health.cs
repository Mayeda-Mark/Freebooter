using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    [SerializeField] bool forSails = false;
    [SerializeField] bool forSideScroll = false;
    [SerializeField] int health;
    public SpriteRenderer sprite;
    void Start() {
        ResetHealth();
    }
    public void DealDamage(int damage) {
        health -= damage;
        if (health > 0)
        {
            ShowDamage();
        }
        if (health <= 0) {
            if(GetComponent<OverWorldNPC>() && !forSails) {
                GetComponent<OverworldNPCController>().Death();
            }
            else if(GetComponent<PlayerShipController>() && !forSails){
                GetComponent<PlayerShipController>().Kill();
            }
            else if (GetComponent<PlayerSidescrollController>())
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
    public void ResetHealth() {
        health = maxHealth;
    }
    public bool isHealthFull() { return health == maxHealth; }
    public void ShowDamage()
    {
        if(forSideScroll)
        {
            StartCoroutine(ChangeSpriteColorForDamage());
        }
    }
    IEnumerator ChangeSpriteColorForDamage()
    {
        sprite.color = new Color(0.7294118f, 0.2784314f, 0.2784314f);
        yield return new WaitForSeconds(0.5f);
        sprite.color = Color.white;
    }
}
