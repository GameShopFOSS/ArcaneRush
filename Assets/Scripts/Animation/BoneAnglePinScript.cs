using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneAnglePinScript : MonoBehaviour
{
	public GameObject parentBone;
	public float parentBoneStandardAngle;
	public float localStandardAngle;
	public float angleDifference;
	public float angleUpperBound;
	public float angleLowerBound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        setVars();
    }

    void setVars() 
    {
    	parentBoneStandardAngle = calculateStandardAngle(parentBone);
    	localStandardAngle = calculateStandardAngle(gameObject);
    	angleDifference = parentBoneStandardAngle - localStandardAngle;


    }

    float calculateStandardAngle(GameObject go) 
    {
    	float angle = go.transform.eulerAngles.z;
    	if (angle >= 360f) 
    	{

    		while (angle >= 360f) 
    		{
    			angle -= 360f;
    		}

    	}
    	else if (angle <= -360f) 
    	{
    		while (angle <= 360f) 
    		{
    			angle += 360f;
    		}
    		if (angle < 0) 
    		{
    			angle += 360f;
    		}
    	}
    	return angle;
    }
}
