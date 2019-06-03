using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimator : MonoBehaviour
{
    public PathMovementHandler pmh;
    public bool isRightSideVar;
    Animator animator;
    int isRightSide = Animator.StringToHash("isRightSide");
    int isIdle = Animator.StringToHash("isIdle");
    int isWalking = Animator.StringToHash("isWalking");
    int isJumping = Animator.StringToHash("isJumping");
    //public bool walking;
    //public bool jumping;
    // Start is called before the first frame update
    void Start()
    {
        pmh = GetComponentInParent<PathMovementHandler>();
        animator = GetComponent<Animator>();
        if (isRightSideVar)
        {
            animator.SetBool(isRightSide, true);

        }
        else
        {
            animator.SetBool(isRightSide, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
    //    walking = pmh.walking;
        //jumping = pmh.jumping;
       // pmh = GetComponentInParent<PathMovementHandler>();
        //animator = GetComponent<Animator>();
       // if (pmh.destinationFound)
      //  { else if (pmh.walking)
        if (pmh.jumping && !pmh.landed)
           
            {
                clearBools();
                animator.SetBool(isJumping, true);
            }
        else if (pmh.landed && pmh.walking)
        {
                clearBools();
                animator.SetBool(isWalking, true);
            }

        else
        {
            clearBools();
            animator.SetBool(isIdle, true);
        }
    }

    void clearBools()
    {
        animator.SetBool(isIdle, false);
        animator.SetBool(isJumping, false);
        animator.SetBool(isWalking, false);
    }
}
