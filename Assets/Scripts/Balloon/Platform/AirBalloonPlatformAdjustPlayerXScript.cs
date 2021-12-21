using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBalloonPlatformAdjustPlayerXScript : MonoBehaviour
{
	public GameObject player;
	public float offsetX;
	public Vector2 lastPosition;
	public Vector2 positionChange;
    public Rigidbody2D playerRb;
    // Start is called before the first frame update
    void Start()
    {
        lastPosition = new Vector2(transform.position.x, transform.position.y);
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    	
    }

    void FixedUpdate() 
    {
    	positionChange = new Vector2(transform.position.x - lastPosition.x, transform.position.y - lastPosition.y);
        if ((player.transform.position.y <= transform.position.y + 1.7f && player.transform.position.y >= transform.position.y + 1.5f) && (player.transform.position.x < transform.position.x + offsetX && player.transform.position.x > transform.position.x - offsetX)) 
        {
         
                player.transform.SetParent(transform);
        		//playerRb.velocity = new Vector2(positionChange.x, positionChange.y);
                //player.transform.position = new Vector3(player.transform.position.x + positionChange.x * player.transform.localScale.x, player.transform.position.y + positionChange.y * player.transform.localScale.y, player.transform.position.z);
        	
        } 
        else {
            player.transform.SetParent(null);
        }
        lastPosition = new Vector2(transform.position.x, transform.position.y);
    }

    // void OnCollisionEnter2D (Collider2D col) 
    // {
    //     if (col.gameObject.name == "Rune") 
    //     {
    //         col.gameObject.transform.parent = transform;
    //     }
    // }

    // void OnCollisionExit2D (Collider2D col) 
    // {
    //     if (col.gameObject.name == "Rune")
    //     {
    //         col.gameObject.transform.parent = null;
    //     }
    // }
}
