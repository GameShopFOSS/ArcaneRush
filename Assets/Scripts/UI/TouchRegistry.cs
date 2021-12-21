using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchRegistry : MonoBehaviour
{
	public GameObject player;
	public GameObject cursor;
	public CursorCoordinates cc;
	public Rigidbody2D rb;
	public float maxMovementSpeed;
	float releaseElapsedTime;
	float clickElapsedTime;
	float spawnNewSampleTime;
	public GameObject sample;
	public List<GameObject> samples;
	public GameObject direction;
	public List <GameObject> allDirections;
	public GameObject shortestDirection;
    public PlayerMovementAnimator pma;
	//Make arraylist for child objects
	//Iterate through array list to detect whether touch is intended
	//for button, movement, or target

    // Start is called before the first frame update

    void Start()
    {
        cc = cursor.GetComponent<CursorCoordinates>();
        rb = player.GetComponent<Rigidbody2D>();
        pma = player.GetComponent<PlayerMovementAnimator>();
        samples = new List<GameObject>();
        allDirections = new List<GameObject>();
        //samples.Add((GameObject) GameObject.Instantiate(sample, new Vector3(transform.position.x, transform.position.y , transform.position.z), Quaternion.identity));
        releaseElapsedTime = 0f;
        clickElapsedTime = 0f;
        spawnNewSampleTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetMouseButton(0))
        {
            cc.putNewCoordinates();
            clickElapsedTime += Time.deltaTime;
            onClick();
            spawnNewSampleTime += Time.deltaTime;
            if (spawnNewSampleTime > .05f) 
            {
            	spawnNewSampleTime = 0f;
            }
            
        }

        if (!Input.GetMouseButton(0))  {
        	onRelease();
        	releaseElapsedTime += Time.deltaTime;
        	
        }

        if (clickElapsedTime > 0f) 
        {
        	if (!isUITouch() && !isGesture()) 
        	{
        		if (cc.cursorCoordinates.x > transform.position.x) 
        		{
        			doRightMovement();
        		}

        		if (cc.cursorCoordinates.x < transform.position.x) 
        		{
        			doLeftMovement();
        		}
        	}
        }

        if (releaseElapsedTime > 0f) 
        {
            //pma.clearBools();
        	slowDown();
        }

        if (rb.velocity.x == 0 && rb.velocity.y == 0)
        {
            pma.clearBools();
        }
    }

    bool isUITouch() 
    {
    	return false;
    }

    bool isGesture() 
    {
    	return false;
    }
    
    void onClick() 
    {
    	releaseElapsedTime = 0f;
    	if (spawnNewSampleTime == 0f)
    	{
    		GameObject sam = (GameObject) GameObject.Instantiate(sample, new Vector3(transform.position.x, transform.position.y , transform.position.z), Quaternion.identity);
    		sam.GetComponent<TouchSample>().coordinate = new Vector2(cc.cursorCoordinates.x - transform.position.x, cc.cursorCoordinates.y - transform.position.y);
    		samples.Add(sam);
       
    	}
    }

    void onRelease() 
    {
        //pma.isWalkingState = false;
    	clickElapsedTime = 0f;
    	if (releaseElapsedTime == 0f){
    		generateDirections();
    		assessDirections();
    		samples.Clear();
    		allDirections.Clear();
    	}
        
        //pma.isJumpingState = false;
    	
    	//Need to use both direction and lifespan (or releasedElapsedTime), maybe to get a derivative
    	//Intended gesture calculations will be done later, for now, we will do simple gesture for jump,
    	//taking the shortestDirection variable.
    	
    }

    void generateDirections()
    {
    	if (samples.Count > 1)
    	{
    		for (int i = 0; i < samples.Count - 1; i++)
    		{
    			GameObject dir = (GameObject) GameObject.Instantiate(direction, new Vector3(transform.position.x, transform.position.y , transform.position.z), Quaternion.identity);
    			dir.GetComponent<TouchDirection>().distance = new Vector2(samples[i+1].GetComponent<TouchSample>().coordinate.x - samples[i].GetComponent<TouchSample>().coordinate.x, samples[i+1].GetComponent<TouchSample>().coordinate.y - samples[i].GetComponent<TouchSample>().coordinate.y);
    			allDirections.Add(dir);
    		}	
    			shortestDirection = (GameObject) GameObject.Instantiate(direction, new Vector3(transform.position.x, transform.position.y , transform.position.z), Quaternion.identity);
    			shortestDirection.GetComponent<TouchDirection>().distance = new Vector2(samples[samples.Count - 1].GetComponent<TouchSample>().coordinate.x - samples[0].GetComponent<TouchSample>().coordinate.x, samples[samples.Count - 1].GetComponent<TouchSample>().coordinate.y - samples[0].GetComponent<TouchSample>().coordinate.y);		
    			Debug.Log("Distance: " + shortestDirection.GetComponent<TouchDirection>().distance);
    	}

    }

    void assessDirections()
    {
    	if (samples.Count > 1) 
    	{
    		//TouchDirection td = shortestDirection.GetComponent<TouchDirection>();

    		float mag = shortestDirection.GetComponent<TouchDirection>().calculateMagnitude();
    		Debug.Log("Magnitude: " + mag);
    		if (mag > .5f) 
    		{
    			doJump();
    		}
    	}
    }

    void doRightMovement() 
    {
       // pma.isWalkingState = true;
        pma.registerBools("WALKING");
    	rb.velocity = new Vector2(rb.velocity.x + (clickElapsedTime * maxMovementSpeed * 2f), rb.velocity.y);
    	player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, 0, player.transform.eulerAngles.z);
    	if (rb.velocity.x > maxMovementSpeed) {
    		rb.velocity = new Vector2(maxMovementSpeed, rb.velocity.y);
    	}
    }

    void doLeftMovement() 
    {
        //pma.isWalkingState = true;
        pma.registerBools("WALKING");
    	rb.velocity = new Vector2(rb.velocity.x - (clickElapsedTime * maxMovementSpeed * 2f), rb.velocity.y);
    	player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, 180, player.transform.eulerAngles.z);
    	if (rb.velocity.x < -maxMovementSpeed) {
    		rb.velocity = new Vector2(-maxMovementSpeed, rb.velocity.y);
    	}
    }

    void doJump()
    {

        pma.registerBools("JUMPING");
        //pma.isJumpingState = true;
    	Debug.Log("JUMPING");
        if (cc.cursorCoordinates.x > transform.position.x) 
        {
    	
            if (shortestDirection.GetComponent<TouchDirection>().distance.x > 0) 
    	   {

    		  rb.velocity = new Vector2(12f,7f);
              //rb.AddForce(new Vector2(100,100));  
    		  Debug.Log("JUMP RIGHT");
    	   }
        }
        if (cc.cursorCoordinates.x < transform.position.x) 
        {
            if (shortestDirection.GetComponent<TouchDirection>().distance.x < 0)
            {
                rb.velocity = new Vector2(-5f,7f);
                //rb.AddForce(new Vector2(-100,100));
                Debug.Log("JUMP LEFT");
            } 
        }
    	
    }

    void slowDown()
    {
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
    		
    	}
    }
    //doLeftMovement
    //doRightMovement
    //doJump

}
