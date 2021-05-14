using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarUI : MonoBehaviour
{
    //public List<ToolbarItem> toolbarItems = new List<ToolbarItem>();
    public ToolbarItem[] toolbarItems;
    //public GameObject slotPrefab;
    public Transform slotPanel;
    public int numberOfSlots = 6;
    Inventory inventory;
    Dictionary<Item, int> toolbarInventory = new Dictionary<Item, int>();
    List<Item> inventoryItems = new List<Item>();
    UIInventory uiInventory;
    private void Awake()
    {
        toolbarItems = new ToolbarItem[numberOfSlots];
        for (int i = 0; i < numberOfSlots /*- 1*/; i++)
        {
            /*GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slotPanel);*/
            //toolbarItems.Add(instance.GetComponentInChildren<ToolbarItem>());
            toolbarItems[i] = slotPanel.GetChild(i).GetComponentInChildren<ToolbarItem>();
            toolbarItems[i].SetSlotIndex(i);
        }
    }

    void Start()
    {
        uiInventory = FindObjectOfType<UIInventory>();
        inventory = FindObjectOfType<Inventory>();
        inventoryItems = inventory.GetInventory();
        //EquipFirstItem();
        SetSlotNumbers();
    }
    void Update()
    {
        KeyboardListener();
    }
    public void EquipFirstItem()
    {
        //SetUpToolbar();
        bool foundFirst = false;
        for (int i = 0; i < numberOfSlots - 1; i++)
        {
            if (toolbarItems[i].item != null && toolbarItems[i].quantity > 0 && !foundFirst)
            {
                toolbarItems[i].EquipItem();
            }
        }
        /*foreach (Item item in toolbarInventory.Keys)
        {
            if (toolbarInventory[item] > 0 && !foundFirst)
            {
                toolbarItems[toolbarItems.FindIndex(i => i.item == item)].EquipItem(item);
                foundFirst = true;
            }
        }*/
    }
    private void SetSlotNumbers()
    {
        for(int i = 0; i < numberOfSlots - 1; i++)
        {
            toolbarItems[i].SetSlotNumer(i + 1);
        }
    }
    public void UpdateExistingItem(Item item, int quantity)
    {
        bool foundItem = false;
        for(int i = 0; i < numberOfSlots - 1; i ++)
        {
            if(toolbarItems[i].item == item && !foundItem)
            {
                toolbarItems[i].UpdateItem(item, quantity);
                foundItem = true;
                return;
            }
            //if(toolbarInventory == item) 
        }
        //UpdateSlot(toolbarItems.FindIndex(i => i.item == item), item);
    }
    public void AddNewItem(Item item, int quantity)
    {
        if(item.GetType() == typeof(Equipment))
        {
            bool foundEmptySlot = false;
            for (int i = 0; i < numberOfSlots - 1; i++)
            {
                if (toolbarItems[i].item == null && !foundEmptySlot)
                {
                    toolbarItems[i].UpdateItem(item, quantity);
                    foundEmptySlot = true;
                    return;
                }
            }
        }
        //UpdateSlot(toolbarItems.FindIndex(i => i.item == null), item);
    }
    public void RemoveItem(Item item)
    {
        bool foundItem = false;
        for(int i = 0; i < numberOfSlots /*- 1*/; i++)
        {
            if(toolbarItems[i].item == item && !foundItem)
            {
                toolbarItems[i].UpdateItem(null, 0);
                foundItem = true;
                return;
            }
        }
        //UpdateSlot(toolbarItems.FindIndex(i => i.item == item), null);
    }

    internal void UnequipAllButThis(int index)
    {
        for(int i = 0; i < numberOfSlots /*- 1*/; i++)
        {
            if(i != index)
            {
                toolbarItems[i].Unequip();
            }
        }
    }
    /*public void UpdateSlot(int slot, Item item)
    {
        toolbarItems[slot].UpdateItem(item);
    }*/
    private void SetUpToolbar()
    {
        foreach (Item item in inventoryItems)
        {
            bool hasFoundItem = false;
            if (item.GetType() == typeof(Equipment))
            {
                foreach (Item existingItem in toolbarInventory.Keys)
                {
                    if (item == existingItem)
                    {
                        int totalQuantity = inventory.GetTotalQuantity(item.id);
                        //toolbarInventory[existingItem] = totalQuantity;
                        hasFoundItem = true;
                        UpdateExistingItem(item, totalQuantity);
                    }
                }
                if (!hasFoundItem)
                {
                    int totalQuantity = inventory.GetTotalQuantity(item.id);
                    toolbarInventory.Add(item, totalQuantity);
                    AddNewItem(item, totalQuantity);
                }
            }

        }
    }
    /*private void UpdateQuantities()
    {
        for(int i = 0; i < toolbarItems.Count - 1; i ++)
        {
            if(toolbarItems[i].item != null)
            {
                toolbarItems[i].SetQuantity(inventory.GetTotalQuantity(toolbarItems[i].item.id));
            }
        }
    }*/
    /*public void UpdateToolbar()
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
        UpdateQuantities();
    }*/
    #region Keyboard Listener
    private void KeyboardListener()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (toolbarItems[0].item != null)
            {
                toolbarItems[0].EquipItem(/*toolbarItems[0].item*/);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (toolbarItems[1].item != null)
            {
                toolbarItems[1].EquipItem(/*toolbarItems[1].item*/);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (toolbarItems[2].item != null)
            {
                toolbarItems[2].EquipItem(/*toolbarItems[2].item*/);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (toolbarItems[3].item != null)
            {
                toolbarItems[3].EquipItem(/*toolbarItems[3].item*/);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (toolbarItems[4].item != null)
            {
                toolbarItems[4].EquipItem(/*toolbarItems[4].item*/);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (toolbarItems[5].item != null)
            {
                toolbarItems[5].EquipItem(/*toolbarItems[5].item*/);
            }
        }
    }
    #endregion
}