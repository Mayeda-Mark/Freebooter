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
    {
        maps = new List<Map>
        {
            new Map(0, true, unlockables[0]),
        };
    }
     
}
