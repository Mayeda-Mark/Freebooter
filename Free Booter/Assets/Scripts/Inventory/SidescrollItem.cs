using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidescrollItem
{
    public int id;
    public string type;
    public string itemName;
    //public int range;
    public string effector;
    Dictionary<string, int> stats = new Dictionary<string, int>();

    public SidescrollItem(int id, string itemName, string type, /*int range,*/ string effector, Dictionary<string, int> stats)
    {
        this.id = id;
        this.itemName = itemName;
        this.type = type;
        //this.range = range;
        this.effector = effector;
        this.stats = stats;
    }
    public SidescrollItem(SidescrollItem sidescrollItem)
   {
        this.id = sidescrollItem.id;
        this.itemName = sidescrollItem.itemName;
        this.type = sidescrollItem.type;
        //this.range = sidescrollItem.range;
        this.effector = sidescrollItem.effector;
        this.stats = sidescrollItem.stats;
    }
}

