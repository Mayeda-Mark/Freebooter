using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInventory : MonoBehaviour
{
    public List<Item> shipItems = new List<Item>();
    public ItemDB itemDB;
    public UIInventory shipInventoryUI;
    private void Start() {
        //GiveItem("Cannon Balls");
        GiveItem(1);
        GiveItem(0);
        //RemoveItem(1);
    }
    public void GiveItem(int id) {
        Item itemToAdd = itemDB.GetItem(id);
        shipItems.Add(itemToAdd);
        shipInventoryUI.AddNewItem(itemToAdd);
        Debug.Log("Added " + itemToAdd.itemName);
    }
    public void GiveItem(string itemName) {
        Item itemToAdd = itemDB.GetItem(itemName);
        shipItems.Add(itemToAdd);
    }
    public Item CheckForItem(int id) {
        return shipItems.Find(item => item.id == id);
    }
    public void RemoveItem(int id) {
        Item itemToRemove = CheckForItem(id);
        if(itemToRemove != null) {
            shipItems.Remove(itemToRemove);
            shipInventoryUI.RemoveItem(itemToRemove);
        } else {
            Debug.Log("Item not found");
        }
    }
}
