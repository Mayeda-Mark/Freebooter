using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    int health;
    void Start() {
        ResetHealth();
    }
    public void DealDamage(int damage) {
        health -= damage;
        if(health <= 0) {
            if(GetComponent<OverWorldNPC>()) {
                GetComponent<OverworldNPCController>().Death();
            } else{
                Destroy(gameObject);
            }
        }
    }
    public int GetHealth() { return health; }
    public void ResetHealth() {
        health = maxHealth;
    }
    public bool isHealthFull() { return health == maxHealth; }
}
