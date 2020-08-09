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
        bool hasFoundKey = false;
        foreach(var keyValue in quantities) {
            if(id == keyValue.Key && !hasFinished) { 
            hasFoundKey = true;
                for(int i = 0; i < keyValue.Value.Count; i++) {
                    if(remainingQuantity + keyValue.Value[i] > maxQuantity && keyValue.Value[i] != maxQuantity) {
                        Debug.Log("Topped off");
                        keyValue.Value[i] = maxQuantity;
                        remainingQuantity = remainingQuantity + keyValue.Value[i] - maxQuantity;
            foreach(var keyValues in quantities) {
                foreach(int value in keyValues.Value) {
                    Debug.Log(value);
                }
            }
                        GiveQuantity(id, remainingQuantity);
                    } else if (keyValue.Value.Count - 1 == i && keyValue.Value[i] == maxQuantity) {
                        Debug.Log("The other one");
                        AddToInventory(id);
                        keyValue.Value.Add(remainingQuantity);
                        hasFinished = true;
            foreach(var keyValues in quantities) {
                foreach(int value in keyValues.Value) {
                    Debug.Log(value);
                }
            }
                        return;
                    } else {
                        Debug.Log("added quantity");
                        keyValue.Value[i] += remainingQuantity;
                        hasFinished = true;
            foreach(var keyValues in quantities) {
                foreach(int value in keyValues.Value) {
                    Debug.Log(value);
                }
            }
                    }
                }
            }
        }
        if(!hasFinished && !hasFoundKey) {
            Debug.Log("Created new Entry");
            List<int> valueList = new List<int>();
            valueList.Add(quantity);
            quantities.Add(id, valueList); 
            AddToInventory(id);
            foreach(var keyValues in quantities) {
                foreach(int value in keyValues.Value) {
                    Debug.Log(value);
                }
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
}
