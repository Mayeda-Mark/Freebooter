using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB
{
    public List<Item> items = new List<Item>();
    void BuildDb() {
        items = new List<Item>{
            new Item(0, "Cannon Balls", "Big balls of metal that you can hurl at other boats", 
            new Dictionary<string, int> {
                {"Cost", 20}
            })
        };
    }
}
