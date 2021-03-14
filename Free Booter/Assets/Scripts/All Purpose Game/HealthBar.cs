using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    Vector3 localScale;
    Vector3 parentScale;
    [SerializeField] Health health;
    int startingHealth;
    bool hasBeenDamaged = false;
    float maxHealth;
    void Start()
    {
        maxHealth = health.maxHealth;
        parentScale = transform.parent.transform.localScale;
        localScale = transform.localScale;
        startingHealth = health.maxHealth;
        parentScale.y = 0;
        transform.parent.transform.localScale = parentScale;
    }
    void Update()
    {
        CheckForDamage();
        if(hasBeenDamaged) {
            UpdateBar();
        }
    }
    public void UpdateBar() {
        float healthScale = (float)health.GetHealth();
        localScale.x = healthScale / maxHealth;
        transform.localScale = localScale;
    }
    public void CheckForDamage() {
        if(startingHealth > health.GetHealth()) {
            parentScale.y = 15;
            transform.parent.transform.localScale = parentScale;
            hasBeenDamaged = true;
        } if(health.GetHealth() <= 0) {
            parentScale.y = 0;
            transform.parent.transform.localScale = parentScale;   
        }
    }
}
