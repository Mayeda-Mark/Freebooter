using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownController : MonoBehaviour
{
    public Canvas townCanvas;
    public GameObject townMenu;
    public GameObject merchantMenu;
    void Start() {
        townCanvas.enabled = false;
    }
    public void OpenMenu() {
        townCanvas.enabled = true;
        merchantMenu.gameObject.SetActive(false);
        Time.timeScale = 0f;
    }
    public void CloseMenu() {
        townCanvas.enabled = false;
        Time.timeScale = 1.0f;
    }
    public void OpenMerchant() {
        townMenu.gameObject.SetActive(false);
        merchantMenu.gameObject.SetActive(true);
    }
    public void CloseMerchant() {
        townMenu.gameObject.SetActive(true);
        merchantMenu.gameObject.SetActive(false);
    }
    public void RepairShip() {
        FindObjectOfType<PlayerShipController>().GetComponent<Health>().ResetHealth();
    }
}
