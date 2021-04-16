using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactableObject : MonoBehaviour
{
    public string effector = default;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Eventually set up projectile items here
        ThrownItem thrownItem = collision.GetComponent<ThrownItem>();
        if(thrownItem != null)
        {
            if(thrownItem.ssItem.effector == effector)
            {
                //Eventually set up animations and whatnot here
                GetComponentInParent<GameObject>().SetActive(false);
            }
        }
    }
}
