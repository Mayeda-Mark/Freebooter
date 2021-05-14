using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDB : MonoBehaviour
{
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
    [SerializeField] public List<Map> maps = new List<Map>();
    [SerializeField] GameObject[] unlockables;
    private void Awake()
    {
        BuildDb();
    }
    public Map GetMap(int id)
    {
        return maps.Find(map => map.id == id);
    }
    private void BuildDb()
    {/*
        maps = new List<Map>
        {
            new Map(0, true, unlockables[0]),
            new Map(1, true, unlockables[1]),
            new Map(2, true, unlockables[2]),
            new Map(3, true, unlockables[3]),
            new Map(4, true, unlockables[4]),
            new Map(5, true, unlockables[5]),
            new Map(6, true, unlockables[6]),
            new Map(7, true, unlockables[7]),
            new Map(8, false, unlockables[8]),
            new Map(9, false, unlockables[9]),
        };*/
    }
     
}
