using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuySellUI : MonoBehaviour
{
    public Item item;
    public Image spriteImage;
    public Text descriptonText;
    int costInCart = 0;
    MerchantMenu merchantMenu;
    void Start()
    {
        merchantMenu = GetComponentInParent<MerchantMenu>();
        descriptonText = GetComponent<Text>();
        spriteImage = GetComponent<Image>();
    }
    public void UpdateEntry(Item item) {
        this.item = item;
        spriteImage.sprite = this.item.icon;
        string priceString = "Price: " + item.stats["BaseCost"].ToString();
        string textBox = string.Format("{0}\n{1}", item.itemName, priceString);
        descriptonText.text = textBox;
    }
    public void AddToCart() {
        costInCart += this.item.stats["BaseCost"];
        merchantMenu.UpdateCartInfo(this.item, costInCart);
    }
    public void RemoveFromCart() {
        if(costInCart > 0) {
            costInCart -= this.item.stats["BaseCost"];
            if(costInCart == 0) {
                merchantMenu.RemoveItemFromCart(this.item, 0);
            }
        }
    }
}
