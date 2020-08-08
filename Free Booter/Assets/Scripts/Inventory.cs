using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> shipItems = new List<Item>();
    public ItemDB itemDB;
    public UIInventory shipInventoryUI;
    Tooltip tooltip;
    //Dictionary<int, int> inventoryQuantity = new Dictionary<int, int>();
    List<Dictionary<string, int>> quantities = new List<Dictionary<string, int>>();
    private void Start() {
        GiveItem(0, 80);
        shipInventoryUI.gameObject.SetActive(false);
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.I)) {
            shipInventoryUI.gameObject.SetActive(!shipInventoryUI.gameObject.activeSelf);
        }
    }
    private void DebugInventory() {
        for(int i = 0; i < shipItems.Count; i++) {
            Debug.Log(shipItems[i].itemName);
        }
    }
    public void GiveItem(int id, int quantity) {
        GiveQuantity(id, quantity);
    }
    public void GiveItem(string itemName, int quantity) {
        Item itemToAdd = itemDB.GetItem(itemName);
        shipItems.Add(itemToAdd);
        shipInventoryUI.AddNewItem(itemToAdd);
        //GiveQuantity(id, quantity);
    }
    public void GiveQuantity(int id, int quantity) {
        // bool hasFoundItem = false;
        // foreach(Dictionary<string, int> keyValue in quantities) {
        //     string key = keyValue.Keys.ToString();
        //     string value = keyValue.Values.ToString();
        //     int intValue = Convert.ToInt32(value);
        //     int maxQuantity = itemDB.GetItem(id).GetMaxQuantity();
        //     if(id.ToString() == key && !hasFoundItem) { //If you already have this in the inventory
        //         //Check to see if the added quantity will be too much
        //         if(intValue + quantity > maxQuantity) { 
        //             //If it is, Top it off and call the function again with the remainder
        //             keyValue[key] = maxQuantity;
        //             GiveQuantity(id, intValue + quantity - maxQuantity);
        //         }else {//If it isn't, add the quantity to what is already there
        //             keyValue[key] += quantity;
        //             hasFoundItem = true;
        //         }
        //     } 
        // }
        // if(!hasFoundItem) { //If you make it through the loop without finding an item, add the item's quantity
        // Dictionary<string, int> quantityToAdd = new Dictionary<string, int>{
        //         {id.ToString(), quantity}
        //     };
        //     quantities.Add(quantityToAdd);
            AddToInventory(id);
        //     foreach(Dictionary<string, int> keyValues in quantities) {
        //         string key = keyValues.Keys.ToString();
        //         Debug.Log(keyValues.Values.ToString());
        //     }
        // }
    }
    private void AddToInventory(int id) {
        Item itemToAdd = itemDB.GetItem(id);
        shipItems.Add(itemToAdd);
        shipInventoryUI.AddNewItem(itemToAdd);
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
