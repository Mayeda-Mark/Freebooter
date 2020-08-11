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
    /* PROBLEM: WHEN A SLOT FILLS UP, IT NEEDS TO MOVE ON AND CREATE A NEW ENTRY IN THE LIST, INSTEAD IT'S OVERFLOWING
    THE LIST ENTRY*/
    public void GiveQuantity(int id, int quantity) {
        Debug.Log("Picked up " + quantity);
        int maxQuantity = itemDB.GetItem(id).GetMaxQuantity();
        int remainingQuantity = quantity;
        bool hasFinished = false;
        bool hasFoundKey = false;
        foreach(var keyValue in quantities) { //Iterate through the quantities dictionary by keys
            if(id == keyValue.Key && !hasFinished) {  // if the id matches the key...
            hasFoundKey = true; // you found the key... good job
                for(int i = 0; i < keyValue.Value.Count; i++) { 
                    // start iterating through the values associated with the key
                    if(remainingQuantity + keyValue.Value[i] > maxQuantity && keyValue.Value[i] != maxQuantity) {
                        //If the remainingQuantity and the value of the key exceed the 
                        //maximum and the value isn't already at maximum...
                        Debug.Log("Case 1");
                        remainingQuantity = quantity + keyValue.Value[i] - maxQuantity;
                        keyValue.Value[i] = maxQuantity;
                        //Debug.Log("KeyValue = " + keyValue.Value[i]);
                        GiveQuantity(id, remainingQuantity);
                    } else if (remainingQuantity + keyValue.Value[i] <= maxQuantity && !hasFinished) {
                        Debug.Log("Case 2");
                        keyValue.Value[i] += remainingQuantity;
                        hasFinished = true;
                        //return;
                        //If the total number of values is the same as the loop iteration (you're at the end of the
                        //list) and the value is already at max... 
                        
                        //return;
                    } else if(keyValue.Value.Count - 1 == i && keyValue.Value[i] == maxQuantity){
                        Debug.Log("Case 3");
                        //Save for later keyValue.Value.Count - 1 == i && keyValue.Value[i] == maxQuantity
                        AddToInventory(id);
                        keyValue.Value.Add(remainingQuantity);
                        hasFinished = true;
                        //return;
                    }
                }
            Debug.Log("I'm counting the loops");
            }
            Debug.Log("You have exited the loop");
        }
        if(!hasFinished && !hasFoundKey) {
            Debug.Log("Case 4");
            List<int> valueList = new List<int>();
            valueList.Add(quantity);
            quantities.Add(id, valueList); 
            AddToInventory(id);
        }
        foreach(var keyValue in quantities) {
            for(int i = 0; i < keyValue.Value.Count; i++) {
                Debug.Log(keyValue.Value[i]);
            }
        }
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
    public Dictionary<int, List<int>> GetQuantities() { return quantities; }
}
