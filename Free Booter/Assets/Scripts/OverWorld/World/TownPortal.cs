﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownPortal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D otherCollider) {
        var NPC = otherCollider.GetComponent</*OverWorldNPC*/OverworldNPCController>();
        var player = otherCollider.GetComponent<PlayerShipController>();
        if(NPC && otherCollider.GetType() == typeof(CapsuleCollider2D)) {
            NPC.Kill();
        }
        if(player && otherCollider.GetType() == typeof(CapsuleCollider2D) && otherCollider.isTrigger) {
            FindObjectOfType<TownController>().OpenMenu();
        }
    }
}
