using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    private SidescrollEnemy enemyParent;
    private void Awake()
    {
        enemyParent = GetComponentInParent<SidescrollEnemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            if(enemyParent != null)
            {
                enemyParent.target = collision.transform;
                enemyParent.inRange = true;
                enemyParent.hotZone.SetActive(true);
            }
        }
    }
}
