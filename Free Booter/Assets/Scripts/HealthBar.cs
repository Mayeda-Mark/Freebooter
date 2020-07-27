using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    Vector3 localScale;
    Vector3 parentScale;
    Health health;
    int startingHealth;
    bool hasBeenDamaged = false;
    // Start is called before the first frame update
    void Start()
    {
        parentScale = transform.parent.transform.localScale;
        localScale = transform.localScale;
        health = GetComponentInParent(typeof(Health)) as Health;
        startingHealth = health.GetHealth();
        parentScale.y = 0;
        transform.parent.transform.localScale = parentScale;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForDamage();
        if(hasBeenDamaged) {
            UpdateBar();
        }
    }
    public void UpdateBar() {
        float healthScale = (float)health.GetHealth();
        localScale.x = healthScale / 100f;
        transform.localScale = localScale;
    }
    public void CheckForDamage() {
        if(startingHealth > health.GetHealth()) {
            parentScale.y = 1;
            transform.parent.transform.localScale = parentScale;
            hasBeenDamaged = true;
        } if(health.GetHealth() <= 0) {
            parentScale.y = 0;
            transform.parent.transform.localScale = parentScale;   
        }
    }
}
