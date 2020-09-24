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
    public RectTransform rt;
    // Start is called before the first frame update
    void Start() {
        //Transform parentTransform = parent.transform;
        rt = (RectTransform)this.transform;
    }

    // Update is called once per frame
    void Update() {
        CreateRainDrop();
    }
    private void CreateRainDrop() {
        for(int i = 0; i < intensity; i++){
            float xPos = transform.position.x;
            float yPos = transform.position.y;
            float width = rt.rect.width;
            float height = rt.rect.height;
            Transform raindropTransform = this.transform;
            /*Vector3 rainDropPos*/raindropTransform.position = new Vector3(xPos - areaWidthHeight + UnityEngine.Random.Range(0f, width + areaWidthHeight * 2), (yPos - areaWidthHeight + UnityEngine.Random.Range(0f, height + areaWidthHeight * 2)), 0);

            var rainDrop = Instantiate(rainDropPrefab, raindropTransform);
        }
    }
}
