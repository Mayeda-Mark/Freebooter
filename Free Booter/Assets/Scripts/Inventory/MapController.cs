using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    //MapDB mapDB;
    ItemDB db;
    public GameObject[] unlockables;
    private void Awake()
    {
        //mapDB = GetComponent<MapDB>();
        db = GetComponent<ItemDB>();
    }
    public void UnlockFromMap(Map map)
    {
        //Map map = mapDB.GetMap(mapId);
        GameObject target = unlockables[map.unlockableIndex];
        LockedArea lockedArea = target.GetComponent<LockedArea>();
        if(/*map.isTreasureMap*/lockedArea == null)
        {
            target.gameObject.SetActive(true);
        } else
        {
            lockedArea.UnlockArea();
        }
    }
    public void UnlockAllMapsInInventory()
    {
        List<Item> inventory = FindObjectOfType<Inventory>().GetInventory();
        foreach(Item item in inventory)
        {
            if(item.GetType() == typeof(Map))
            {
                UnlockFromMap((Map)item);
            }
        }
    }
}
