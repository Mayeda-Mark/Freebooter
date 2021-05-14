using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item-Abilities", menuName = "Item-Abilities")]
public class Abilities : Equipment
{
    public int manaCost = default;
    //[HideInInspector]public new string panelName = "Abilities";
}
