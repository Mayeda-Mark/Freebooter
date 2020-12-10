using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item{
    public int id;
    public string itemName;
    public string description;
    public Sprite icon;
    public int maxQuantity;
    public Dictionary<string, int> stats = new Dictionary<string, int>();
    public bool equipable, isAMap, isUnique;
    public Item(int id, string itemName, string description, Sprite icon, int maxQuantity, Dictionary<string, int> stats, bool equipable, bool map, bool isUnique) {
        this.id = id;
        this.itemName = itemName;
        this.description = description;
        this.icon = icon;//Resources.Load<Sprite>("Sprites/Items/" + itemName);
        this.maxQuantity = maxQuantity;
        this.stats = stats;
        this.equipable = equipable;
        this.isAMap = map;
        this.isUnique = isUnique;
    }
    public Item(Item item) {
        this.id = item.id;
        this.itemName = item.itemName;
        this.description = item.description;
        this.icon = item.icon;//Resources.Load<Sprite>("Sprites/Items/" + item.itemName);
        this.maxQuantity = item.maxQuantity;
        this.stats = item.stats;
        this.equipable = item.equipable;
        this.isAMap = item.isAMap;
        this.isUnique = item.isUnique;
    }
    public int GetMaxQuantity()  { return maxQuantity;  }
}
