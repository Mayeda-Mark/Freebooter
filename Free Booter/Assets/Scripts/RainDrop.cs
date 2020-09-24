
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
    //Transform vector;
    RectTransform rt;
    void Start() {
        rt = (RectTransform)stormSystem.transform;
        systemWidth = rt.rect.width;
        systemHeight = rt.rect.height;
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
    private void MoveRain() {
        lineCoords = this.transform;
        lineCoords.position = new Vector3((this.transform.position.x - (this.transform.position.x + systemWidth / 2)) / (systemWidth / 2), this.transform.position.y - (this.transform.position.y + systemHeight / 2) / (systemHeight / 2), 0);
    }
    private void DrawRain(/*Vector3 startpos, Vector3 endPos*/) {
        GameObject myLine = new GameObject();
        myLine.transform.position = this.transform.position;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetColors(rainColor, rainColor);
        lr.SetWidth(rainWidth, rainWidth);
        lr.SetPosition(0, startpos);
        lr.SetPosition(1, endPos);
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
