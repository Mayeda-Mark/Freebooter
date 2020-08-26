using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuySellUI : MonoBehaviour
{
    public Item item;
    public Image spriteImage;
    public Text descriptonText;
    int costInCart, quantityInCart = 0;
    int costOfItem;
    MerchantMenu merchantMenu;
    //Inventory inventory;
    void Start()
    {
        //inventory = FindObjectOfType<PlayerShipController>().GetComponent<Inventory>();
        merchantMenu = GetComponentInParent<MerchantMenu>();
        //descriptonText = GetComponent<Text>();
        //spriteImage = GetComponent<Image>();
        costOfItem = this.item.stats["BaseCost"];
    }
    public void UpdateEntry(Item item) {
        this.item = item;
        spriteImage.sprite = this.item.icon;
        // if(!merchantMenu.isBuy()) {
        // List<int> quantityInInventory = merchantMenu.GetQuantitiesByKey(item.id); 
        // }
        /*
        string priceString = "Price: " + item.stats["BaseCost"].ToString();
        string quantityString = item.stats["QuantitySoldIn"].ToString();
        */
        // int totalQuantity = 0;
        // for(int i = 0; i < quantityInInventory.Count; i++) {
        //     totalQuantity += quantityInInventory[i];
        // }
        // string stock = "Quantity in inventory: " + totalQuantity.ToString();
        /*
        string textBox = string.Format("{0} X {1}\n{2}", item.itemName, quantityString, priceString);
        descriptonText.text = textBox;
        */
        //UpdateTextBox();
    }
    public void UpdateText(string textBox) {
        descriptonText.text = textBox;
    }
    // private void UpdateTextBox() {
    //     int totalQuantity = 0;
    //     string textBox = "";
    //     List<int> quantityInInventory = merchantMenu.GetQuantitiesByKey(item.id);
    //     for(int i = 0; i < quantityInInventory.Count; i++) {
    //         totalQuantity += quantityInInventory[i];
    //     }
    //     string stock = "Quantity in inventory: " + totalQuantity.ToString();
    //     string priceString = "Price: " + item.stats["BaseCost"].ToString();
    //     string quantityString = item.stats["QuantitySoldIn"].ToString();
    //     if(merchantMenu.isBuy()) {
    //         textBox = string.Format("{0} X {1}\n{2}", item.itemName, quantityString, priceString);
    //     } else {
    //         textBox = string.Format("{0} X {1}\n{2}\n{3}", item.itemName, quantityString, priceString, stock);
    //     }
    //     descriptonText.text = textBox;
    // }
    public void AddToCart() {
        if(merchantMenu.isBuy()) {
            costInCart += this.item.stats["BaseCost"];
            quantityInCart += this.item.stats["QuantitySoldIn"];
            merchantMenu.UpdateCartInfo(this.item, costOfItem, quantityInCart);
        } else {
            costInCart += this.item.stats["BaseCost"];
            quantityInCart += this.item.stats["QuantitySoldIn"];
            merchantMenu.UpdateCartForSale(/*this.item.stats["BaseCost"], this.item.stats["QuantitySoldIn"]*/this.item, costOfItem, quantityInCart);
            // merchantMenu.UpdateCartInfo(this.item, costOfItem, quantityInCart);
        }
    }
    public void SetQuantityInCart(int quantityInCart) {
        this.quantityInCart = quantityInCart;
    }
    public void RemoveFromCart() {
        if(merchantMenu.isBuy()) {
            if(costInCart > 0) {
                costInCart -= costOfItem;
                quantityInCart -= this.item.stats["QuantitySoldIn"];
                if(costInCart == 0) {
                    merchantMenu.RemoveItemFromCart(this.item, 0);
                } else {
                    merchantMenu.UpdateCartInfo(this.item, costOfItem * -1, quantityInCart);
                }
            }
        }
    }
    public Item GetItem() { return item; }
    public void KillSelf() {
        Destroy(gameObject);
    }
}
