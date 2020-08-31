using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealthBar : MonoBehaviour
{
    Vector3 localScale;
    Vector3 parentScale;
    int startingHealth;
    bool hasBeenDamaged = false;
    Health playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        parentScale = transform.parent.transform.localScale;
        localScale = transform.localScale;
        playerHealth = FindObjectOfType<PlayerShipController>().GetComponent<Health>();
        startingHealth = playerHealth.GetHealth();
        //parentScale.y = 1;
        transform.parent.transform.localScale = parentScale;
    }

    void Update()
    {
        //CheckForDamage();
        //if(hasBeenDamaged) {
            UpdateBar();
        //}
    }
    public void UpdateBar() {
        float healthScale = (float)playerHealth.GetHealth();
        localScale.x = healthScale / 100f;
        transform.localScale = localScale;
    }
    public void CheckForDamage() {
        if(startingHealth > playerHealth.GetHealth()) {
            parentScale.y = 1;
            transform.parent.transform.localScale = parentScale;
            hasBeenDamaged = true;
        } if(playerHealth.GetHealth() <= 0) {
            parentScale.y = 0;
            transform.parent.transform.localScale = parentScale;   
        }
    }
}
