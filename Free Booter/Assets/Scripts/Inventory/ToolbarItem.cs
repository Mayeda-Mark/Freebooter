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
    [SerializeField] Image parentImage = default;
    Color selectedItemColor = new Color();
    Color emptyItemColor = new Color();
    Color defaultColor = new Color();
    Color mouseOverColor = new Color();
    ToolbarUI toolbarUI;
    //Text quantityText;
    //public Text keyboardShortcut;
    Inventory inventory;
    bool equipped;
    private void Awake()
    {
        //parentImage = GetComponentInParent<Image>();
        ColorUtility.TryParseHtmlString("#FFFF00", out selectedItemColor);
        ColorUtility.TryParseHtmlString("#575757", out emptyItemColor);
        ColorUtility.TryParseHtmlString("#AB843A", out defaultColor);
        ColorUtility.TryParseHtmlString("#DDAA4A", out mouseOverColor);
        player = FindObjectOfType<PlayerShipController>();
        spriteImage = GetComponent<Image>();
        inventory = FindObjectOfType<Inventory>();
        UpdateItem(null);
        parentImage.color = defaultColor;
        toolbarUI = FindObjectOfType<ToolbarUI>();
    }
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }*/
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
        //ADD IF STATEMENT CHECKING FOR QUANTITY > 0
        parentImage.color = selectedItemColor;
        player.EquipItem(item.itemName);
        equipped = true;
        toolbarUI.UnequipAllButThis(item);
    }
    public void Unequip()
    {
        equipped = false;
        parentImage.color = default;
    }
    #endregion
    #region Enter
    public void OnPointerEnter(PointerEventData eventData1)
    {
        if (!equipped)
        {
            parentImage.color = mouseOverColor;
        }
    }
    #endregion
    #region Exit
    public void OnPointerExit(PointerEventData eventData2)
    {
        if (!equipped)
        {
            parentImage.color = defaultColor;
        }
    }
    #endregion
}
