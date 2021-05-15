using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantMenu : MonoBehaviour
{
    public GameObject buySellPrefab, merchantCanvas;
    public Transform merchantPanel;
    public List<Item> itemsForSale;
    Dictionary<Item, int> shoppingCart = new Dictionary<Item, int>();
    //List<int> quantities = new List<int>();
    public List<BuySellUI> buySellUIs = new List<BuySellUI>();
    //public List<BuySellUI> sellUIs = new List<BuySellUI>();
    //Dictionary<int, List<int>> quantitiesFromInventory = new Dictionary<int, List<int>>();
    public Text cart, alertBox;
    int cartTotal = 0;
    Inventory inventory;
    public Button checkout;
    ItemDB itemDB;
    int numBuySellSlots;//numSellSlots, numBuySlots = 0;
    bool buy, cartEmpty = true;

    public void SetUpSell()
    {
        merchantCanvas.SetActive(true);
        ClearUI();
        foreach (Item item in inventory.inventoryItems)
        {
            if (item.ableToBuy)
            {
                GameObject instance = Instantiate(buySellPrefab);
                instance.transform.SetParent(merchantPanel);
                instance.GetComponent<BuySellUI>().SetUpUI(item, buy);
            }
        }
    }
    //MAYBE SET UP A BOOL IN BUYSELLUI TO DETERMINE WHETHER YOU ARE BUYING OR SELLING
    public void SetUpBuy()
    {
        buy = true;
        merchantCanvas.SetActive(true);
        inventory = FindObjectOfType<Inventory>();
        ClearUI();
        foreach (Item item in itemsForSale)
        {
            GameObject instance = Instantiate(buySellPrefab);
            instance.transform.SetParent(merchantPanel);
            instance.GetComponent<BuySellUI>().SetUpUI(item, buy);
        }
    }
    private void ClearUI()
    {
        for (int i = 0; i < merchantPanel.childCount; i++)
        {
            Destroy(merchantPanel.GetChild(i));
        }
    }
    /*private void Awake() // May need to be Start instead of Awake
    {
        inventory = FindObjectOfType<PlayerShipController>().GetComponent<Inventory>();
        quantitiesFromInventory = inventory.GetQuantities();
        checkout = GetComponent<Button>();
        itemDB = FindObjectOfType<ItemDB>();
        numBuySlots = itemIdsForSale.Count;
    }*/
   /* void Start() {
        alertBox.gameObject.SetActive(false);
        buyInventoryPanel.gameObject.SetActive(false);
        sellInventoryPanel.gameObject.SetActive(false);
        //cart.gameObject.SetActive(false);
        TurnOffCart();
        SetUpForBuy();
    }*/
    /*public void UpdateCartInfo(Item item, int cost, int quantity) {
        // GET HOW MUCH GOLD THE PLAYER HAS AND COMPARE IT AGAINST HOW MUCH THEY WANT TO SPEND. IF WHAT THEY WANT TO SPEND EXCEEDS THE AMOUNT OF GOLD THEY HAVE, DON'T ADD ANYTHING TO THE CART AND SET THE BUYSELL VALUES BACK TO WHAT THEY WERE BEFORE THE BUTTON WAS PRESSED
        int playerGold = inventory.GetTotalGold();
        int costInCart = 0;
        int totalCostInCart = 0;
        int quantityIndex = 0;
        bool foundItemInCart = false;
        foreach(var key in shoppingCart.Keys) {
            totalCostInCart += shoppingCart[key];
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
            quantities.Add(item.buyQuantity);
        } else{
            if(totalCostInCart + cost <= playerGold) {
                alertBox.gameObject.SetActive(false);
                shoppingCart[item] = (costInCart + cost);
                quantities[quantityIndex] = quantity;
            } else {
                alertBox.gameObject.SetActive(true);
                foreach(BuySellUI ui in buyUIs) {
                    if(item == ui.item) {
                        Debug.Log("called");
                        ui.SetQuantityInCart(quantities[quantityIndex]);
                        ui.SetCostInCart(costInCart);
                    }
                }
            }
        }    
        if(!buy) {
            UpdateSellTextBox();
        }
        DisplayCart();
    }*/
   /* public void UpdateCartForSale(Item item, int cost, int quantity) {
        int amountOfItem = 0;
        List<int> amountPlayerHas = inventory.GetQuantitiesByKey(item.id);
        foreach(int stack in amountPlayerHas) {
            amountOfItem += stack;
        }
        int maxCost = (amountOfItem * cost) / item.buyQuantity;
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
            quantities.Add(item.buyQuantity); // ADD IT TO THE QUANTITY LIST TOO
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
                        Debug.Log("called");
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
    }*/
    /*public void RemoveItemFromCart(Item item, int cost) {
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
        string cartText = "Gold: " + inventory.GetTotalGold() + "\n\n";
        foreach(var key in shoppingCart.Keys) {
            totalCost += shoppingCart[key];
            cartText += key.itemName + ":\t" + shoppingCart[key].ToString() + " Gold \n";
        }
        cartText += "\nTotal: " + *//*costInCart.ToString()*//*totalCost.ToString() + " Gold";
        cart.text = cartText;
    }*/
    /*public void BuySellButton() {
        List<Item> transactionItems = new List<Item>();
        int quantityIndex = 0;
        foreach(var key in shoppingCart.Keys) {
            if(buy) {
                inventory.RemoveGold(shoppingCart[key]);
                inventory.GiveItem(key.id, quantities[quantityIndex]);
                buyInventoryPanel.gameObject.SetActive(false);
            } else {
                inventory.AddGold(shoppingCart[key]);
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
    }*/
   /* public void BuyButtonClick() {
        DisplayCart();
        
        buyInventoryPanel.gameObject.SetActive(true);
        sellInventoryPanel.gameObject.SetActive(false);
        cart.gameObject.SetActive(true);
        buy = true;
        //costInCart = 0;
    }*/
    /*private void SetUpForBuy() {
        for(int i = 0; i < numBuySlots; i++) {
            GameObject instance = Instantiate(buySellPrefab);
            instance.transform.SetParent(buyInventoryPanel);
            buyUIs.Add(instance.GetComponentInChildren<BuySellUI>());
        }
        for(int i = 0; i < numBuySlots; i++) {
            buyUIs[i].UpdateEntry(itemDB.GetItem(itemIdsForSale[i]));
        }
        UpdateBuyTextBox();
    }*/
    /*public void SellButtonClick() {
        //inventory = FindObjectOfType<PlayerShipController>().GetComponent<Inventory>();
        SetUpForSale();
        buyInventoryPanel.gameObject.SetActive(false);
        sellInventoryPanel.gameObject.SetActive(true);
        cart.gameObject.SetActive(true);
        buy = false;  
        //costInCart = 0;     
    }*/
    /*private void SetUpForSale() { 
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
        for(int i = 0; i < numSellSlots; i++) {
            if(inventory.ReturnByIndex(i) != null) {
                sellUIs[i].UpdateEntry(inventory.ReturnByIndex(i));
            }
        }
        UpdateSellTextBox();
    }*/
    /*public bool isBuy() { return buy; }
    public List<int> GetQuantitiesByKey(int key) {
        return quantitiesFromInventory[key];
    }
    private void UpdateBuyTextBox() {
        for (int i = 0; i < numBuySlots; i++) {
            string priceString = "Price: " + buyUIs[i].item.buyPrice.ToString();
            string quantityString = buyUIs[i].item.buyQuantity.ToString();
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
            string priceString = "Price: " + sellUIs[i].item.buyPrice.ToString();
            string quantityString = sellUIs[i].item.buyQuantity.ToString();
            int totalQuantity = 0;
            int id = sellUIs[i].item.id;
            List<int> quantityInInventory = inventory.GetQuantitiesByKey(id); // THIS IS THE QUANTITIES FROM THE INVENTORY
            // for(int j = 0; i < quantityInInventory.Count; j++) {
            //     Debug.Log(j);
            //     /*totalQuantity +=*//* Debug.Log(quantityInInventory[j]);
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
    }*/
    /*private int GetQuantityIndex(Item item) {
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
    }*/
}
/*THINGS TO DO BEFORE NEXT BUILD IS FINISHED: 
    BUG: WHEN YOU DROP BELOW 0 ON AN ITEM'S QUANTITY, IT DOESN'T REMOVE IT FROM THE UI AND CAUSES AN ARGUMENTOUTOFRANGE ERROR - MIGHT NOT BE ABLE TO SPLIT QUANTITY, WHICH COULD KIND OF SUCK
    PLUG GOLD INTO REPAIR SHIP
    CLEAN UP YOUR CODE, PARTICULARLY THE UPDATE BUY/SELL CART FUNCTIONS
*/