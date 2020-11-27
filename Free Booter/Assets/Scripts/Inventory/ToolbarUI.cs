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

    void Start()
    {
        EquipFirstItem();
        SetSlotNumbers();
    }
    public void EquipFirstItem()
    {
        UpdateToolbar();
        bool foundFirst = false;
        foreach (Item item in toolbarInventory.Keys)
        {
            if (toolbarInventory[item] > 0 && !foundFirst)
            {
                toolbarItems[toolbarItems.FindIndex(i => i.item == item)].EquipItem(item);
                foundFirst = true;
            }
        }
    }

    private void SetSlotNumbers()
    {
        for(int i = 0; i < toolbarItems.Count - 1; i++)
        {
            toolbarItems[i].SetSlotNumer(i + 1);
        }
    }
    private void UpdateQuantities()
    {
        for(int i = 0; i < toolbarItems.Count - 1; i ++)
        {
            if(toolbarItems[i].item != null)
            {
                toolbarItems[i].SetQuantity(inventory.GetTotalQuantity(toolbarItems[i].item.id));
            }
        }
    }
    void Update()
    {
        KeyboardListener();
    }

    private void KeyboardListener()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(toolbarItems[0].item != null)
            {
                toolbarItems[0].EquipItem(toolbarItems[0].item);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (toolbarItems[1].item != null)
            {
                toolbarItems[1].EquipItem(toolbarItems[1].item);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (toolbarItems[2].item != null)
            {
                toolbarItems[2].EquipItem(toolbarItems[2].item);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (toolbarItems[3].item != null)
            {
                toolbarItems[3].EquipItem(toolbarItems[3].item);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (toolbarItems[4].item != null)
            {
                toolbarItems[4].EquipItem(toolbarItems[4].item);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (toolbarItems[5].item != null)
            {
                toolbarItems[5].EquipItem(toolbarItems[5].item);
            }
        }
    }

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
        UpdateQuantities();
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
