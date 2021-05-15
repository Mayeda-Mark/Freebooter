using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuySellUI : MonoBehaviour
{
    public Item item;
    public Image spriteImage;
    public Text descriptonText;
    public Button plusButton, minusButton;
    int costInCart, quantityInCart = 0;
    int costOfItem;
    MerchantMenu merchantMenu;
    Inventory inventory;
    //private bool buy = default;

    public void SetUpUI(Item item, bool buy)
    {
        spriteImage.sprite = item.icon;
        if(buy)
        {
            SetUpBuy(item);
        }
    }
    private void SetUpBuy(Item item)
    {
        string text = "" + item.itemName + "\nPrice: " + item.buyPrice.ToString() + " X " + item.buyQuantity.ToString();
        descriptonText.text = text;
        plusButton.onClick.RemoveAllListeners();
        plusButton.onClick.AddListener(BuyPlusClick);
        minusButton.onClick.RemoveAllListeners();
        minusButton.onClick.AddListener(BuyMinusClick);
    }
    public void BuyPlusClick()
    {
        print("Plus Clicked");

    }
    public void BuyMinusClick()
    {
        print("Minus clicked!");
    }
    public void SellPlusClick()
    {

    }
    public void SellMinusClick()
    {

    }
    /*void Start()
    {
        inventory = FindObjectOfType<PlayerShipController>().GetComponent<Inventory>();
        merchantMenu = GetComponentInParent<MerchantMenu>();
        //descriptonText = GetComponent<Text>();
        //spriteImage = GetComponent<Image>();
        costOfItem = this.item.buyPrice;
    }
    public void UpdateEntry(Item item) {
        this.item = item;
        spriteImage.sprite = this.item.icon;
        // if(!merchantMenu.isBuy()) {
        // List<int> quantityInInventory = merchantMenu.GetQuantitiesByKey(item.id); 
        // }
        *//*
        string priceString = "Price: " + item.stats["BaseCost"].ToString();
        string quantityString = item.stats["QuantitySoldIn"].ToString();
        *//*
        // int totalQuantity = 0;
        // for(int i = 0; i < quantityInInventory.Count; i++) {
        //     totalQuantity += quantityInInventory[i];
        // }
        // string stock = "Quantity in inventory: " + totalQuantity.ToString();
        *//*
        string textBox = string.Format("{0} X {1}\n{2}", item.itemName, quantityString, priceString);
        descriptonText.text = textBox;
        *//*
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
            costInCart += costOfItem;
            quantityInCart += this.item.buyQuantity;
            merchantMenu.UpdateCartInfo(this.item, costOfItem, quantityInCart);
        } else {
            costInCart += costOfItem;
            quantityInCart += this.item.buyQuantity;
            merchantMenu.UpdateCartForSale(*//*this.item.stats["BaseCost"], this.item.stats["QuantitySoldIn"]*//*this.item, costOfItem, quantityInCart);
            // merchantMenu.UpdateCartInfo(this.item, costOfItem, quantityInCart);
        }
    }
    public void SetQuantityInCart(int quantityInCart) {
        this.quantityInCart = quantityInCart;
    }
    public void SetCostInCart(int costInCart) {
        this.costInCart = costInCart;
    }
    public void RemoveFromCart() {
        // if(merchantMenu.isBuy()) {
            if(costInCart > 0) {
                costInCart -= costOfItem;
                quantityInCart -= this.item.buyQuantity;
                if(costInCart <= 0) {
                    costInCart = 0;
                    if(!merchantMenu.isBuy()) {
                        merchantMenu.UpdateSellTextBox();
                    }
                    merchantMenu.RemoveItemFromCart(this.item, 0);
                } else {
                    merchantMenu.UpdateCartInfo(this.item, costOfItem * -1, quantityInCart);
                }
            }
        // } else {
        //     if(costInCart > 0) {
        //         int amountInInventory = inventory.GetTotalQuantity(this.item.id);
        //         if(amountInInventory - this.item.stats["QuantitySoldIn"] < 0) {
        //         } else{
        //         }
        //     }
        // }
    }
    public Item GetItem() { return item; }
    public void KillSelf() {
        Destroy(gameObject);
    }*/
}
