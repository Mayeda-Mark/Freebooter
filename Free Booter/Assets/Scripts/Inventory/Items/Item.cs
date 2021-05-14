using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Item-Supplies", menuName = "Item-Supplies")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    //public string type;
    public string description;
    public Sprite icon;
    public int quantity, maxQuantity;
    //public Dictionary<string, int> stats = new Dictionary<string, int>();
    public bool /*equipable, isAMap,*/ forSidescroll, ableToBuy;
    public int buyQuantity, buyPrice, sellPrice;
    public string panelName = default;
    /*public Item(int id, string itemName, string type, string description, Sprite icon, int maxQuantity, Dictionary<string, int> stats, bool equipable, bool map, bool forSidescroll) {
        this.id = id;
        this.itemName = itemName;
        this.type = type;
        this.description = description;
        this.icon = icon;//Resources.Load<Sprite>("Sprites/Items/" + itemName);
        this.maxQuantity = maxQuantity;
        this.stats = stats;
        this.equipable = equipable;
        this.isAMap = map;
        this.forSidescroll = forSidescroll;
    }
    public Item(Item item) {
        this.id = item.id;
        this.itemName = item.itemName;
        this.type = item.type;
        this.description = item.description;
        this.icon = item.icon;//Resources.Load<Sprite>("Sprites/Items/" + item.itemName);
        this.maxQuantity = item.maxQuantity;
        this.stats = item.stats;
        this.equipable = item.equipable;
        this.isAMap = item.isAMap;
        this.forSidescroll = item.forSidescroll;
    }*/
    //public int GetMaxQuantity()  { return maxQuantity;  }
}
