using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item-Map", menuName = "Item-Map")]
public class Map : Item
{
    //public bool isTreasureMap;
    public int unlockableIndex;
    //[HideInInspector]public new string panelName = "Maps";

    /*    public Map(int id, bool isTreasureMap, GameObject unlockable)
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
        }*/
}
