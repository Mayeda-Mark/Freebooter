using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item-Equipment", menuName = "Item-Equipment")]
public class Equipment : Item
{
    public bool melee = default;
    public int sidescrollDamage, hullDamage, sailDamage, range = default;
    public float cooldown;
    public string rangedType, effector = default;
    //[HideInInspector] public new string panelName = "Equipment";
}