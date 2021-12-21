using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonMakeShadowWithSun : MonoBehaviour
{
	SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
    	//Debug.Log("Balloon collided with " + col.name);
    	if (col.name == "Sun_Light_Yellow" || col.name == "Sun_Hard_Yellow") 
    	{
    		sr.color = new Color(0f,0f,0f,1f);
    	}
    }

    void OnTriggerExit2D(Collider2D col) 
    {
    	if (col.name == "Sun_Light_Yellow" || col.name == "Sun_Hard_Yellow") 
    	{
    		sr.color = new Color(1f,1f,1f,1f);
    	}
    }
}
