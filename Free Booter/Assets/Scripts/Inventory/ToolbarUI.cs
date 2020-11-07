using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarUI : MonoBehaviour
{
    public List<ToolbarItem> toolbarItems = new List<ToolbarItem>();
    public GameObject slotPrefab;
    public Transform slotPanel;
    public int numberOfSlots = 6;
    Inventory inventory;
    Dictionary<Item, int> toolbarInventory = new Dictionary<Item, int>();
    List<Item> inventoryItems = new List<Item>();
    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        inventoryItems = inventory.GetInventory();
        for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slotPanel);
            toolbarItems.Add(instance.GetComponentInChildren<ToolbarItem>());
        }
    }
    public void EquipFirstItem()
    {
        UpdateToolbar();
        bool foundFirst = false;
        foreach(Item item in toolbarInventory.Keys)
        {
            if(toolbarInventory[item] > 0 && !foundFirst)
            {
                toolbarItems[toolbarItems.FindIndex(i => i.item == item)].EquipItem(item);
                foundFirst = true;
            }
        }
    }

    void Start()
    {
        EquipFirstItem();
    }
/*
// Update is called once per frame
void Update()
{

}*/
    public void UpdateToolbar()
    {
        foreach (Item item in inventoryItems)
        {
            bool hasFoundItem = false;
            if (item.equipable)
            {
                foreach(Item existingItem in toolbarInventory.Keys)
                {
                    if(item == existingItem)
                    {
                        int totalQuantity = inventory.GetTotalQuantity(item.id);
                        //toolbarInventory[existingItem] = totalQuantity;
                        hasFoundItem = true;
                        UpdateExistingItem(item);
                    }
                }
                if(!hasFoundItem)
                {
                    int totalQuantity = inventory.GetTotalQuantity(item.id);
                    toolbarInventory.Add(item, totalQuantity);
                    AddNewItem(item);
                }
            }
        }
    }
    public void UpdateExistingItem(Item item)
    {
        UpdateSlot(toolbarItems.FindIndex(i => i.item == item), item);
    }
    public void AddNewItem(Item item)
    {
        UpdateSlot(toolbarItems.FindIndex(i => i.item == null), item);
    }
    public void RemoveItem(Item item)
    {
        UpdateSlot(toolbarItems.FindIndex(i => i.item == item), null);
    }

    internal void UnequipAllButThis(Item item)
    {
        foreach(ToolbarItem slot in toolbarItems)
        {
            if(slot.item != item)
            {
                slot.Unequip();
            }
        }
    }

    public void UpdateSlot(int slot, Item item)
    {
        toolbarItems[slot].UpdateItem(item);
    }
}
