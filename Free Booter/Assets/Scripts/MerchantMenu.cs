using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantMenu : MonoBehaviour
{
    public GameObject buySellPrefab;
    public Transform inventoryPanel;
    public List<int> itemIdsForSale;
    List<Item> cartItems = new List<Item>();
    public List<BuySellUI> buySellUIs = new List<BuySellUI>();
    Text cart;
    Button checkout;
    ItemDB itemDB;
    int numSlots;
    private void Awake() // May need to be Start instead of Awake
    {
        cart = GetComponent<Text>();
        checkout = GetComponent<Button>();
        itemDB = FindObjectOfType<ItemDB>();
        numSlots = itemIdsForSale.Count;
        for(int i = 0; i < numSlots; i++) {
            GameObject instance = Instantiate(buySellPrefab);
            instance.transform.SetParent(inventoryPanel);
            buySellUIs.Add(instance.GetComponentInChildren<BuySellUI>());
        }
    }
    void Start() {
        for(int i = 0; i < numSlots; i++) {
            buySellUIs[i].UpdateEntry(itemDB.GetItem(itemIdsForSale[i]));
        }
    }
    public void UpdateCartInfo(Item item, int cost) {} // START HERE. YOU ARE WRITING THE SHOPPING CART TO THE TEXT BOX
    public void RemoveItemFromCart(Item item, int cost) {}
}
