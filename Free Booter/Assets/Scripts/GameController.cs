using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Canvas instructionCanvas;
    // Start is called before the first frame update
    void Start()
    {
        instructionCanvas.enabled = true;
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartButton() {
        Time.timeScale = 1.0f;
        instructionCanvas.enabled = false;
    }
}
