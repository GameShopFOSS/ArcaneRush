using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMovementMapper : MonoBehaviour
{
    public bool hasJumped;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            jump();
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow)){
            hasJumped = false;
        }
    }

    void jump() {
        if (!hasJumped) {
            rb.velocity = new Vector2(2,10);//rb.velocity.Set(rb.velocity.x, 100);
            hasJumped = true;
        }
    }
}
