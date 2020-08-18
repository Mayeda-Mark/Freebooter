using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantMenu : MonoBehaviour
{
    public GameObject buySellPrefab;
    public Transform buyInventoryPanel, sellInventoryPanel;
    public List<int> itemIdsForSale;
    Dictionary<Item, int> shoppingCart = new Dictionary<Item, int>();
    List<int> quantities = new List<int>();
    public List<BuySellUI> buySellUIs = new List<BuySellUI>();
    public List<BuySellUI> sellUIs = new List<BuySellUI>();
    public Text cart;
    Inventory inventory;
    Button checkout;
    ItemDB itemDB;
    int numSellSlots, numBuySlots = 0;
    bool buy = true;
    private void Awake() // May need to be Start instead of Awake
    {
        inventory = FindObjectOfType<PlayerShipController>().GetComponent<Inventory>();
        checkout = GetComponent<Button>();
        itemDB = FindObjectOfType<ItemDB>();
        numBuySlots = itemIdsForSale.Count;
        SetUpForBuy();
        //SetUpForSale();
    }
    void Start() {
        buyInventoryPanel.gameObject.SetActive(false);
        sellInventoryPanel.gameObject.SetActive(false);
        cart.gameObject.SetActive(false);
        for(int i = 0; i < numBuySlots; i++) {
            buySellUIs[i].UpdateEntry(itemDB.GetItem(itemIdsForSale[i]));
        }
        // for(int i = 0; i < numSellSlots; i++) {
        //     sellUIs[i].UpdateEntry(inventory.ReturnByIndex(i));
        // }
    }
    public void UpdateCartInfo(Item item, int cost, int quantity) {
        int costInCart = 0;
        int quantityIndex = 0;
        bool foundItemInCart = false;
        foreach(var key in shoppingCart.Keys) {
            if(item == key) {
                foundItemInCart = true;
                costInCart = shoppingCart[key];
                quantities[quantityIndex] = quantity;
            }
            if(!foundItemInCart) {
                quantityIndex ++;
            }
        }
        if(!foundItemInCart) {
            shoppingCart.Add(item, cost);
            quantities.Add(item.stats["QuantitySoldIn"]);
        } else{
            shoppingCart[item] = (costInCart + cost);
            quantities[quantityIndex] = quantity;
        }    
        DisplayCart();
    }
    public void RemoveItemFromCart(Item item, int cost) {
        int quantityIndex = 0;
        foreach(var key in shoppingCart.Keys) {
            if(key == item) {
                quantities.RemoveAt(quantityIndex);
            } else {
                quantityIndex++;
            }
        }
        shoppingCart.Remove(item);
        DisplayCart();
    }
    private void DisplayCart() {
        int totalCost = 0;
        string cartText = "";
        foreach(var key in shoppingCart.Keys) {
            totalCost += shoppingCart[key];
            cartText += key.itemName + ":\t" + shoppingCart[key].ToString() + " Gold \n";
        }
        cartText += "\nTotal: " + totalCost.ToString() + " Gold";
        cart.text = cartText;
    }
    public void BuySellButton() {
        foreach (int quantity in quantities) {
            Debug.Log(quantity);
        }
        if(buy) {
            List<Item> purchasedItems = new List<Item>();
            int quantityIndex = 0;
            foreach(var key in shoppingCart.Keys) {
                Debug.Log(quantityIndex);
                inventory.GiveItem(key.id, quantities[quantityIndex]);
                //shoppingCart.Remove(key);
                // quantities.RemoveAt(quantityIndex);
                // quantityIndex ++;
                purchasedItems.Add(key);
            }
            foreach(var item in purchasedItems) {
                shoppingCart.Remove(item);
            }
            for(int i = quantities.Count - 1; i > 0; i--) {
                quantities.RemoveAt(i);
            }
        }
    }
    public void BuyButtonClick() {
        buyInventoryPanel.gameObject.SetActive(true);
        sellInventoryPanel.gameObject.SetActive(false);
        cart.gameObject.SetActive(true);
        buy = true;
    }
    public void SellButtonClick() {
        SetUpForSale();
        buyInventoryPanel.gameObject.SetActive(false);
        sellInventoryPanel.gameObject.SetActive(true);
        cart.gameObject.SetActive(true);
        buy = false;       
    }
    private void SetUpForBuy() {
        for(int i = 0; i < numBuySlots; i++) {
            GameObject instance = Instantiate(buySellPrefab);
            instance.transform.SetParent(buyInventoryPanel);
            buySellUIs.Add(instance.GetComponentInChildren<BuySellUI>());
        }
    }
    private void SetUpForSale() { 
        numSellSlots = inventory.shipItems.Count;
        if(sellUIs.Count == 0) {
            for(int i = 0; i < numSellSlots; i++) {
                GameObject instance = Instantiate(buySellPrefab);
                instance.transform.SetParent(sellInventoryPanel);
                sellUIs.Add(instance.GetComponentInChildren<BuySellUI>());
            }
        } else if(sellUIs.Count < numSellSlots) {
            for(int j = sellUIs.Count; j < numSellSlots; j++) {
                GameObject instance = Instantiate(buySellPrefab);
                instance.transform.SetParent(sellInventoryPanel);
                sellUIs.Add(instance.GetComponentInChildren<BuySellUI>());
            }
        } else {
            Debug.Log("You need to figure out how to destroy some of these...");
        }
        for(int i = 0; i < numSellSlots; i++) { // FIGURE OUT HOW TO SKIP GOLD IN INVENTORY
            if(sellUIs[i].GetItem() != inventory.ReturnByIndex(i)) {
                sellUIs[i].UpdateEntry(inventory.ReturnByIndex(i));
            }
        }
    }
}