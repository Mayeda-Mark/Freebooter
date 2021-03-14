using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> shipItems = new List<Item>();
    private ItemDB itemDB;
    //public UIInventory shipInventoryUI;
    Tooltip tooltip;
    ToolbarUI toolbarUI;
    Dictionary<int, List<int>> quantities = new Dictionary<int, List<int>>();
    MapController mapController;
    private ToastController toast;
    private void Awake()
    {
       
    }
    private void Start() {
        itemDB = FindObjectOfType<ItemDB>();
        toast = FindObjectOfType<ToastController>();
        //DontDestroyOnLoad(this);
        mapController = FindObjectOfType<MapController>();
        toolbarUI = FindObjectOfType<ToolbarUI>();
        //shipInventoryUI.gameObject.SetActive(false);
        GiveItem(0, 80);
        GiveItem(1, 50);
        GiveItem(2, 100);
        GiveItem(6, 1);
        int numInventories = FindObjectsOfType<Inventory>().Length;
        /*if (numInventories > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }*/
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.I)) {
            //print("Bloop!");
            //shipInventoryUI.gameObject.SetActive(!shipInventoryUI.gameObject.activeSelf);
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            GiveItem(2, 100);
        }
    }
    private void DebugInventory() {
        for(int i = 0; i < shipItems.Count; i++) {
            Debug.Log(shipItems[i].itemName);
        }
    }
    public void GiveItem(int id, int quantity) {
        GiveQuantity(id, quantity);
        if(toolbarUI != null)
        {
            toolbarUI.UpdateToolbar();
        }
        Item givenItem = CheckForItem(id);
        if(givenItem.isAMap)
        {
            mapController.UnlockFromMap(givenItem.stats["MapIndex"]);
        }
        //toast.gameObject.SetActive(true);
        toast.TriggetToast("Received " + quantity + " " + givenItem.itemName);
    }
    public void GiveItem(string itemName, int quantity) {
        Item itemToAdd = itemDB.GetItem(itemName);
        shipItems.Add(itemToAdd);
        //shipInventoryUI.AddNewItem(itemToAdd);
        int id = itemDB.GetItem(itemName).id;
        GiveQuantity(id, quantity);
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
                        remainingQuantity = quantity + keyValue.Value[i] - maxQuantity;
                        keyValue.Value[i] = maxQuantity;
                        GiveQuantity(id, remainingQuantity);
                        return;
                    } else if (remainingQuantity + keyValue.Value[i] <= maxQuantity && !hasFinished) {
                        keyValue.Value[i] += remainingQuantity;
                        hasFinished = true;
                    } else if(keyValue.Value.Count - 1 == i && keyValue.Value[i] == maxQuantity){
                        //Save for later keyValue.Value.Count - 1 == i && keyValue.Value[i] == maxQuantity
                        AddToInventory(id);
                        keyValue.Value.Add(remainingQuantity);
                        hasFinished = true;
                    }
                }
            }
        }
        if(!hasFinished && !hasFoundKey) {
            List<int> valueList = new List<int>();
            valueList.Add(quantity);
            quantities.Add(id, valueList); 
            AddToInventory(id);
        }
        // foreach(var keyValue in quantities) {
        //     for(int i = 0; i < keyValue.Value.Count; i++) {
        //         Debug.Log(keyValue.Value[i]);
        //     }
        // }
    }
    private void AddToInventory(int id) {
        Item itemToAdd = itemDB.GetItem(id);
        shipItems.Add(itemToAdd);
        //shipInventoryUI.AddNewItem(itemToAdd);
    }
    public Item CheckForItem(int id) {
        return shipItems.Find(item => item.id == id);
    }
    public Item ReturnByIndex(int index) {
        bool itemFound;
        List<int> keyArray = new List<int>();
        List<Item> copyList = new List<Item>();
        for(int i = 0; i < shipItems.Count; i++) {
            itemFound = false;
            foreach(int key in keyArray) {
                if(key == shipItems[i].id) {
                    itemFound = true;
                }
            }
            if(!itemFound) {
                keyArray.Add(shipItems[i].id);
                if(shipItems[i].id != 2) {
                    copyList.Add(shipItems[i]);
                }
            }
        }
        return copyList[index];
    }
    public void RemoveItem(int id) {
        Item itemToRemove = CheckForItem(id);
        if(itemToRemove != null) {
            shipItems.Remove(itemToRemove);
            //shipInventoryUI.RemoveItem(itemToRemove);
        } else {
            Debug.Log("Item not found");
        }
    }
    public Dictionary<int, List<int>> GetQuantities() { return quantities; }
    public int GetQuantitiesByKeyIndex(int key, int index) {
        return quantities[key][index];
    }
    public List<int> GetQuantitiesByKey(int key) {
        return quantities[key];
    }
    public int GetTotalQuantity(int key) {
        int total = 0;
        foreach(int stack in quantities[key]) {
            total += stack;
        }
        return total;
    }
    public int GetTotalGold() {
        int totalGold = 0;
        Item gold = CheckForItem(2);
        if(gold != null) {
            foreach(int pile in quantities[2]) {
                totalGold += pile;
            }
        }
        return totalGold;
    }
    public int GetCountLessGold() {
        int count = 0;
        List <int> keyArray = new List<int>();
        bool itemFound;
        foreach(Item item in shipItems) {
            itemFound = false;
            foreach(int key in keyArray) {
                if(key == item.id) {
                    itemFound = true;
                }
            }
            if(!itemFound && item.id != 2) {
                keyArray.Add(item.id);
                count ++;
            }
        }
        return count;
    }
    public void DecreaseQuantity(int id, int amount) {
        quantities[id][quantities[id].Count - 1] -= amount;
        if(quantities[id][quantities[id].Count - 1] <=0) {
            int remaiingAmount = quantities[id][quantities[id].Count - 1] * -1;
            if(quantities[id].Count == 1) {
                quantities.Remove(id);
                RemoveItem(id);
                return;
            }
            quantities[id].RemoveAt(quantities[id].Count - 1);
            DecreaseQuantity(id, remaiingAmount);
        }
        toolbarUI.UpdateToolbar();
    }
    public void AddGold(int amount) {
        GiveQuantity(2, amount);
    }
    public void RemoveGold(int amount) {
        DecreaseQuantity(2, amount);
    }
    public void FireCannonBall() {
        DecreaseQuantity(0, 1);
    }
    public bool CanFireCannon() {
        return quantities[0] != null;
    }
    public List<Item> GetInventory()
    {
        return shipItems;
    }
}
