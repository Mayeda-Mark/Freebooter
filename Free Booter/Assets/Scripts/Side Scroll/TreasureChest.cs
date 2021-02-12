using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    bool playerOnBox, looted = false;
    public Sprite openTreasureSprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerSidescrollController player = collision.GetComponent<PlayerSidescrollController>();
        if(player)
        {
            playerOnBox = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerSidescrollController player = collision.GetComponent<PlayerSidescrollController>();
        if (player)
        {
            playerOnBox = false;
        }
    }
    private void Update()
    {
        if(playerOnBox && Input.GetButton("Loot") && !looted)
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.sprite = openTreasureSprite;
            looted = true;
            GiveReward();
        }
    }

    private void GiveReward()
    {
        print("Here you go, you earned it!");
    }
}
