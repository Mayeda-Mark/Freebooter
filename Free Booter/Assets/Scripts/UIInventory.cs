using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public List<UIItem> uIItems = new List<UIItem>();
    public GameObject slotPrefab;
    public Transform slotPanel;
    public int numberOfSlots = 16;
    private void Awake() {
        for (int i = 0; i < numberOfSlots; i++) {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slotPanel);
            uIItems.Add(instance.GetComponentInChildren<UIItem>());
        }
    }
    private void Update() {
        AssignQuantityIndex();
    }
    public void UpdateSlot(int slot, Item item) {
        uIItems[slot].UpdateItem(item);
    }
    public void AddNewItem(Item item) {
        UpdateSlot(uIItems.FindIndex(i => i.item == null), item);
    }
    public void RemoveItem(Item item) {
        UpdateSlot(uIItems.FindIndex(i => i.item == item), null);
    }
    private void AssignQuantityIndex() {
        List<int> slotQuantitiesIndexes = new List<int>();
        for (int i = 0; i < numberOfSlots; i++) { 
            if(uIItems[i].HasItem()) {
                int itemId = uIItems[i].GetItemId();
                bool hasFoundId = false;
                foreach(int quantity in slotQuantitiesIndexes) {
                    if(quantity == itemId) {
                        hasFoundId = true;
                    }
                }
                if(!hasFoundId) {
                    slotQuantitiesIndexes.Add(itemId);
                }
            }
        }
        foreach(int quantity in slotQuantitiesIndexes) {
            int numberFound = 0;
            bool hasFoundOne = false;
            for(int i = 0; i < numberOfSlots; i++) {
                if(uIItems[i].HasItem()) {
                    int itemId = uIItems[i].GetItemId();
                    if(quantity == itemId && !hasFoundOne) {
                        hasFoundOne = true;
                        uIItems[i].UpdateThisItem(numberFound);
                        Debug.Log(numberFound);
                        numberFound++;
                    }
                    if(quantity == itemId && hasFoundOne) {
                        //slotQuantitiesIndexes[key].Add(numberFound);
                        uIItems[i].UpdateThisItem(numberFound);
                        Debug.Log(numberFound);
                        numberFound++;
                    }
                }
            }
        }
    }
}
