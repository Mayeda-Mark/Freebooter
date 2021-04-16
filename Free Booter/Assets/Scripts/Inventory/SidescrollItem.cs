using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidescrollItem
{
    public int id;
    public string type;
    public string itemName;
    public string effector;
    public float cooldown;
    public Dictionary<string, int> stats = new Dictionary<string, int>();

    public SidescrollItem(int id, string itemName, string type, string effector, float cooldown, Dictionary<string, int> stats)
    {
        this.id = id;
        this.itemName = itemName;
        this.type = type;
        this.effector = effector;
        this.cooldown = cooldown;
        this.stats = stats;
    }
    public SidescrollItem(SidescrollItem sidescrollItem)
   {
        this.id = sidescrollItem.id;
        this.itemName = sidescrollItem.itemName;
        this.type = sidescrollItem.type;
        this.effector = sidescrollItem.effector;
        this.cooldown = sidescrollItem.cooldown;
        this.stats = sidescrollItem.stats;
    }
}

