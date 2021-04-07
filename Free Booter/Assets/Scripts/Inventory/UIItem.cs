using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    public Image spriteImage;
    public Text quantityText;
    Inventory inventory;
    UIInventory uIInventory;
    public string itemName;
    public bool itemQuantityFull;
    int itemQuantity;
    InventoryUIController inventoryController;
    private void Awake()
    {
        /*spriteImage = GetComponentInChildren<Image>();
        quantityText = GetComponentInChildren<Text>();*/
        //UpdateItem(null);
    }
    private void Start() {
        uIInventory = GetComponentInParent<UIInventory>();
        //quantityText = transform.parent.GetComponentInChildren<Text>();
        //spriteImage = GetComponent<Image>();
        itemQuantityFull = false;
        inventory = FindObjectOfType<Inventory>();
        inventoryController = FindObjectOfType<InventoryUIController>();
        UpdateItem(null, 0);
    }
    /*public void UpdateItem(Item item) {
        this.item = item;
        if(this.item != null) {
            //itemName = item.itemName;
            spriteImage.color = Color.white;
            spriteImage.sprite = this.item.icon;
            int quantityValue = inventory.GetQuantities()[item.id][0];
            quantityText.color = Color.white;
            quantityText.text = quantityValue.ToString();
        } else {
            spriteImage.color = Color.clear;
            quantityText.color = Color.clear;
        }
    }*/
    public void UpdateItem(Item item, int /*index*/ quantity) {
        this.item = item;
        itemQuantity = quantity;
        if(this.item != null) {
            spriteImage.color = Color.white;
            spriteImage.sprite = this.item.icon;
            int quantityValue = quantity;//inventory.GetQuantitiesByKeyIndex(this.item.id, /*index*/);
            quantityText.color = Color.white;
            quantityText.text = quantityValue.ToString();
            if(quantity >= item.maxQuantity)
            {
                itemQuantityFull = true;
            }
        } else {
            spriteImage.color = Color.clear;
            quantityText.color = Color.clear;
        }
    }
    public void OnPointerClick(PointerEventData eventData) {
        /*int clickCount = eventData.clickCount;
        if(clickCount == 2) {
            DoubleClick();
        }
        if(clickCount == 1) {
            SingleClick();
        }*/
        if (this.item != null)
        { // If this square is not empty...
            if (inventoryController.selectedItem.item != null)
            { // And you already have an item selected...
                //Put the selected item into the square
                //Item clone = new Item(uIInventory.selectedItem);
                //selectedItem.UpdateItem(this.item);
                Item cloneItem = this.item;
                int cloneQuantity = this.itemQuantity;
                UpdateItem(inventoryController.selectedItem.item, inventoryController.selectedItem.quantity);
                inventoryController.UpdateSelectedItem(cloneItem, cloneQuantity);
            }
            else
            { //You don't have an item selected...
                inventoryController.UpdateSelectedItem(this.item, this.itemQuantity);
                UpdateItem(null, 0);
            }
        }
        else if (this.item == null/*uIInventory.selectedItem.item != null*/)
        { //If this square is empty...
            if(inventoryController.selectedItem.item != null)
            { // ... And you have an item selected...
                // Move the selected item into the square
                UpdateItem(inventoryController.selectedItem.item, inventoryController.selectedItem.quantity);
                inventoryController.UpdateSelectedItem(null, 0);
            }
            else
            {
                print("empty to empty");
            }
        }
    }
    /*
    private void SingleClick() {
        if(this.item != null) { // If this square is empty...
            if(selectedItem.item != null) { // And you already have an item selected...
                Item clone = new Item(selectedItem.item);
                selectedItem.UpdateItem(this.item);
                UpdateItem(clone);
            } else { //You don't have an item selected...
                Debug.Log("Equipped" + this.item.itemName);
            }
        } else if(selectedItem.item != null) { //If you already have an item selected...
            UpdateItem(selectedItem.item);
            selectedItem.UpdateItem(null);
        }
    }
    private void DoubleClick() {
        if(this.item != null) {
            if(selectedItem.item != null) {
                Item clone = new Item(selectedItem.item);
                selectedItem.UpdateItem(this.item);
                UpdateItem(clone);
            } else {
                selectedItem.UpdateItem(this.item);
                UpdateItem(null);
            }
        } else if(selectedItem.item != null) {
            UpdateItem(selectedItem.item);
            selectedItem.UpdateItem(null);
        }
    }*/
    public void OnPointerEnter(PointerEventData eventData1) {
        if(this.item != null) {
        }
    }
    public void OnPointerExit(PointerEventData eventData2) {
    }
    public void UpdateThisItem(int index) {
        UpdateItem(this.item, index);
    }
    public int GetSlotQuantity() { return inventory.GetQuantities()[item.id][0]; }
    public int GetSlotQuantity(int index) { return inventory.GetQuantities()[item.id][index]; }
    public int GetItemId() { return this.item.id; }
    public bool HasItem() {
        if(this.item != null) {
            return true;
        } else { return false; }
    }
}
