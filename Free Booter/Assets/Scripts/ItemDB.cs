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
            new Item(0, "Cannon Balls", "Big balls of metal that you can hurl at other boats", 99, 
            new Dictionary<string, int> {
                {"HullDamage", 20},
                {"SailDamage", 5}
            }), 
            new Item(1, "Chain Shot", "It's like a bolo, but for a boat", 99,
            new Dictionary<string, int> {
                {"HullDamage", 5},
                {"SailDamage", 20}
            }),
            new Item(2, "Gold", "Sparkly orangish stuff that you can buy things with", 300  , 
            new Dictionary<string, int> {}),
            new Item(3, "Rum", "The nectar of the Gods. Good on ham", 300 ,
            new Dictionary<string, int> {}),
            new Item(4, "Silk", "Soft fabric that can be traded for gold", 300 ,
            new Dictionary<string, int> {}),
            new Item(5, "Spices", "Things that make other things tase good", 300 ,
            new Dictionary<string, int> {}),
        };
    }
}
