using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 100;
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
}
