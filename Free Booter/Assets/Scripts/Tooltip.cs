using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    private Text tooltipText;
    void Start() {
        tooltipText = GetComponentInChildren<Text>();
        tooltipText.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
    public void GenerateTooltip (Item item) {
        string statText = "";
        if( item.stats.Count > 0) {
            foreach(var stat in item.stats) {
                string statName = stat.Key.ToString();
                statText += statName += ": " + stat.Value.ToString() + "\n";
            }
        }
        string tooltip = string.Format("{0}\n{1}\n\n{2}", item.itemName, item.description, statText);
        tooltipText.text = tooltip;
        gameObject.SetActive(true);
        tooltipText.gameObject.SetActive(true);
    }
}
