using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterKeyToAnimation : MonoBehaviour
{

    public bool isRightSideVar;
    Animator animator;
    int isRightSide = Animator.StringToHash("isRightSide");
    int isIdle = Animator.StringToHash("isIdle");
    int isWalking = Animator.StringToHash("isWalking");
    int isJumping = Animator.StringToHash("isJumping");
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (isRightSideVar)
        {
            animator.SetBool(isRightSide, true);

        }
        else {
            animator.SetBool(isRightSide, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            clearBools();
            animator.SetBool(isWalking, true);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            clearBools();
            animator.SetBool(isJumping, true);
        }

        if (!Input.anyKey) {
            clearBools();
            animator.SetBool(isIdle, true);
        }
    }

    void clearBools() {
        animator.SetBool(isIdle, false);
        animator.SetBool(isJumping, false);
        animator.SetBool(isWalking, false);
    }
}
