using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInventory : MonoBehaviour
{
    private void Start() {
        GiveItem("Cannon Balls");
        RemoveItem(1);
    }
    public List<Item> shipItems = new List<Item>();
    public ItemDB itemDB;
    public void GiveItem(int id) {
        Item itemToAdd = itemDB.GetItem(id);
        shipItems.Add(itemToAdd);
    }
    public void GiveItem(string itemName) {
        Item itemToAdd = itemDB.GetItem(itemName);
        shipItems.Add(itemToAdd);
    }
    public Item CheckForItem(int id) {
        return shipItems.Find(item => item.id == id);
    }
    public void RemoveItem(int id) {
        Item item = CheckForItem(id);
        if(item != null) {
            shipItems.Remove(item);
            Debug.Log("Removed " + item.itemName);
        } else {
            Debug.Log("Item not found");
        }
    }
}
