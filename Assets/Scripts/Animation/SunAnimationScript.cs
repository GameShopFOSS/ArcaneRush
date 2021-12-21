using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunAnimationScript : MonoBehaviour
{
	public GameObject sunHardYellow;
	public GameObject sunLightYellow;
	public GameObject sunRayHardYellow;
	public GameObject sunRayLightYellow;
	public GameObject[] hardRays;
	public GameObject[] lightRays;
	public SpriteRenderer sHYsr;
	public Vector2 origin; //(.1,.06)
	float sHYelapsedTime;
	float sLYelapsedTime;
	public float opacity;
	public float scale;
	public float hardRaysAngle;
	float hardRaysElapsedTime;
	float hardRaysOscElapsedTime;
	public float hardRaysOscDistance;
	float lightRaysElapsedTime;
	public float lightRaysAngle;
    // Start is called before the first frame update
    void Start()
    {
    	sHYsr = sunHardYellow.GetComponent<SpriteRenderer>();
    	sHYelapsedTime = 0f;
    	sLYelapsedTime = 0f;
    	scale = 1f;
    	hardRaysAngle = 0f;
    	hardRaysOscElapsedTime = 0f;
    	hardRaysOscDistance = 0f;
    	lightRaysElapsedTime = 0f;
    	lightRaysAngle = 0f;
    	hardRays = new GameObject[12];
    	lightRays = new GameObject [24];
    	origin = new Vector2(transform.position.x + 3.141986f, transform.position.y - 0.3660932f);
        //origin = new Vector2(sunHardYellow.transform.position.x, sunHardYellow.transform.position.y);
    	generateSunRays();
       //Generate SunRays
    }

    // Update is called once per frame
    void Update()
    {
      origin = new Vector2(transform.position.x + 3.141986f, transform.position.y - 0.3660932f);
      //origin = new Vector2(sunHardYellow.transform.position.x, sunHardYellow.transform.position.y);
        
      sHYelapsedTime += Time.deltaTime;
      sLYelapsedTime += Time.deltaTime;
      hardRaysElapsedTime += Time.deltaTime;
      hardRaysOscElapsedTime += Time.deltaTime;
      lightRaysElapsedTime += Time.deltaTime;
      //makeSunGlow();
      moveSunRays();
      makeSunExpand();
      opacity = sHYsr.color.a;

    }


    void generateSunRays(){
    	for (int i = 0; i < 12; i++){
    		hardRays[i] = (GameObject) GameObject.Instantiate(sunRayHardYellow, new Vector3(origin.x, origin.y , transform.position.z), Quaternion.identity);
            hardRays[i].AddComponent<BackgroundCameraFocus>();
            hardRays[i].GetComponent<BackgroundCameraFocus>().player = sunHardYellow;
    		float angle = i * 30f;
    		hardRays[i].transform.eulerAngles = new Vector3(hardRays[i].transform.eulerAngles.x, hardRays[i].transform.eulerAngles.y, angle - 90f);
    		hardRays[i].transform.position = new Vector3(hardRays[i].transform.position.x + Mathf.Cos((angle * Mathf.PI)/180) * 2, hardRays[i].transform.position.y + Mathf.Sin((angle * Mathf.PI)/180) * 2, hardRays[i].transform.position.z);
    	}

    	for (int i = 0; i < 24; i++){
    		lightRays[i] = (GameObject) GameObject.Instantiate(sunRayLightYellow, new Vector3(origin.x, origin.y , transform.position.z), Quaternion.identity);
            lightRays[i].AddComponent<BackgroundCameraFocus>();
            lightRays[i].GetComponent<BackgroundCameraFocus>().player = sunHardYellow;
    		float angle = i * 15f;
    		lightRays[i].transform.eulerAngles = new Vector3(lightRays[i].transform.eulerAngles.x, lightRays[i].transform.eulerAngles.y, angle - 90f);
    		lightRays[i].transform.position = new Vector3(lightRays[i].transform.position.x + Mathf.Cos((angle * Mathf.PI)/180) * 2, lightRays[i].transform.position.y + Mathf.Sin((angle * Mathf.PI)/180) * 2, lightRays[i].transform.position.z);
    	}



    }

    void moveSunRays() {
    	//Orbiting movement as well as up and down movement;
    	//even boolean;
    	//Degrees per second... 45
    	hardRaysAngle = hardRaysElapsedTime * 6f;
    	if (hardRaysAngle >= 360f) {
    		hardRaysAngle = 0f;
    		hardRaysElapsedTime = 0f;
    	}
    	if (hardRaysOscElapsedTime >= 2) {
    		hardRaysOscElapsedTime = 0f;
    	} 
    	else if (hardRaysOscElapsedTime < .5f) {
    		hardRaysOscDistance = hardRaysOscElapsedTime;
    	}
    	else if (hardRaysOscElapsedTime >= 1.5f) {
    		hardRaysOscDistance = (-2f + hardRaysOscElapsedTime);
    	}
    	else if (hardRaysOscElapsedTime >= .5f) {
    		hardRaysOscDistance = 1f - hardRaysOscElapsedTime;
    	}
    	// else if (hardRaysOscElapsedTime >= 1) {
    	// 	hardRaysOscDistance = -(hardRaysOscElapsedTime - 1.5f);
    	// }
    	lightRaysAngle = -(lightRaysElapsedTime * 3f);
    	if (lightRaysAngle <= -360f){
    		lightRaysAngle = 0f;
    		lightRaysElapsedTime = 0f;
    	}
    	
    	bool even = true;
    	for (int i = 0; i < 12; i++){
    		float angle = (i * 30f) + hardRaysAngle;

    		float dist = 2f;// = 2f;
    		if (even) {
    			dist = 2f + hardRaysOscDistance;

    		} 
    		else if (!even) {
    			dist = 2f - hardRaysOscDistance;

    		}
    		hardRays[i].transform.eulerAngles = new Vector3(hardRays[i].transform.eulerAngles.x, hardRays[i].transform.eulerAngles.y, angle - 90f);
    		hardRays[i].transform.position = new Vector3(origin.x + Mathf.Cos((angle * Mathf.PI)/180) * dist, origin.y + Mathf.Sin((angle * Mathf.PI)/180) * dist, hardRays[i].transform.position.z);
    	
    		even = !even;
    	}

    	for (int i = 0; i < 24; i++){
    		float angle = (i * 15f) + lightRaysAngle;
    		lightRays[i].transform.eulerAngles = new Vector3(lightRays[i].transform.eulerAngles.x, lightRays[i].transform.eulerAngles.y, angle - 90f);
    		lightRays[i].transform.position = new Vector3(origin.x + Mathf.Cos((angle * Mathf.PI)/180) * 2, origin.y + Mathf.Sin((angle * Mathf.PI)/180) * 2, lightRays[i].transform.position.z);
    
    	}

    }

    void makeSunGlow(){
    	if (sHYelapsedTime > 5) {
    		sHYelapsedTime = 0f;
    	}

    	else if (sHYelapsedTime < 2.5f) {
    		sHYsr.color =  new Color (1f,1f,1f,((sHYelapsedTime * 100) + ((sHYelapsedTime / 5) * 10))); 

    	}

    	else if (sHYelapsedTime > 2.5f) {
	
			sHYsr.color =  new Color (1f,1f,1f, ((255 * 2) - ((sHYelapsedTime * 100) + ((sHYelapsedTime / 5) * 10)))); 
    	}


    }

    void makeSunExpand() {
    	if (sLYelapsedTime > 2) {
    		sLYelapsedTime = 0;
    	}

    	else if (sLYelapsedTime < 1) {
    		scale = 1f + (sLYelapsedTime / 2);

    	}

    	else if (sLYelapsedTime > 1) {
    		scale = 3 - (1f + (sLYelapsedTime / 2));
    	}

    	sunLightYellow.transform.localScale = new Vector3(scale, scale, 1f);
    }


}
