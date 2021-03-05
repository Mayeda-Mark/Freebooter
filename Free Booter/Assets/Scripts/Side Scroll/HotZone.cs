using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotZone : MonoBehaviour
{
    private SidescrollEnemy enemyParent;
    private RangedAndMeleeController rangedAndMeleeParent;
    private bool inRange;
    private Animator anim;
    private void Awake()
    {
        enemyParent = GetComponentInParent<SidescrollEnemy>();
        rangedAndMeleeParent = GetComponentInParent<RangedAndMeleeController>();
        anim = GetComponentInParent<Animator>();
    }
    private void Update()
    {
        if(inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack_Melee") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack_Ranged"))
        {
            if(enemyParent != null)
            {
                enemyParent.Flip();
            } else if (rangedAndMeleeParent != null)
            {
                rangedAndMeleeParent.Flip();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
            gameObject.SetActive(false);
            if(enemyParent != null)
            {
                enemyParent.triggerArea.SetActive(true);
                enemyParent.inRange = false;
                enemyParent.SelectTarget();
            } else if(rangedAndMeleeParent != null)
            {
                rangedAndMeleeParent.triggerArea.SetActive(true);
                rangedAndMeleeParent.inRange = false;
                rangedAndMeleeParent.SelectTarget();
            }
        }
    }
}
