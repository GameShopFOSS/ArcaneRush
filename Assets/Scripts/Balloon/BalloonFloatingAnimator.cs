using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonFloatingAnimator : MonoBehaviour
{
	public float floatingDistance;
	float elapsedTime;
	Vector2 origin;

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0f;
        origin = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        origin = new Vector2(transform.position.x, transform.position.y);
        elapsedTime += Time.deltaTime;
        makeFloat();
    }

    void makeFloat() {
    	if (elapsedTime >= 2f) {
    		elapsedTime = 0f;
    	}
    	else if (elapsedTime <= 1f) {
    		floatingDistance = transform.localScale.y * elapsedTime / 2f;
    		//floatingVelocity = f;

    	}
    	else {
    		floatingDistance = transform.localScale.y * (1f - (elapsedTime / 2f));
    	}

    	//if (even) {
    		transform.position = new Vector3(origin.x, origin.y + floatingDistance, transform.position.z);

    	//} 
    }
}
