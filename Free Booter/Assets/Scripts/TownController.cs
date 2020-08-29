using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownController : MonoBehaviour
{
    public Canvas townCanvas;
    public GameObject townMenu;
    public GameObject merchantMenu;
    Inventory inventory;
    Health playerHealth;
    [SerializeField] int repairCost = 100;
    [SerializeField] Text alertText;
    [SerializeField] Text repairButtonText;
    void Start() {
        repairButtonText.text = "Repair Ship: " + repairCost.ToString() + " Gold";
        playerHealth = FindObjectOfType<PlayerShipController>().GetComponent<Health>();
        alertText.gameObject.SetActive(false);
        inventory = FindObjectOfType<PlayerShipController>().GetComponent<Inventory>();
        townCanvas.enabled = false;
    }
    public void OpenMenu() {
        townCanvas.enabled = true;
        merchantMenu.gameObject.SetActive(false);
        Time.timeScale = 0f;
    }
    public void CloseMenu() {
        Debug.Log("Called Close");
        townCanvas.enabled = false;
        Time.timeScale = 1.0f;
    }
    public void OpenMerchant() {
        alertText.gameObject.SetActive(false);
        townMenu.gameObject.SetActive(false);
        merchantMenu.gameObject.SetActive(true);
    }
    public void CloseMerchant() {
        townMenu.gameObject.SetActive(true);
        merchantMenu.gameObject.SetActive(false);
    }
    public void RepairShip() {
        if(playerHealth.isHealthFull()) {
            alertText.gameObject.SetActive(true);
            alertText.text = "Ship health already full";
        } else if(inventory.GetTotalGold() >= repairCost) {
            alertText.gameObject.SetActive(false);
            playerHealth.ResetHealth();
            inventory.RemoveGold(repairCost);
        } else {
            alertText.gameObject.SetActive(true);
            alertText.text = "Not enough gold";
        }
    }
}
