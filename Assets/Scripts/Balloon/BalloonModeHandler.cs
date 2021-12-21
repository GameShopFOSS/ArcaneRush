using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonModeHandler : MonoBehaviour
{
	public enum BalloonMode {
		HORIZON,
		FLOATING,
		SPAWNING,
		LEAVING,
		NEXT
	}

	public BalloonMode mode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateBalloonMode();
    }

    void updateBalloonMode() {

    	BalloonFloatingAnimator bfa = GetComponent<BalloonFloatingAnimator>();
    	BalloonHorizonAnimator bha = GetComponent<BalloonHorizonAnimator>();

    	switch (mode){
    		case BalloonMode.HORIZON:
    			bfa.enabled = false;
    			bha.enabled = true;
    		break;
    		case BalloonMode.FLOATING:
    			bfa.enabled = true;
    			bha.enabled = false;
    		break;

    	}
    }

}
