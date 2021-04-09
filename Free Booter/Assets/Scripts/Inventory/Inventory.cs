using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> inventoryItems = new List<Item>();
    private ItemDB itemDB;
    public InventoryUIController inventoryUI;
    public ToolbarUI toolbarUI;
    Dictionary<int, List<int>> quantities = new Dictionary<int, List<int>>();
    //Dictionary<Item, List<int>> inventoryWithQuantities = new Dictionary<Item, List<int>>();
    MapController mapController;
    public bool uiActive;
    public ToastController toast;
    private LevelController levelController;
    private void Awake()
    {
       
    }
    private void Start() {
        int numberOfInventories = FindObjectsOfType<Inventory>().Length;
        if(numberOfInventories > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        levelController = FindObjectOfType<LevelController>();
        itemDB = GetComponent<ItemDB>();
        toast = FindObjectOfType<ToastController>();
        mapController = FindObjectOfType<MapController>();
        GiveItem(0, 80);
        GiveItem(1, 50);
        GiveItem(2, 100);
        GiveItem(6, 1);
        GiveItem(17, 1);
        GiveItem(18, 10);
        uiActive = false;
        inventoryUI.gameObject.SetActive(false);
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.I))
        {
            uiActive = !uiActive;
            inventoryUI.gameObject.SetActive(uiActive);
            if(uiActive)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            //DecreaseQuantity(2, 100);
        }
    }
    private void DebugInventory() {
        for(int i = 0; i < inventoryItems.Count; i++) {
            Debug.Log(inventoryItems[i].itemName);
        }
    }
    public void GiveItem(int id, int quantity) {
        GiveQuantity(id, quantity);
        /*if(toolbarUI != null)
        {
            toolbarUI.UpdateToolbar();
        }*/
        Item givenItem = CheckForItem(id);
        if(givenItem.isAMap && mapController != null)
        {
            mapController.UnlockFromMap(givenItem.stats["MapIndex"]);
        }
        toast.TriggetToast("Received " + quantity + " " + givenItem.itemName);
    }
    public void GiveItem(string itemName, int quantity) {
        Item itemToAdd = itemDB.GetItem(itemName);
        inventoryItems.Add(itemToAdd);
        if(itemToAdd.forSidescroll == levelController.isSideScroll)
        {
            inventoryUI.AddNewItem(itemToAdd, GetTotalQuantity(itemToAdd.id));
            //toolbarUI.AddNewItem(itemToAdd, GetTotalQuantity(itemToAdd.id));
        }
        int id = itemDB.GetItem(itemName).id;
        GiveQuantity(id, quantity);
    }
    public void GiveQuantity(int id, int quantity) {
        int maxQuantity = itemDB.GetItem(id).maxQuantity;
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
                        if(itemDB.GetItem(id).forSidescroll == levelController.isSideScroll)
                        {
                            inventoryUI.AddExistingItem(itemDB.GetItem(id), maxQuantity);
                            //toolbarUI.UpdateExistingItem(itemDB.GetItem(id), keyValue.Value[i]);
                        }
                        GiveQuantity(id, remainingQuantity);
                        return;
                    }
                    else if (remainingQuantity + keyValue.Value[i] <= maxQuantity && !hasFinished)
                    { // ADD TO QUANTITY
                        keyValue.Value[i] += remainingQuantity;
                        if (itemDB.GetItem(id).forSidescroll == levelController.isSideScroll)
                        {
                            inventoryUI.AddExistingItem(itemDB.GetItem(id), keyValue.Value[i]);
                            //toolbarUI.UpdateExistingItem(itemDB.GetItem(id), keyValue.Value[i]);
                        }
                        hasFinished = true;
                    }
                    else if (keyValue.Value.Count - 1 == i && keyValue.Value[i] == maxQuantity)
                    { // ADD NEW QUANTITY
                        AddExistingToInventory(id, remainingQuantity);
                        if (itemDB.GetItem(id).forSidescroll == levelController.isSideScroll)
                        {
                            //toolbarUI.UpdateExistingItem(itemDB.GetItem(id), keyValue.Value[i]);
                        }
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
        if(itemToAdd.forSidescroll == levelController.isSideScroll)
        {
            inventoryUI.AddNewItem(itemToAdd, GetTotalQuantity(itemToAdd.id));
            //toolbarUI.AddNewItem(itemToAdd, GetTotalQuantity(itemToAdd.id));
        }
    }
    private void AddExistingToInventory(int id, int quantity)
    {
        Item itemToAdd = itemDB.GetItem(id);
        inventoryItems.Add(itemToAdd);
        if(itemToAdd.forSidescroll == levelController.isSideScroll)
        {
            inventoryUI.AddNewItem(itemToAdd, quantity);
        }
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
        quantities[id][quantities[id].Count - 1] -= amount; // TAKE THE AMOUNT AWAY FROM THE QUANTITY
        if (quantities[id][quantities[id].Count - 1] <= 0) // IF THERE ISN'T ANYTHING LEFT IN THE QUANTITY...
        {
            int remainingAmount = quantities[id][quantities[id].Count - 1] * -1;
            if (quantities[id].Count == 1) // AND IF THAT'S THE ONLY QUANTITY WE HAVE...
            { // REMOVE IT COMPLETELY FROM THE INVENTORY
                quantities.Remove(id);
                RemoveItem(id);
                return;
            } // OTHERWISE, DROP THAT QUANTITY AND RUN IT AGAIN
            quantities[id].RemoveAt(quantities[id].Count - 1);
            DecreaseQuantity(id, remainingAmount);
        }
        else // IF THERE IS SOMETHING LEFT IN THE QUANTITY, ADJUST THE UI AND OVE ON WITH YOUR LIFE
        {
            inventoryUI.RemoveItem(itemDB.GetItem(id), quantities[id][quantities[id].Count - 1]);
            //toolbarUI.UpdateExistingItem(itemDB.GetItem(id), GetTotalQuantity(id));
        }
        //toolbarUI.UpdateToolbar();
    }
    public void RemoveItem(int id) {
        Item itemToRemove = CheckForItem(id);
        if(itemToRemove != null) {
            inventoryItems.Remove(itemToRemove);
            inventoryUI.RemoveItem(itemToRemove, 0);
            //toolbarUI.RemoveItem(itemToRemove);
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
