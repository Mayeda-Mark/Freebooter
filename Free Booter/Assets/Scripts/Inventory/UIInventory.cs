using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public Transform slotPanel;
    public int numberOfSlots = 16;
    public UIItem[] uIItems;
    private void Start()
    {
        uIItems = new UIItem[numberOfSlots];
        for(int i = 0; i < numberOfSlots; i++)
        {
            uIItems[i] = slotPanel.GetChild(i).GetComponent<UIItem>();
        }
    }
    public void UpdateSlot(int slot, Item item, int quantity) {
        uIItems[slot].UpdateItem(item, quantity);
    }
    public void AddNewItem(Item item, int quantity)
    {
        bool foundEmpty = false;
        for(int i = 0; i < numberOfSlots - 1; i++)
        {
            if(!foundEmpty && uIItems[i].item == null)
            {
                UpdateSlot(i, item, quantity);
                foundEmpty = true;
            }
        }
    }
    public void AddExistingItem(Item item, int quantity)
    {
        bool foundItem = false;
        for (int i = 0; i < numberOfSlots - 1; i++)
        {
            if (!foundItem && !uIItems[i].itemQuantityFull && uIItems[i].item.id == item.id)
            {
                UpdateSlot(i, item, quantity);
                foundItem = true;
            }
        }

    }
    public void RemoveItem(Item item, int quantity) {
        bool foundItem = false;
        for(int i = numberOfSlots - 1; i > 0; i--)
        {
            if(!foundItem && item == uIItems[i].item)
            {
                if(quantity <= 0)
                {
                    UpdateSlot(i, null, 0);
                }
                else
                {
                    UpdateSlot(i, item, quantity);
                }
            }
        }
        //UpdateSlot(uIItems.FindIndex(i => i.item == item), null, 0);
    }
    // probably can delete
    private void AssignQuantityIndex() {
        List<int> slotQuantitiesIndexes = new List<int>(); // Make a list of all of the quantities indexes
        for (int i = 0; i < numberOfSlots; i++) { //Iterate through each of your inventory slots
            if(uIItems[i].HasItem()) { // If there's an item in the inventory slot
                int itemId = uIItems[i].GetItemId(); // Get its Id
                bool hasFoundId = false; // Make a bool set to false to signify wheter or not you've found the Id we're looking for
                foreach(int quantity in slotQuantitiesIndexes) { // Now iterate through the slotQuanitiesIndexes
                    if(quantity == itemId) { //If the index matches the Item id...
                        hasFoundId = true; //You found the id, set the bool to false
                    }
                }
                if(!hasFoundId) { // If you get all the way through the loop without finding the id
                    slotQuantitiesIndexes.Add(itemId); // Add it to the list
                }
            }
        }
        foreach(int quantity in slotQuantitiesIndexes) { // Iterate through the slotQauntitiesIndex list
            int numberFound = 0; // declare an int with a value of 0 to signify the number of items of each item that you've found
            bool hasFoundOne = false; // Set a bool to false to signify that you haven't found one of the item yet
            for(int i = 0; i < numberOfSlots; i++) { // Iterate through yout inventory slots
                if(uIItems[i].HasItem()) { // If there's something in that slot..
                    int itemId = uIItems[i].GetItemId(); // Grab its id
                    if(quantity == itemId && !hasFoundOne) { // If the id matches the index in the list and you haven't found one yet...
                        hasFoundOne = true; // You found one
                        uIItems[i].UpdateThisItem(numberFound); // Update that item with the number you have found (minus 1 to signify the index)
                        //Debug.Log(numberFound);
                        numberFound++; // Iterate the number found
                    }
                    else if(quantity == itemId && hasFoundOne) { // If you match the item index and you have found on already
                        uIItems[i].UpdateThisItem(numberFound); //Update that item with the correct index
                        numberFound++; // Iterate the index
                    }
                }
            }
        }
    }
}
