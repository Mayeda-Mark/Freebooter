using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownController : MonoBehaviour
{
    public Canvas townCanvas;
    void Start() {
        townCanvas.enabled = false;
    }
    public void OpenMenu() {
        townCanvas.enabled = true;
        Time.timeScale = 0f;
    }
    public void CloseMenu() {
        townCanvas.enabled = false;
        Time.timeScale = 1.0f;
    }
    public void RepairShip() {
        FindObjectOfType<PlayerShipController>().GetComponent<Health>().ResetHealth();
    }
}
