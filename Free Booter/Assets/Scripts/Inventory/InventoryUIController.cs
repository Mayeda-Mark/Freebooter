﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIController : MonoBehaviour
{
    [System.Serializable]
    public class SelectedItem
    {
        public Item item;
        public int quantity;
    }
    /*[System.Serializable]
    public class Panel
    {
        public string panelName;
        public UIInventory ui;
    }
    public List<Panel> panels = new List<Panel>();*/
    public GameObject inventoryPanel, questPanel;
    public UIInventory inventoryUI;
    private Inventory inventory;
    private bool active = false;
    public SelectedItem selectedItem;
    public QuestUIController questUI;
    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        /*if (FindObjectOfType<LevelController>().isSideScroll && FindObjectOfType<LevelController>() != null)
        {
            ActivatePanel("Equipment");
        }
        else
        {
            ActivatePanel("Supplies");
        }*/
        //DeactivateUI();
    }
    void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.I))
        {
            if(active)
            {
                DeactivateUI();
            }
            else
            {
                ActivateUI();
            }
        }*/
    }
    private void DeactivateUI()
    {
        /*foreach (Panel panel in panels)
        {
            panel.ui.gameObject.SetActive(false);
        }
        foreach (Button button in GetComponentsInChildren<Button>())
        {
            button.gameObject.SetActive(false);
        }*/
    }
    private void ActivateUI()
    {
        if (FindObjectOfType<LevelController>().isSideScroll)
        {
            ActivatePanel("Equipment");
        }
        else
        {
            ActivatePanel("Supplies");
        }
        foreach (Button button in GetComponentsInChildren<Button>())
        {
            button.gameObject.SetActive(true);
        }
    }
    public void ActivatePanel(string panelName)
    {
        /*foreach(Panel panel in panels)
        {
            if(panel.panelName == panelName)
            {
                panel.ui.gameObject.SetActive(true);
                RefreshPanel(panelName);
            } 
            else
            {
                panel.ui.gameObject.SetActive(false);
            }
        }*/
        if(panelName == "Quests")
        {
            questPanel.SetActive(true);
            inventoryPanel.SetActive(false);
            questUI.ActivateUI();
        }
        else
        {
            inventoryPanel.SetActive(true);
            questPanel.SetActive(false);
            RefreshPanel(panelName);
        }
    }
    public void AddNewItem(Item item, int quantity)
    {
        /*foreach(Panel panel in panels)
        {
            if(panel.panelName == item.type)
            {
                panel.ui.AddNewItem(item, quantity);
            }
        }*/
    }
    public void AddExistingItem(Item item, int quantity)
    {
        /*foreach (Panel panel in panels)
        {
            if (panel.panelName == item.type)
            {
                panel.ui.AddExistingItem(item, quantity);
            }
        }*/
    }
    public void RemoveItem(Item item, int quantity)
    {
        /*foreach (Panel panel in panels)
        {
            if (panel.panelName == item.type)
            {
                panel.ui.RemoveItem(item, quantity);
            }
        }*/
    }
    public void RefreshPanel(string panelName)
    {
        inventoryUI.ResetMenu();
        foreach(Item item in inventory.GetInventory())
        {
            if(item.panelName == panelName) // This might cause an error with different classes
            {
                foreach(int quantity in inventory.GetQuantitiesByKey(item.id))
                {
                    inventoryUI.AddNewItem(item, quantity);
                }
            }
        }
        /*foreach(Panel panel in panels)
        {
            if(panel.panelName == panelName)
            {
                panel.ui.ResetMenu();
                foreach(Item item in inventory.GetInventory())
                {
                    if(item.type == panel.panelName)
                    {
                        foreach(int quantity in inventory.GetQuantitiesByKey(item.id))
                        {
                            panel.ui.AddNewItem(item, quantity);
                        }
                    }
                }
            }
        }*/
    }
    public void UpdateSelectedItem(Item item, int quantity)
    {
        selectedItem.item = item;
        selectedItem.quantity = quantity;
    }
}
