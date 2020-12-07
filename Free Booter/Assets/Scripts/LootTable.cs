using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTable : MonoBehaviour
{
    [System.Serializable]
    public class TableItem
    {
        public string itemName;
        public int itemId;
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
        inventory = FindObjectOfType<PlayerShipController>().GetComponent<Inventory>();
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
        int randomNumber = UnityEngine.Random.Range(0, total);
        int quantityToGive = UnityEngine.Random.Range(minLoot, maxLoot);
        foreach(var id in table)
        {
            if(randomNumber <= id.lootProbability)
            {
                Item itemToGive = itemDB.GetItem(id.itemId);
                if(itemToGive.map)
                {
                    Item existingMap = inventory.CheckForItem(id.itemId);
                    if(existingMap != null)
                    {
                        awardLoot();
                        return;
                    }
                    else
                    {
                        inventory.GiveItem(id.itemId, 1);
                    }
                } 
                else
                {
                    inventory.GiveItem(id.itemId, quantityToGive);
                }
            }
            else
            {
                randomNumber -= id.lootProbability;
            }
        }
    }
}
