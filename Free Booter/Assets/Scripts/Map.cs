using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    public int id;
    public bool isTreasureMap;
    public GameObject unlockable;

    public Map(int id, bool isTreasureMap, GameObject unlockable)
    {
        this.id = id;
        this.isTreasureMap = isTreasureMap;
        this.unlockable = unlockable;
    }
    public Map(Map map)
    {
        this.id = map.id;
        this.isTreasureMap = map.isTreasureMap;
        this.unlockable = map.unlockable;
    }
}
