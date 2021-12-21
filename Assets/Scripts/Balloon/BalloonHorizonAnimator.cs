using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonHorizonAnimator : MonoBehaviour
{

	public float speed;
	public Vector2 origin;
	float elapsedTime;
    BackgroundCameraFocus bcf;
    Vector2 distance;
    // Start is called before the first frame update
    void Start()
    {
        bcf = GetComponent<BackgroundCameraFocus>();
        //origin = new Vector2(transform.position.x, transform.position.y);
        elapsedTime = 0f;
        //distance = new Vector2(bcf.transform.position.x - transform.position.x, bcf.transform.position.y - transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
         //distance = new Vector2(bcf.transform.position.x - transform.position.x, bcf.transform.position.y - transform.position.y);
        //origin = new Vector2(origin.x, transform.position.y);
        origin = new Vector2(bcf.transform.position.x, transform.position.y);
    	elapsedTime += Time.deltaTime;
        transform.position = new Vector3(origin.x  + (elapsedTime * speed * transform.localScale.x), origin.y, transform.position.z);
        checkDestroy();
    }

    void checkDestroy() 
    {
        
    	if (transform.position.x > bcf.transform.position.x + 11f) {
    		Destroy(gameObject);
    	}
    }
}
