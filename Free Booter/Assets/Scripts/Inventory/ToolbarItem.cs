using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ToolbarItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    //I WANT THE TOOLBAR ITEMS TO BECOME GOLD TO INDICATE THAT THEY ARE EQUIPPED, BLACK WHEN EMPTY, MUTED WHEN NOT EQUIPPED, BRIGHTENED TO NORMAL WHEN MOUSED OVER AND EQUIP THE ITEM WHEN CLICKED
    public Item item;
    public int quantity;
    private Image spriteImage;
    PlayerShipController player;
    [SerializeField] Image parentImage = default;
    Color selectedItemColor = new Color();
    Color emptyItemColor = new Color();
    Color defaultColor = new Color();
    Color mouseOverColor = new Color();
    ToolbarUI toolbarUI;
    [SerializeField] Text slotNumber;
    [SerializeField] Text quantityText;
    /*public*/ InventoryUIController inventoryController;
    Inventory inventory;
    LevelController levelController;
    bool equipped;
    public bool isActive = false;
    public int slotIndex;
    private void Awake()
    {
        //parentImage = GetComponentInParent<Image>();
        ColorUtility.TryParseHtmlString("#FFFF00", out selectedItemColor);
        ColorUtility.TryParseHtmlString("#575757", out emptyItemColor);
        ColorUtility.TryParseHtmlString("#AB843A", out defaultColor);
        ColorUtility.TryParseHtmlString("#DDAA4A", out mouseOverColor);
        player = FindObjectOfType<PlayerShipController>();
        spriteImage = GetComponent<Image>();
        inventory = FindObjectOfType<Inventory>();
        UpdateItem(null, 0);
        parentImage.color = defaultColor;
        toolbarUI = FindObjectOfType<ToolbarUI>();
    }
    void Start()
    {
        //uIInventory = FindObjectOfType<UIInventory>();
        levelController = FindObjectOfType<LevelController>();
        isActive = false;
        inventoryController = FindObjectOfType<InventoryUIController>();
    }
/*
    // Update is called once per frame
    void Update()
    {

    }*/
    public void SetSlotNumer(int number)
    {
        slotNumber.text = number.ToString();
    }
    public void SetQuantity(int number)
    {
        quantityText.text = number.ToString();
    }
    public void UpdateItem(Item item, int updateQuantity)
    {
        this.item = item;
        quantity = updateQuantity;
        if (this.item != null)
        {
            SetQuantity(quantity);
            isActive = true;
            spriteImage.color = Color.white;
            spriteImage.sprite = this.item.icon;
            quantityText.color = Color.white;
            if(!equipped)
            {
                parentImage.color = defaultColor;
            }
        }
        else
        {
            isActive = false;
            spriteImage.color = Color.clear;
            quantityText.color = Color.clear;
        }
    }
    #region Equipping
    public void OnPointerClick(PointerEventData eventData)
    {
        if(inventory.uiActive)
        {
            if(this.item != null && isActive)
            {
                if(inventoryController.selectedItem.item != null)
                { // If you have an item in this slot and you have an item selected from the menu
                    if(levelController.isSideScroll == inventoryController.selectedItem.item.forSidescroll)
                    {
                        Item cloneItem = this.item;
                        int cloneQuantity = this.quantity;
                        UpdateItem(inventoryController.selectedItem.item, inventoryController.selectedItem.quantity);
                        inventoryController.UpdateSelectedItem(cloneItem, cloneQuantity);
                    }
                }
                else
                {// If you have an item in this slot, but don't have anything selected
                    if (equipped)
                    {
                        Unequip();
                    }
                    inventoryController.UpdateSelectedItem(this.item, this.quantity);
                    UpdateItem(null, 0);
                    isActive = false;
                }
            }
            else if(this.item == null && !isActive)
            {
                if(inventoryController.selectedItem.item != null)
                {
                    print("Bloop!");
                    UpdateItem(inventoryController.selectedItem.item, inventoryController.selectedItem.quantity);
                    inventoryController.UpdateSelectedItem(null, 0);
                }
                else
                {
                    print("Empty on empty");
                    print(inventoryController.selectedItem.item.itemName);
                }
            }
        }
        /*if(isActive)
        {
            EquipItem(*//*item*//*);
        }*/
    }
    public void SetSlotIndex(int number)
    {
        slotIndex = number;
    }

    public void EquipItem(/*Item item*/)
    {
        //ADD IF STATEMENT CHECKING FOR QUANTITY > 0
        parentImage.color = selectedItemColor;
        player.EquipItem(item.itemName);
        equipped = true;
        toolbarUI.UnequipAllButThis(slotIndex);
    }
    public void Unequip()
    {
        equipped = false;
        parentImage.color = default;
    }
    #endregion
    #region Enter
    public void OnPointerEnter(PointerEventData eventData1)
    {
        if (!equipped && isActive)
        {
            parentImage.color = mouseOverColor;
        }
    }
    #endregion
    #region Exit
    public void OnPointerExit(PointerEventData eventData2)
    {
        if (!equipped &&isActive)
        {
            parentImage.color = defaultColor;
        }
    }
    #endregion
}
