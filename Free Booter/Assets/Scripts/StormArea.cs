using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormArea : MonoBehaviour
{
    public float areaWidthHeight = 200;
    public int intensity = 40;
    public GameObject rainDropPrefab;
    public GameObject parent;
    float width, height, xPos, yPos;
    //Mesh mesh;
    //public RectTransform rt;
    // Start is called before the first frame update
    void Start() {
        width = parent.GetComponent<Collider2D>().bounds.size.x;
        height = parent.GetComponent<Collider2D>().bounds.size.y;
        xPos = transform.position.x;
        yPos = transform.position.y;
        //Transform parentTransform = parent.transform;
        //rt = (RectTransform)this.transform;
    }

    // Update is called once per frame
    void Update() {
        CreateRainDrop();
    }
    private void CreateRainDrop() {
        for(int i = 0; i < intensity; i++){
            Transform raindropTransform = this.transform;
            /*Vector3 rainDropPos*/raindropTransform.position = new Vector3(/*0, 0*/xPos - areaWidthHeight + UnityEngine.Random.Range(0f, width + areaWidthHeight * 2), (yPos - areaWidthHeight + UnityEngine.Random.Range(0f, height + areaWidthHeight * 2)), 0);
            print(raindropTransform.position);
            GameObject rainDrop = Instantiate(rainDropPrefab, raindropTransform) as GameObject;
        }
    }

    public float GetWidth() { return width;  }
    public float GetHeight() { return height; }
}
