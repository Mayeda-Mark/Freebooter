using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public Dictionary<int, List<int>> slotQuantitiesIndexes = new Dictionary<int, List<int>>();

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

    
        //I need to iterate through the items I have in my inventory and then assign indexes so that I can match them
        // with their quantities list index
        /* If I iterate through my dictionary, I could check the item.id in each slot against my Dictionary and then,
        when they match up, I could insert the data nad then iterate the value.
        */
    private void AssignQuantityIndex() {
        for (int i = 0; i < numberOfSlots; i++) { // Iterate through the slots...
            if(uIItems[i].HasItem()) {
                bool foundId = false;
                int index = uIItems[i].GetItemId();
                foreach(int key in slotQuantitiesIndexes.Keys) { //Iterate through the dictionary
                    if(!foundId && key == index) { // If you haven't found a match yet, but then you find a match
                        slotQuantitiesIndexes[key].Add(slotQuantitiesIndexes[key].Count); // Add an index
                        foundId = true;
                    }
                }
                if(!foundId) { // Otherwise, add to the dictionary
                    slotQuantitiesIndexes.Add(index, new List<int>() {0});
                }
            }
        }
        foreach(int key in slotQuantitiesIndexes.Keys) {
            int valueIndex = 0;
            for (int i = 0; i < numberOfSlots; i++) {
                if(uIItems[i].HasItem()) {
                    if(uIItems[i].GetItemId() == key) {
                        uIItems[i].UpdateThisItem(slotQuantitiesIndexes[key][valueIndex]);
                    }
                }
            }
        }
    }
}
