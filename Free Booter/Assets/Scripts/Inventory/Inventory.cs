using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> inventoryItems = new List<Item>();
    private ItemDB itemDB;
    public UIInventory inventoryUI;
    ToolbarUI toolbarUI;
    Dictionary<int, List<int>> quantities = new Dictionary<int, List<int>>();
    //Dictionary<Item, List<int>> inventoryWithQuantities = new Dictionary<Item, List<int>>();
    MapController mapController;
    private bool uiActive;
    private ToastController toast;
    private void Awake()
    {
       
    }
    private void Start() {
        //inventoryUI = FindObjectOfType<UIInventory>();
        itemDB = FindObjectOfType<ItemDB>();
        toast = FindObjectOfType<ToastController>();
        //DontDestroyOnLoad(this);
        mapController = FindObjectOfType<MapController>();
        toolbarUI = FindObjectOfType<ToolbarUI>();
        //inventoryUI.gameObject.SetActive(false);
        GiveItem(0, 80);
        GiveItem(1, 50);
        GiveItem(2, 100);
        GiveItem(6, 1);
        int numInventories = FindObjectsOfType<Inventory>().Length;
        uiActive = false;
        inventoryUI.gameObject.SetActive(false);
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
            uiActive = !uiActive;
            inventoryUI.gameObject.SetActive(uiActive);
            //print("Bloop!");
            // inventoryUI.gameObject.SetActive(!inventoryUI.gameObject.activeSelf);
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            DecreaseQuantity(2, 100);
        }
    }
    private void DebugInventory() {
        for(int i = 0; i < inventoryItems.Count; i++) {
            Debug.Log(inventoryItems[i].itemName);
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
        inventoryItems.Add(itemToAdd);
        inventoryUI.AddNewItem(itemToAdd, GetTotalQuantity(itemToAdd.id));
        int id = itemDB.GetItem(itemName).id;
        GiveQuantity(id, quantity);
    }
    public void GiveQuantity(int id, int quantity) {
        int maxQuantity = itemDB.GetItem(id).GetMaxQuantity();
        int remainingQuantity = quantity;
        bool hasFinished = false;
        bool hasFoundKey = false;
        foreach (var keyValue in quantities)
        {
            if (id == keyValue.Key && !hasFinished)
            {
                hasFoundKey = true;
                for (int i = 0; i < keyValue.Value.Count; i++)
                {
                    if (remainingQuantity + keyValue.Value[i] > maxQuantity && keyValue.Value[i] != maxQuantity)
                    { // TOP OFF QUANTITY
                        remainingQuantity = quantity + keyValue.Value[i] - maxQuantity;
                        keyValue.Value[i] = maxQuantity;
                        inventoryUI.AddExistingItem(itemDB.GetItem(id), maxQuantity);
                        GiveQuantity(id, remainingQuantity);
                        return;
                    }
                    else if (remainingQuantity + keyValue.Value[i] <= maxQuantity && !hasFinished)
                    { // ADD TO QUANTITY
                        keyValue.Value[i] += remainingQuantity;
                        inventoryUI.AddExistingItem(itemDB.GetItem(id), keyValue.Value[i]);
                        hasFinished = true;
                    }
                    else if (keyValue.Value.Count - 1 == i && keyValue.Value[i] == maxQuantity)
                    { // ADD NEW QUANTITY
                        AddExistingToInventory(id, remainingQuantity);
                        keyValue.Value.Add(remainingQuantity);
                        hasFinished = true;
                    }
                }
            }
        }
        if (!hasFinished && !hasFoundKey) { // ADD ENTIRELY NEW ITEM
            List<int> valueList = new List<int>();
            valueList.Add(quantity);
            quantities.Add(id, valueList); 
            AddToInventory(id);
        }
    }
    private void AddToInventory(int id)
    {
        Item itemToAdd = itemDB.GetItem(id);
        inventoryItems.Add(itemToAdd);
        inventoryUI.AddNewItem(itemToAdd, GetTotalQuantity(itemToAdd.id));
    }
    private void AddExistingToInventory(int id, int quantity)
    {
        Item itemToAdd = itemDB.GetItem(id);
        inventoryItems.Add(itemToAdd);
        inventoryUI.AddNewItem(itemToAdd, quantity);
    }
    public Item CheckForItem(int id) {
        return inventoryItems.Find(item => item.id == id);
    }
    public Item ReturnByIndex(int index) {
        bool itemFound;
        List<int> keyArray = new List<int>();
        List<Item> copyList = new List<Item>();
        for(int i = 0; i < inventoryItems.Count; i++) {
            itemFound = false;
            foreach(int key in keyArray) {
                if(key == inventoryItems[i].id) {
                    itemFound = true;
                }
            }
            if(!itemFound) {
                keyArray.Add(inventoryItems[i].id);
                if(inventoryItems[i].id != 2) {
                    copyList.Add(inventoryItems[i]);
                }
            }
        }
        return copyList[index];
    }
    public void DecreaseQuantity(int id, int amount)
    {
        quantities[id][quantities[id].Count - 1] -= amount;
        if (quantities[id][quantities[id].Count - 1] <= 0)
        {
            int remainingAmount = quantities[id][quantities[id].Count - 1] * -1;
            if (quantities[id].Count == 1)
            {
                quantities.Remove(id);
                RemoveItem(id);
                return;
            }
            quantities[id].RemoveAt(quantities[id].Count - 1);
            DecreaseQuantity(id, remainingAmount);
        }
        else
        {
            inventoryUI.RemoveItem(itemDB.GetItem(id), quantities[id][quantities[id].Count - 1]);
        }
        toolbarUI.UpdateToolbar();
    }
    public void RemoveItem(int id) {
        Item itemToRemove = CheckForItem(id);
        if(itemToRemove != null) {
            inventoryItems.Remove(itemToRemove);
            inventoryUI.RemoveItem(itemToRemove, 0);
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
        foreach(Item item in inventoryItems) {
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
        return inventoryItems;
    }
}
