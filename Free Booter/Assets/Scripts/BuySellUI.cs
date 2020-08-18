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
    void Start()
    {
        merchantMenu = GetComponentInParent<MerchantMenu>();
        descriptonText = GetComponent<Text>();
        spriteImage = GetComponent<Image>();
        costOfItem = this.item.stats["BaseCost"];
    }
    public void UpdateEntry(Item item) {
        this.item = item;
        spriteImage.sprite = this.item.icon;
        string priceString = "Price: " + item.stats["BaseCost"].ToString();
        string quantityString = item.stats["QuantitySoldIn"].ToString();
        string textBox = string.Format("{0} X {1}\n{2}", item.itemName, quantityString, priceString);
        descriptonText.text = textBox;
    }
    public void AddToCart() {
        costInCart += this.item.stats["BaseCost"];
        quantityInCart += this.item.stats["QuantitySoldIn"];
        merchantMenu.UpdateCartInfo(this.item, costOfItem, quantityInCart);
    }
    public void RemoveFromCart() {
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
    public Item GetItem() { return item; }
}
