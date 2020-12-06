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
    public void UnlockFromMap(int mapId)
    {
        Map map = mapDB.GetMap(mapId);
        GameObject target = map.unlockable;
        if(map.isTreasureMap)
        {
            target.SetActive(true);
        }
    }
}
