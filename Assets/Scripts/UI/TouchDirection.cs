using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDirection : MonoBehaviour
{
	public Vector2 distance;
	public float lifeSpan;
	//public float mag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifeSpan += Time.deltaTime;
        //calculateMagnitude();
    }

    public float calculateMagnitude() 
    {
    	return Mathf.Sqrt((distance.x * distance.x) + (distance.y * distance.y));
    	
    }
}
