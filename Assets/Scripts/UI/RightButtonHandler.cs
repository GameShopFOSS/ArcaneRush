using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightButtonHandler : MonoBehaviour
{

	public GameObject player;
	public GameObject cursor;
	public CursorCoordinates cc;
	public Rigidbody2D rb;
	public float maxMovementSpeed;
	float releaseElapsedTime;
	float clickElapsedTime;
	//int checkPoint;
    // Start is called before the first frame update
    void Start()
    {
        cc = cursor.GetComponent<CursorCoordinates>();
        rb = player.GetComponent<Rigidbody2D>();
        releaseElapsedTime = 0f;
        clickElapsedTime = 0f;
        //checkPoint = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            cc.putNewCoordinates();
            clickElapsedTime += Time.deltaTime;
            onClick();
        }

        if (!Input.GetMouseButton(0))  {
        	releaseElapsedTime += Time.deltaTime;
        	onRelease();
        }
    }

    void onClick() {

    	if (cc.cursorCoordinates.x > transform.position.x - .45f && cc.cursorCoordinates.x < transform.position.x + .45f){
    		if (cc.cursorCoordinates.y > transform.position.y - .495f && cc.cursorCoordinates.y < transform.position.y + .495f){
    			//rb.velocity = new Vector2(5f, rb.velocity.y);
    			//rb.AddForce(transform.right * 167f);
    			rb.velocity = new Vector2(rb.velocity.x + (clickElapsedTime * maxMovementSpeed * 2f), rb.velocity.y);
    			player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, 0, player.transform.eulerAngles.z);
    			if (rb.velocity.x > maxMovementSpeed) {
    				rb.velocity = new Vector2(maxMovementSpeed, rb.velocity.y);
    			}
    		}
    	}
    }

    void onRelease(){
    	clickElapsedTime = 0f;
    	if (rb.velocity.x > 0) {
    		//rb.AddForce(transform.right * -167f);
    		//if (releaseElapsedTime > checkPoint){
    			//checkPoint++;
    			if (rb.velocity.x - (releaseElapsedTime * maxMovementSpeed * 2f) > 0){
					rb.velocity = new Vector2(rb.velocity.x - (releaseElapsedTime * maxMovementSpeed * 2f),rb.velocity.y);
    		
    			} else {
    				rb.velocity = new Vector2(0, rb.velocity.y);
    			}
			
    		//}
    		
    	} else {
    		releaseElapsedTime = 0f;
    		//checkPoint = 1;
    	}
    	
    }
}
