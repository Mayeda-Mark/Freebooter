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
        ThrownItemController thrownItem = collision.GetComponent<ThrownItemController>();
        if (thrownItem != null)
        {
            if (thrownItem.item.effector == effector && thrownItem.reactable)
            {
                parent.GetComponent<ReactableObject>().React();
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        ThrownItemController thrownItem = collision.GetComponent<ThrownItemController>();
        if (thrownItem != null)
        {
            if (thrownItem.item.effector == effector && thrownItem.reactable)
            {
                parent.GetComponent<ReactableObject>().React();
            }
        }
    }
}
