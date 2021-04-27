using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    MapDB mapDB;
    private void Awake()
    {
        mapDB = GetComponent<MapDB>();
    }
    void Start()
    {
    }

    //[System.Obsolete]
    public void UnlockFromMap(int mapId)
    {
        print(mapId);
        Map map = mapDB.GetMap(mapId);
        GameObject target = map.unlockable;
        if(map.isTreasureMap)
        {
            target.gameObject.SetActive(true);
        } else
        {
            target.GetComponent<LockedArea>().UnlockArea();
        }
    }
    public void UnlockAllMapsInInventory()
    {
        List<Item> inventory = FindObjectOfType<Inventory>().GetInventory();
        foreach(Item item in inventory)
        {
            if(item.isAMap)
            {
                UnlockFromMap(item.stats["MapIndex"]);
            }
        }
    }
}
