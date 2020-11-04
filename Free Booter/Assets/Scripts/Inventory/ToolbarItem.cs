using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ToolbarItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    //I WANT THE TOOLBAR ITEMS TO BECOME GOLD TO INDICATE THAT THEY ARE EQUIPPED, BLACK WHEN EMPTY, MUTED WHEN NOT EQUIPPED, BRIGHTENED TO NORMAL WHEN MOUSED OVER AND EQUIP THE ITEM WHEN CLICKED
    public Item item;
    private Image spriteImage;
    PlayerShipController player;
    //Text quantityText;
    //public Text keyboardShortcut;
    Inventory inventory;
    private void Awake()
    {
        player = FindObjectOfType<PlayerShipController>();
        spriteImage = GetComponent<Image>();
        inventory = FindObjectOfType<Inventory>();
        UpdateItem(null);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateItem(Item item)
    {
        this.item = item;
        if (this.item != null)
        {
            spriteImage.color = Color.white;
            spriteImage.sprite = this.item.icon;
            int quantityValue = inventory.GetQuantities()[item.id][0];
            /*quantityText.color = Color.white;
            quantityText.text = quantityValue.ToString();*/
        }
        else
        {
            spriteImage.color = Color.clear;
            /*quantityText.color = Color.clear;*/
        }
    }
    #region Click
    public void OnPointerClick(PointerEventData eventData)
    {
        EquipItem(item);
    }

    public void EquipItem(Item item)
    {
        throw new NotImplementedException();
    }
    #endregion
    #region Enter
    public void OnPointerEnter(PointerEventData eventData1) { }
    #endregion
    #region Exit
    public void OnPointerExit(PointerEventData eventData2) { }
    #endregion
}
