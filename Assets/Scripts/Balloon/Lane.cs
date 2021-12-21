using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
	public Vector2 spawnPoint;
	public float elapsedTime;
	public float spawnFrequency;
	//public float laneSpeed;
	public GameObject balloon;
    // Start is called before the first frame update
    void Start()
    {
    	elapsedTime = 0f;
        spawn();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime+= Time.deltaTime;
        attemptSpawn();
    }

    void attemptSpawn() {
    	if (elapsedTime >= spawnFrequency) {
    		spawn();
    		elapsedTime = 0f;
    	}
    }

    void spawn(){
        BackgroundCameraFocus bcf = GetComponent<BackgroundCameraFocus>();
    	GameObject balloonSpawn = (GameObject) GameObject.Instantiate(balloon, new Vector3(bcf.transform.position.x - 11f, spawnPoint.y , transform.position.z), Quaternion.identity);
        balloonSpawn.AddComponent<BackgroundCameraFocus>();
         balloonSpawn.GetComponent<BackgroundCameraFocus>().player = gameObject;
		BalloonModeHandler bmode = balloonSpawn.GetComponent<BalloonModeHandler>();
		bmode.mode = BalloonModeHandler.BalloonMode.HORIZON; 
		BalloonHorizonAnimator bha = balloonSpawn.GetComponent<BalloonHorizonAnimator>();
		bha.speed = spawnFrequency;   		

    }
}
