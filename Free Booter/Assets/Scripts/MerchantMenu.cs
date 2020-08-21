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
    public List<BuySellUI> buyUIs = new List<BuySellUI>();
    public List<BuySellUI> sellUIs = new List<BuySellUI>();
    Dictionary<int, List<int>> quantitiesFromInventory = new Dictionary<int, List<int>>();
    public Text cart;
    Inventory inventory;
    Button checkout;
    ItemDB itemDB;
    int numSellSlots, numBuySlots = 0;
    bool buy = true;
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
        cart.gameObject.SetActive(false);
        SetUpForBuy();
        // for(int i = 0; i < numBuySlots; i++) {
        //     buyUIs[i].UpdateEntry(itemDB.GetItem(itemIdsForSale[i]));
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
    }
    private void SetUpForSale() { 
        //inventory = FindObjectOfType<PlayerShipController>().GetComponent<Inventory>();
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
        } else if(sellUIs.Count > numSellSlots){
            for(int k = sellUIs.Count - 1; k > numSellSlots; k--) {
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
    private void UpdateSellTextBox() {
        for (int i = 0; i < numSellSlots; i++) {
            string priceString = "Price: " + sellUIs[i].item.stats["BaseCost"].ToString();
            string quantityString = sellUIs[i].item.stats["QuantitySoldIn"].ToString();
            int totalQuantity = 0;
            int id = sellUIs[i].item.id;
            List<int> quantityInInventory = inventory.GetQuantitiesByKey(id);
            // for(int j = 0; i < quantityInInventory.Count; j++) {
            //     Debug.Log(j);
            //     /*totalQuantity +=*/ Debug.Log(quantityInInventory[j]);
            // }
            foreach(int stack in quantityInInventory) {
                totalQuantity += stack;
            }
            string stock = "Quantity: " + totalQuantity.ToString();
            string textBox = string.Format("{0} X {1}\n{2}\n{3}", sellUIs[i].item.itemName, quantityString, priceString, stock);
            sellUIs[i].UpdateText(textBox);
        }
    }
}

/*THINGS TO DO BEFORE NEXT BUILD IS FINISHED: 
    FIGURE OUT WORK AROUND FOR NULL REFERENCE IN SELL MENU REFRESH
    FIGURE OUT HOW TO HAVE GOLD NOT INTERFERE WITH THE MERCHANT MENUS
    PLUG IN GOLD TO THE MERCHANT MENUS
    PLUG GOLD INTO REPAIR SHIP
    MAKE FLOATING TEXT BOX FOR NOT ENOUGH GOLD
*/