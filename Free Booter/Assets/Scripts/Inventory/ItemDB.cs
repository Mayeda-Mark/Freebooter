using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    [SerializeField] public List<Item> items = new List<Item>();
    [SerializeField] Sprite[] icons;
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
            new Item(0, "Cannon Balls", "Big balls of metal that you can hurl at other boats", icons[0], 99, 
            new Dictionary<string, int> {
                {"HullDamage", 20},
                {"SailDamage", 5},
                {"BaseCost", 5},
                {"QuantitySoldIn", 10}
            }, true), 
            new Item(1, "Chain Shot", "It's like a bolo, but for a boat", icons[1], 99,
            new Dictionary<string, int> {
                {"HullDamage", 5},
                {"SailDamage", 20},
                {"BaseCost", 5},
                {"QuantitySoldIn", 10}
            }, true),
            new Item(2, "Gold", "Sparkly orangish stuff that you can buy things with", icons[2], 300 , 
            new Dictionary<string, int> {}, false),
            new Item(3, "Rum", "The nectar of the Gods. Good on ham", icons[3], 300,
            new Dictionary<string, int> {
                {"BaseCost", 10},
                {"QuantitySoldIn", 10}
            }, false),
            new Item(4, "Silk", "Soft fabric that can be traded for gold", icons[4], 300,
            new Dictionary<string, int> {
                {"BaseCost", 20},
                {"QuantitySoldIn", 10}
            }, false),
            new Item(5, "Spices", "Things that make other things tase good", icons[5], 300,
            new Dictionary<string, int> {
                {"BaseCost", 30},
                {"QuantitySoldIn", 10}
            }, false),
            new Item(6, "Treasure Map", "A graphical description of the best route to take to get to some treasure", icons[6], 1, 
            new Dictionary<string, int> {
                {"MapIndex", 0 }
            }, false),
        };
    }
}
