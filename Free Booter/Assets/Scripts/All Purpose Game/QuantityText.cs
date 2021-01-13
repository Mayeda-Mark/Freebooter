using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuantityText : MonoBehaviour
{
    UIItem uIItem;
    Text quantityText;
    void Start()
    {
        uIItem = GetComponent<UIItem>();
        quantityText = GetComponent<Text>();
    }
    public void TurnOff() {
        quantityText.color = Color.clear;
    }
    public void UpdateText(int quantity) {
        quantityText.text = quantity.ToString();
        quantityText.color = Color.white;
    }
}
