using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTable : MonoBehaviour
{
    [System.Serializable]
    public class TableItem
    {
        public string itemName;
        public int[] itemIds;
        public int lootProbability;
    }
    //public Dictionary<int /*item id*/, int /*probability*/> table;
    public List<TableItem> table;

    Inventory inventory;
    ItemDB itemDB;
    [SerializeField] int maxLoot;
    [SerializeField] int minLoot;
    int total;
    void Start()
    {
        //table = new Dictionary<int, int>();
        inventory = FindObjectOfType<Inventory>();
        itemDB = FindObjectOfType<ItemDB>();
        SetTotal();
    }
    private void SetTotal()
    {
        foreach(var item in table)
        {
            total += item.lootProbability;
        }
    }
    public void awardLoot()
    {
        int randomNumber = Random.Range(0, total);
        int quantityToGive = Random.Range(minLoot, maxLoot);
        print(randomNumber);
        foreach(var id in table)
        {
            int index = GetItemId(id.itemIds.Length);
            if(randomNumber <= id.lootProbability)
            {
                Item itemToGive = itemDB.GetItem(id.itemIds[index]);
                if(itemToGive.isAMap)
                {
                    List<int> mapsTriedToGive = new List<int>();
                    bool mapAlreadyInInventory = false;
                    do
                    {
                        Item existingMap = inventory.CheckForItem(id.itemIds[index]);
                        if (existingMap != null)
                        {
                            mapAlreadyInInventory = true;
                            index = GetItemId(id.itemIds.Length);
                        } else
                        {
                            mapAlreadyInInventory = false;
                            inventory.GiveItem(id.itemIds[index], 1);
                            return;
                        }
                        bool mapInList = false;
                        foreach(int map in mapsTriedToGive)
                        {
                            if(map == id.itemIds[index])
                            {
                                mapInList = true;
                            }
                        }
                        if(!mapInList)
                        {
                            mapsTriedToGive.Add(id.itemIds[index]);
                        }
                        if(mapsTriedToGive.Count == id.itemIds.Length)
                        {
                            awardLoot();
                            return;
                        }
                    } while (mapAlreadyInInventory);

                   /* bool noUniqueMap = false;
                    int[] randomizedIndexes = RandomizeArray(id.itemIds);
                    foreach(int arrayIndex in randomizedIndexes)
                    {
                        print("Array Index: " + arrayIndex);
                        Item existingMap = inventory.CheckForItem(id.itemIds[arrayIndex]);
                        if (existingMap != null)
                        {
                            noUniqueMap = true;
                        } else
                        {
                            inventory.GiveItem(id.itemIds[arrayIndex], 1);
                            return;
                        }
                    }
                    if(noUniqueMap)
                    {
                        awardLoot();
                        return;
                    }*/

                    //RANDOMIZE MAPS
                    /*Item existingMap = inventory.CheckForItem(id.itemIds[index]);
                    if(existingMap != null)
                    {
                        awardLoot();
                        return;
                    }
                    else
                    {
                        print("Giving Map");
                        inventory.GiveItem(id.itemIds[index], 1);
                        return;
                    }*/
                } 
                else
                {
                    print("Giving " + itemToGive.itemName);
                    inventory.GiveItem(id.itemIds[index], quantityToGive);
                    return;
                }
            }
            else
            {
                randomNumber -= id.lootProbability;
            }
        }
    }
    private int GetItemId(int count) { return Random.Range(0, count); }
    private int[] RandomizeArray(int[] array)
    {
        int[] shuffledArray = new int[array.Length];
        List<int> usedIndexes = new List<int>();
        for (int i = 0; i < array.Length; i++)
        {
            int index;
            bool foundIndex;
            do
            {
                foundIndex = false;
                index = Random.Range(0, array.Length - 1);
                foreach(int item in usedIndexes)
                {
                    if(index == item)
                    {
                        foundIndex = true;
                    }
                }
            }
            while (!foundIndex);
            usedIndexes.Add(index);
            shuffledArray[i] = array[index];
        }
        return shuffledArray;
    }
}
