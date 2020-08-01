using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    [SerializeField] public List<Item> items = new List<Item>();
    private void Awake() {
        BuildDb();
    }
    public Item GetItem(int id) {
        return items.Find(item => item.id == id);
    }
    public Item GetItem(string itemName) {
        return items.Find(item => item.itemName == itemName);
    }
    void BuildDb() {
        items = new List<Item>{
            new Item(0, "Cannon Balls", "Big balls of metal that you can hurl at other boats", 
            new Dictionary<string, int> {
                {"Cost", 20},
                {"HullDamage", 20},
                {"SailDamage", 5}
            }), 
            new Item(1, "Chain Shot", "It's like a bolo, but for a boat", 
            new Dictionary<string, int> {
                {"Cost", 20},
                {"HullDamage", 5},
                {"SailDamage", 20}
            })
        };
    }
}
