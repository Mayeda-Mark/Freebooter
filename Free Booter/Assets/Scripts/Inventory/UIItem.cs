using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    private Image spriteImage;
    Text quantityText;
    private UIItem selectedItem;
    private Tooltip tooltip;
    Inventory inventory;

    private void Awake() {
        quantityText = transform.parent.GetComponentInChildren<Text>();
        inventory = FindObjectOfType<Inventory>();
        spriteImage = GetComponent<Image>();
        UpdateItem(null);
        selectedItem = GameObject.Find("SelectedItem").GetComponent<UIItem>();
        tooltip = GameObject.Find("Tooltip").GetComponent<Tooltip>();
    }
    public void UpdateItem(Item item) {
        this.item = item;
        if(this.item != null) {
            spriteImage.color = Color.white;
            spriteImage.sprite = this.item.icon;
            int quantityValue = inventory.GetQuantities()[item.id][0];
            quantityText.color = Color.white;
            quantityText.text = quantityValue.ToString();
        } else {
            spriteImage.color = Color.clear;
            quantityText.color = Color.clear;
        }
    }
    public void UpdateItem(Item item, int index) {
        this.item = item;
        if(this.item != null) {
            spriteImage.color = Color.white;
            spriteImage.sprite = this.item.icon;
            int quantityValue = inventory.GetQuantitiesByKeyIndex(this.item.id, index);
            quantityText.color = Color.white;
            quantityText.text = quantityValue.ToString();
        } else {
            spriteImage.color = Color.clear;
            quantityText.color = Color.clear;
        }
    }
    public void OnPointerClick(PointerEventData eventData) {
        int clickCount = eventData.clickCount;
        if(clickCount == 2) {
            DoubleClick();
        }
        if(clickCount == 1) {
            SingleClick();
        }
    }
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
    }
    public void OnPointerEnter(PointerEventData eventData1) {
        if(this.item != null) {
            tooltip.GenerateTooltip(this.item);
        }
    }
    public void OnPointerExit(PointerEventData eventData2) {
        tooltip.gameObject.SetActive(false);
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
