using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownPortal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D otherCollider) {
        var NPC = otherCollider.GetComponent<OverWorldNPC>();
        if(NPC) {
            NPC.Kill();
        }
    }
}
