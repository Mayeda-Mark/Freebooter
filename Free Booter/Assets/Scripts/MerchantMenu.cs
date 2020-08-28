﻿using System.Collections;
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
    public List<BuySellUI> buyUIs = new List<BuySellUI>();
    public List<BuySellUI> sellUIs = new List<BuySellUI>();
    Dictionary<int, List<int>> quantitiesFromInventory = new Dictionary<int, List<int>>();
    public Text cart;
    int cartTotal = 0;
    Inventory inventory;
    Button checkout;
    ItemDB itemDB;
    int numSellSlots, numBuySlots = 0;
    bool buy, cartEmpty
     = true;
    private void Awake() // May need to be Start instead of Awake
    {
        inventory = FindObjectOfType<PlayerShipController>().GetComponent<Inventory>();
        quantitiesFromInventory = inventory.GetQuantities();
        checkout = GetComponent<Button>();
        itemDB = FindObjectOfType<ItemDB>();
        numBuySlots = itemIdsForSale.Count;
    }
    void Start() {
        buyInventoryPanel.gameObject.SetActive(false);
        sellInventoryPanel.gameObject.SetActive(false);
        //cart.gameObject.SetActive(false);
        TurnOffCart();
        SetUpForBuy();
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
        if(!buy) {
            UpdateSellTextBox();
        }
        DisplayCart();
    }
    public void UpdateCartForSale(Item item, int cost, int quantity) {
        int amountOfItem = 0;
        List<int> amountPlayerHas = inventory.GetQuantitiesByKey(item.id);
        foreach(int stack in amountPlayerHas) {
            amountOfItem += stack;
        }
        int maxCost = (amountOfItem * cost) / item.stats["QuantitySoldIn"];
        int costInCart = 0;
        int quantityIndex = 0;
        bool foundItemInCart = false;
        foreach(var key in shoppingCart.Keys) { //CHECK YOUR SHOPPING CART FOR THE ITEM KEYS
            if(item == key) { // IF YOU FIND IT
                foundItemInCart = true; // SET BOOL TO TRUE
                costInCart = shoppingCart[key];  // ASSOCIATE THE COST IN THE CART WITH THE KEY
                quantities[quantityIndex] = quantity; // ASSOCIATE THE QUANTITY WITH THE KEY
            }
            if(!foundItemInCart) {
                quantityIndex ++; // OTHERWISE ITERATE THE QUANTITY INDEX
            }
        }
        if(!foundItemInCart) { // IF YOU MAKE IT THROUGH THE DICTIONARY WITHOUT FINDING THE KEY
            shoppingCart.Add(item, cost); // ADD IT
            quantities.Add(item.stats["QuantitySoldIn"]); // ADD IT TO THE QUANTITY LIST TOO
        } else{ // IF YOU MAKE IT THROUGH THE LOOP AND HAVE FOUND THE ITEM...
            if(costInCart + cost < maxCost) { //... AND THE COST IN THE CART IS LESS THAN THE MAXCOST
                shoppingCart[item] = (costInCart + cost); // ADD THE COST TO THE CART
            } else {
                shoppingCart[item] = maxCost;
            }
            if(quantity < amountOfItem) {
                quantities[quantityIndex] = quantity;
            } else{
                quantities[quantityIndex] = amountOfItem;
                foreach(BuySellUI ui in sellUIs) {
                    if(item == ui.item) {
                        ui.SetQuantityInCart(amountOfItem);
                        ui.SetCostInCart(maxCost);
                    }
                }
            } // ADD THE COST IN THE CART TO WHAT IS ALREADY IN THERE // UPDATE THE VALUE IN THE QUANTITY LIST
            // TODO: make sure the player can't add more than what they currently have in their inventory
        }
        cartEmpty = false;
        DisplayCart();   
        UpdateSellTextBox(); 
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
    private void TurnOffCart() {
        cart.text = "";
        cart.gameObject.SetActive(false);
    }
    private void DisplayCart() {
        int totalCost = 0;
        string cartText = "";
        foreach(var key in shoppingCart.Keys) {
            totalCost += shoppingCart[key];
            cartText += key.itemName + ":\t" + shoppingCart[key].ToString() + " Gold \n";
        }
        cartText += "\nTotal: " + /*costInCart.ToString()*/totalCost.ToString() + " Gold";
        cart.text = cartText;
    }
    public void BuySellButton() {
        List<Item> transactionItems = new List<Item>();
        int quantityIndex = 0;
        foreach(var key in shoppingCart.Keys) {
            if(buy) {
                inventory.GiveItem(key.id, quantities[quantityIndex]);
                buyInventoryPanel.gameObject.SetActive(false);
            } else {
                inventory.DecreaseQuantity(key.id, quantities[quantityIndex]);
                sellInventoryPanel.gameObject.SetActive(false);
            }
            transactionItems.Add(key);
        }
        foreach(var item in transactionItems) {
            shoppingCart.Remove(item);
        }
        for(int i = quantities.Count - 1; i > 0; i--) {
            quantities.RemoveAt(i);
        }
        //costInCart = 0; 
        //cart.gameObject.SetActive(false);
        TurnOffCart();
    }
    public void BuyButtonClick() {
        buyInventoryPanel.gameObject.SetActive(true);
        sellInventoryPanel.gameObject.SetActive(false);
        cart.gameObject.SetActive(true);
        buy = true;
        //costInCart = 0;
    }
    private void SetUpForBuy() {
        for(int i = 0; i < numBuySlots; i++) {
            GameObject instance = Instantiate(buySellPrefab);
            instance.transform.SetParent(buyInventoryPanel);
            buyUIs.Add(instance.GetComponentInChildren<BuySellUI>());
        }
        for(int i = 0; i < numBuySlots; i++) {
            buyUIs[i].UpdateEntry(itemDB.GetItem(itemIdsForSale[i]));
        }
        UpdateBuyTextBox();
    }
    public void SellButtonClick() {
        //inventory = FindObjectOfType<PlayerShipController>().GetComponent<Inventory>();
        SetUpForSale();
        buyInventoryPanel.gameObject.SetActive(false);
        sellInventoryPanel.gameObject.SetActive(true);
        cart.gameObject.SetActive(true);
        buy = false;  
        //costInCart = 0;     
    }
    private void SetUpForSale() { 
        //inventory = FindObjectOfType<PlayerShipController>().GetComponent<Inventory>();
        numSellSlots = inventory.GetCountLessGold();
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
        } else if(sellUIs.Count > numSellSlots){
            for(int k = sellUIs.Count - 1; k >= numSellSlots; k--) {
                sellUIs[k].KillSelf();
                sellUIs.RemoveAt(k);
            }
        }
        for(int i = 0; i < numSellSlots; i++) { // FIGURE OUT HOW TO SKIP GOLD IN INVENTORY
            //if(sellUIs[i].GetItem() != inventory.ReturnByIndex(i)) {
                if(inventory.ReturnByIndex(i) != null) {
                    sellUIs[i].UpdateEntry(inventory.ReturnByIndex(i));
                }
            //}
        }
        //DebugSellUIS();
        UpdateSellTextBox();
    }
    // private void DebugSellUIS() {
    //     for(int i = 0; i < sellUIs.Count; i++) {
    //         Debug.Log(i);
    //     }
    // }
    public bool isBuy() { return buy; }
    public List<int> GetQuantitiesByKey(int key) {
        return quantitiesFromInventory[key];
    }
    private void UpdateBuyTextBox() {
        for (int i = 0; i < numBuySlots; i++) {
            string priceString = "Price: " + buyUIs[i].item.stats["BaseCost"].ToString();
            string quantityString = buyUIs[i].item.stats["QuantitySoldIn"].ToString();
            string textBox = string.Format("{0} X {1}\n{2}", buyUIs[i].item.itemName, quantityString, priceString);
            buyUIs[i].UpdateText(textBox);
        }
    }
    public void UpdateSellTextBox() {
        for (int i = 0; i < numSellSlots; i++) {
            bool itemInCart = false;
            foreach(Item key in shoppingCart.Keys) {
                if(key == sellUIs[i].item) {
                    itemInCart = true;
                }
            }
            string cartQuantity = "";
            string priceString = "Price: " + sellUIs[i].item.stats["BaseCost"].ToString();
            string quantityString = sellUIs[i].item.stats["QuantitySoldIn"].ToString();
            int totalQuantity = 0;
            int id = sellUIs[i].item.id;
            List<int> quantityInInventory = inventory.GetQuantitiesByKey(id); // THIS IS THE QUANTITIES FROM THE INVENTORY
            // for(int j = 0; i < quantityInInventory.Count; j++) {
            //     Debug.Log(j);
            //     /*totalQuantity +=*/ Debug.Log(quantityInInventory[j]);
            // }
            foreach(int stack in quantityInInventory) {
                totalQuantity += stack; // THIS IS THE TOTAL AMOUNT OF ONE ITEM IN THE INVENTORY
            }
            Debug.Log(totalQuantity);
            if(!itemInCart) {
                Debug.Log("Called");
                cartQuantity = totalQuantity.ToString(); // THIS IS THAT IN A STRING
            } else {
                int quantityInCart = quantities[GetQuantityIndex(sellUIs[i].item)];
                cartQuantity = (totalQuantity - quantityInCart).ToString();
            }
            Debug.Log(cartQuantity);
            string stock = "Quantity: " + cartQuantity;
            string textBox = string.Format("{0} X {1}\n{2}\n{3}", sellUIs[i].item.itemName, quantityString, priceString, stock);
            sellUIs[i].UpdateText(textBox);
        }
    }
    private int GetQuantityIndex(Item item) {
        int quantityIndex = 0;
        bool foundIndex = false;
        foreach(var key in shoppingCart.Keys) {
            if(key == item) {
                foundIndex = true;
            } 
            if(!foundIndex) {
                quantityIndex++;
            }
        } 
        return quantityIndex;
    }
}
/*THINGS TO DO BEFORE NEXT BUILD IS FINISHED: 
    BUG: WHEN YOU DROP BELOW 0 ON AN ITEM'S QUANTITY, IT DOESN'T REMOVE IT FROM THE UI AND CAUSES AN ARGUMENTOUTOFRANGE ERROR - MIGHT NOT BE ABLE TO SPLIT QUANTITY, WHICH COULD KIND OF SUCK
    BUG: wHEN YOU GET MORE THAN ONE INVENTORY SLOT OF AN ITEM, IT DUPLICATES IN THE SELL MENU AS WELL
    BUG: ABLE TO DECREASE ITEMS PAST WHAT THE PLAYER CURRENTLY HAS IN SELL
    PLUG IN GOLD TO THE MERCHANT MENUS
    PLUG GOLD INTO REPAIR SHIP
    MAKE FLOATING TEXT BOX FOR NOT ENOUGH GOLD
    CLEAN UP YOUR CODE, PARTICULARLY THE UPDATE BUY/SELL CART FUNCTIONS
*/