﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidescrollItemDB : MonoBehaviour
{
    public List<SidescrollItem> sidescrollItems = new List<SidescrollItem>();
    /*[SerializeField] public List<Item> items = new List<Item>();
    [SerializeField] Sprite[] icons;
    private void Awake()
    {
        BuildDb();
    }
    public Item GetItem(int id)
    {
        return items.Find(item => item.id == id);
    }
    public Item GetItem(string itemName)
    {
        return items.Find(item => item.itemName == itemName);
    }*/
    private void Awake()
    {
        BuildDB();
    }
    public SidescrollItem GetSidescrollItem(int id)
    {
        return sidescrollItems.Find(sidescrollItem => sidescrollItem.id == id);
    }
    public SidescrollItem GetSidescrollItem(string itemName)
    {
        return sidescrollItems.Find(sidescrollItem => sidescrollItem.itemName == itemName);
    }
    private void BuildDB()
    {
        sidescrollItems = new List<SidescrollItem>
        {
            new SidescrollItem(0, "Sword", "Melee", "none", 
            new Dictionary<string, int> {
                {"Damage", 10 }
            })
        };
    }
}
