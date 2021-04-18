using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionTriger : MonoBehaviour
{
    public string effector = default;
    public GameObject parent = default;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Eventually set up projectile items here
        ThrownItem thrownItem = collision.GetComponent<ThrownItem>();
        if (thrownItem != null)
        {
            if (thrownItem.ssItem.effector == effector && thrownItem.reactable)
            {
                parent.GetComponent<ReactableObject>().React();
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        ThrownItem thrownItem = collision.GetComponent<ThrownItem>();
        if (thrownItem != null)
        {
            if (thrownItem.ssItem.effector == effector && thrownItem.reactable)
            {
                parent.GetComponent<ReactableObject>().React();
            }
        }
    }
}
