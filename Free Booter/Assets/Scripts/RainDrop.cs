
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainDrop : MonoBehaviour
{
    public int height= 40;
    [SerializeField] Color rainColor;
    [SerializeField] float rainWidth = 0.1f;
    GameObject stormSystem;
    public float length = 2; 
    float systemWidth, systemHeight;
    Transform lineCoords;
    StormArea parent;
    public Material rainMaterial;
    LineRenderer lr;
    //public Shader lineShader;
    //Transform vector;
    //RectTransform rt;
    void Start() {
        parent = GetComponentInParent<StormArea>();
        //rt = (RectTransform)stormSystem.transform;
        systemWidth = parent.GetWidth();
        systemHeight = parent.GetHeight();
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update() {
        DrawRain();
        height -= 1;
        if(height <=0) {
            Destroy(gameObject);
            //Create a splash
        }
    }
    //private void MoveRain() {
    //}
    private void DrawRain(/*Vector3 startpos, Vector3 endPos*/)
    {
        lineCoords = this.transform;
        lineCoords.position = new Vector3((this.transform.position.x - (this.transform.position.x + systemWidth / 2)) / (systemWidth / 2), this.transform.position.y - (this.transform.position.y + systemHeight / 2) / (systemHeight / 2), 0);
        //GameObject myLine = new GameObject();
        Vector3 startPos = new Vector3(this.transform.position.x - (lineCoords.position.x + systemWidth / 2) / (systemWidth / 2), (this.transform.position.y + lineCoords.position.y * Mathf.Pow(height, 2)));
        Vector3 endPos = new Vector3(this.transform.position.x + lineCoords.position.x * Mathf.Pow(height + length, 1), this.transform.position.y + lineCoords.position.y * Mathf.Pow(height + length, 1));
        //x+vectorx*sqr(height+length), y+vectory*sqr(height+length), 2);
        //myLine.transform.position = startPos;
        /*myLine.*///AddComponent<LineRenderer>();
        /*LineRenderer*/ //lr = /*myLine.*/GetComponent<LineRenderer>();
        lr.material = rainMaterial;//new Material(lineShader/*.Find("Particles/Alpha Blended Premultiply")*/);
        lr.SetColors(rainColor, rainColor);
        lr.SetWidth(rainWidth, rainWidth);
        //lr.sortingLayerName = "Weather";
        lr.SetPosition(0, /*startPos*/ new Vector3(0, 1, 0));
        lr.SetPosition(1, /*endPos*/ new Vector3(0, 2, 0));
    }
}
/*
 1. Create bounds of rain system and instantiate raindrops within those bounds on each update at random locations
2. Create raindrop object to instantiate
	a. Set rain to move and destroy itself after a set period
	b. instantiate a splash on destroy
3. Set color and alpha
4. Set length variable and x,y speeds
	a. vectorx = (x-(view_xview+view_wview/2))/(view_wview/2)
	b. vectory = (y-(view_yview+view_hview/2))/(view_hview/2)
	c. length = 2
5. Draw line
	a. draw_line_width(x+vectorx*sqr(height), y+vectory*sqr(height), x+vectorx*sqr(height+length), y+vectory*sqr(height+length), 2);
    */
