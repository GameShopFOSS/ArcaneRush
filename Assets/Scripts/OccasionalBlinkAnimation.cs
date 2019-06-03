using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OccasionalBlinkAnimation : MonoBehaviour
{
    Animator animator;
    int isBlinking = Animator.StringToHash("Blink");
    bool _blinking;
    bool _initiated;
    float waitTime = -1f;
    float elapsedTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        _blinking = false;
        _initiated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_blinking)
        {
            elapsedTime = 0;
            waitTime = -1;
            animator.SetBool(isBlinking, true);
            _blinking = false;
            _initiated = true;
        }
        else if (_initiated)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= .98) {
                animator.SetBool(isBlinking, false);
                _initiated = false;
            }
        }
        else
        {
            if (waitTime < 0)
            {
                waitTime = Random.value * 10;
            }
            else
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= waitTime)
                {
                    _blinking = true;
                }
            }
        }
    
    }

    void blink()
    {

    }


}
