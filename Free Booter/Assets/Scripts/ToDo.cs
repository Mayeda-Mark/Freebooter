﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToDo : MonoBehaviour
{
    /* TODO:
    NOTE -- MAPS WILL NEED TO JUST REFERENCE A DIFFERENT GAME OBJECT WHICH WILL HOLD ALL OF THE UNLOCK INFORMATION
    REFACTOR MUSIC/SOUND MANAGERS -- CHECK
    SYNC UP TOOLBAR AND INVENTORY UIS SO ITEMS CAN BE ADDED/REMOVED FROM TOOLBAR -- MIGHT NOT DO THIS FOR OVERWORLD
    REFACTOR SPAWNERS TO POOLS
    FIX SAILS BUG (SAIL DAMAGE AFFECTS PLAYER MOVE SPEED WAY TOO MUCH)
    IMPLIMENT FOG OF WAR
        EXPAND MAP -- CHECK
        IMPLIMENT FOG OF WAR IN EXTRA AREAS -- CHECK
        SUPRESS SHIP SPAWNING IN NEW AREAS UNTIL THEY ARE UNLOCKED
        UNLOCK EXPANDED AREAS WITH MAPS
    TREASURE MAPS
        ADD TO ITEMDB -- CHECK
        ADD UNIQUE TO ITEM (UNIQUE ITEMS SHOULDN'T STACK IN INVENTORY) -- CHECK
        REFACTOR ITEM ICONS SO THAT THE SAME ICON CAN BE USED FOR MULTIPLE ITEMS -- CHECK
        ADD RARITY TO LOOT DROPS -- CHECK
        CREATE HIDDEN TREASURE SPOT -- CHECK
        ADD HIDDEN "HIDDEN TREASURE" SPOTS THROUGHOUT THE MAP
        UNLOCK SPECIFIC HIDDEN TREASURE SPOTS WHEN PLAYER HOLDS A MAP -- CHECK
        RANDOMIZE MAP LOCATIONS
        ADD TREASURE MAPS TO APPEAR RANDOMLY IN SHOPS
    FIX MAIN MENU BUTTONS SO THAT THEY DON'T AUTOMATICALLY TRIGGER ON LOAD

    BUG!!!
    SELL MENU WOULDN'T COME UP AFTER RETURNING TO TOWN
    KeyNotFoundException: The given key was not present in the dictionary.
    System.Collections.Generic.Dictionary`2[TKey,TValue].get_Item (TKey key) (at <fb001e01371b4adca20013e0ac763896>:0)
    MerchantMenu.UpdateSellTextBox () (at Assets/Scripts/MerchantMenu.cs:257)
    MerchantMenu.SetUpForSale () (at Assets/Scripts/MerchantMenu.cs:234)
    MerchantMenu.SellButtonClick () (at Assets/Scripts/MerchantMenu.cs:201)
    UnityEngine.Events.InvokableCall.Invoke () (at <3dc54541a2574ac7826a004a212a4332>:0)
    UnityEngine.Events.UnityEvent.Invoke () (at <3dc54541a2574ac7826a004a212a4332>:0)
    UnityEngine.UI.Button.Press () (at C:/Program Files/Unity/Hub/Editor/2019.4.1f1/Editor/Data/Resources/PackageManager/BuiltInPackages/com.unity.ugui/Runtime/UI/Core/Button.cs:68)

    SPAWNER IS ONLY RESPAWNING SHIPS THAT I HAVE KILLED BEFORE.
    */
}
