using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIController : MonoBehaviour
{
    [System.Serializable]
    public class Panel
    {
        public string panelName;
        public UIInventory ui;
    }
    public List<Panel> panels = new List<Panel>();
    private Inventory inventory;
    private bool active = false;
    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        if (FindObjectOfType<LevelController>().isSideScroll)
        {
            ActivatePanel("Equipment");
        }
        else
        {
            ActivatePanel("Supplies");
        }
        DeactivateUI();
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
        foreach(Panel panel in panels)
        {
            panel.ui.gameObject.SetActive(false);
        }
        foreach(Button button in GetComponentsInChildren<Button>())
        {
            button.gameObject.SetActive(false);
        }
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
        foreach(Panel panel in panels)
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
        }
    }
    public void AddNewItem(Item item, int quantity)
    {
        foreach(Panel panel in panels)
        {
            if(panel.panelName == item.type)
            {
                panel.ui.AddNewItem(item, quantity);
            }
        }
    }
    public void AddExistingItem(Item item, int quantity)
    {
        foreach (Panel panel in panels)
        {
            if (panel.panelName == item.type)
            {
                panel.ui.AddExistingItem(item, quantity);
            }
        }
    }
    public void RemoveItem(Item item, int quantity)
    {
        foreach (Panel panel in panels)
        {
            if (panel.panelName == item.type)
            {
                panel.ui.RemoveItem(item, quantity);
            }
        }
    }
    public void RefreshPanel(string panelName)
    {
        foreach(Panel panel in panels)
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
        }
    }
}
