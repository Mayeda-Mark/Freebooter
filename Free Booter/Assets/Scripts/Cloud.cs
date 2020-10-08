using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] Sprite[] cloudSprites;
    float cloudSpeed;
    StormArea parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<StormArea>();
        cloudSpeed = UnityEngine.Random.Range(parent.GetWindSpeed() - 0.25f, parent.GetWindSpeed() + 0.25f);
        int index = UnityEngine.Random.Range(0, cloudSprites.Length - 1);
        GetComponent<SpriteRenderer>().sprite = cloudSprites[index];
        //print(parent.GetWindSpeed());
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        //print(transform.position.y);
        transform.Translate(Vector2.up * cloudSpeed * Time.deltaTime);
        var position = transform.position;
        //print(parent.GetMaxBounds().y);
        if(position.x < parent.GetMinBounds().x || position.x > parent.GetMaxBounds().x ||
           position.y < parent.GetMinBounds().y || position.y > parent.GetMaxBounds().y)
        {
            //print("called!");
            Vector3 viewPos = parent.GetCamera().WorldToViewportPoint(position);
            //print(viewPos.y);
            if ((viewPos.x > 1.05 || viewPos.x < -0.05) || (viewPos.y > 1.05 || viewPos.y < -0.05))
            {
                //print("I'm out!");
                parent.RemoveCLoud();
                Destroy(gameObject);
            }
        }
    }
}
