using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBalloonPlatformAnimateScript : MonoBehaviour
{
	public bool even;
	public Vector2 floatingDistance;
	public Vector2 floatingMagnitude;
	//public float floatingVelocity;
	public Rigidbody2D rb;
	float elapsedTime;
	Vector2 origin;
	public enum AirBalloonPlatformMode{
		VERTICAL,
		HORIZONTAL,
		DIAGNOL,
		ORBITAL
	}

	public AirBalloonPlatformMode mode;
	
    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0f;
        origin = new Vector2(transform.position.x, transform.position.y);
       // rb = GetComponent<Rigidbody2D>();
        floatingDistance = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        
    }

    void FixedUpdate() {
    	makeFloat();
    }
    void makeFloat(){

    	if (elapsedTime >= 2f) {
    		elapsedTime = 0f;
    	}
    	else if (elapsedTime <= 1f) {
    		floatingDistance.y = floatingMagnitude.y * (transform.localScale.y * elapsedTime / 2f);
    		floatingDistance.x = floatingMagnitude.x * (transform.localScale.x * elapsedTime / 2f);
    		//floatingVelocity = f;
    		//rb.velocity = new Vector2(rb.velocity);
    		//floatingDistance = 1f;
    	}
    	else {
    		floatingDistance.y = floatingMagnitude.y * (transform.localScale.y * (1f - (elapsedTime / 2f)));
    		floatingDistance.x = floatingMagnitude.x * (transform.localScale.x * (1f - (elapsedTime / 2f)));
    		//floatingDistance = -1f;
    	}


    	if (even) {
    		transform.position = new Vector3(origin.x + floatingDistance.x, origin.y + floatingDistance.y, transform.position.z);
    		//rb.velocity = new Vector2(rb.velocity.x, floatingDistance);
    	} 
    	else if (!even) {
			transform.position = new Vector3(origin.x + floatingDistance.x, origin.y - floatingDistance.y, transform.position.z);
			//rb.velocity = new Vector2(rb.velocity.x, -floatingDistance);
    	}
    }

    // void OnCollisionEnter2D(Collision2D collision) {
    // 	collision.gameObject.transform.position = new Vector3(collision.gameObject.transform.position.x, transform.position.y + 1.32f , collision.gameObject.transform.position.z);
    // }
}
