using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAnimator : MonoBehaviour
{
	Animator animator;
	int isWalk = Animator.StringToHash("Walk");
	int isJump = Animator.StringToHash("Jump");


    // Start is called before the first frame update
    void Start()
    {
       animator = GetComponent<Animator>();
       clearBools(); 
    }

    // Update is called once per frame
    void Update()
    {
        //registerBools();
    }

    public void clearBools()
    {
    	animator.SetBool(isWalk, false);
    	animator.SetBool(isJump, false);
    }

    public void registerBools(string whichBool)
    {
    	clearBools();
        switch (whichBool) 
        {
            case "WALKING":
                animator.SetBool(isWalk, true);
            break;
            case "JUMPING":
                animator.SetBool(isJump, true);
            break;
        }


    }
}
