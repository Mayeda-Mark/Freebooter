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
    Dictionary<int, List<int>> quantities = new Dictionary<int, List<int>>();
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
        int maxQuantity = itemDB.GetItem(id).GetMaxQuantity();
        int remainingQuantity = quantity;
        bool hasFinished = false;
        foreach(var keyValue in quantities) {
            if(id == keyValue.Key && !hasFinished) { //If you already have this in the inventory
            //Check to see if the added quantity will be too much
                for(int i = 0; i < keyValue.Value.Count; i++) {
                    if(remainingQuantity + keyValue.Value[i] > maxQuantity) {
                        keyValue.Value[i] = maxQuantity;
                        remainingQuantity = remainingQuantity + keyValue.Value[i] - maxQuantity;
                        GiveQuantity(id, remainingQuantity);
                    } else {
                        keyValue.Value[i] += remainingQuantity;
                        hasFinished = true;
                    }
                }
            }
        }

        /*******************************/
        //THE PROBLEM COULD BE THAT IT IS TRYING TO WRITE OUTSIDE OF THE LIST BECAUSE WE AREN'T EXITING THE LOOP TO ADD TO THE LIST

        // Dictionary<string, int> quantityToAdd = new Dictionary<string, int>{
        //         {id.ToString(), quantity}
        //     };
        if(!hasFinished) {
            // Dictionary<int, List<int>> quantityToAdd = new Dictionary<int, List<int>>{
            //     {id, new List<int> { quantity }}
            // };
            List<int> valueList = new List<int>();
            valueList.Add(quantity);
            quantities.Add(id, valueList); 
        }
        // bool hasFoundItem = false;
        // foreach(Dictionary<string, int> keyValue in quantities) {
        //     string key = keyValue.Keys.ToString();
        //     string value = keyValue.Values.ToString();
        //     int intValue = Convert.ToInt32(value);
        //     int maxQuantity = itemDB.GetItem(id).GetMaxQuantity();
        //     if(id.ToString() == key && !hasFoundItem) { 
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
            foreach(var keyValues in quantities) {
                foreach(int value in keyValues.Value) {
                    Debug.Log(value);
                }
            }
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
