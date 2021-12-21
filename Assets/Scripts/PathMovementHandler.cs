using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMovementHandler : MonoBehaviour
{
    //source square
    //destination square
    public Vector2 sourceSquare;
    public Vector2 destinationSquare;
    public Vector2 nextSquare;
    public bool nsSet;
    public bool psSet;
    public GameObject tilemap;
    public GameObject player;
    public GameObject cursor;
    public CursorCoordinates cc;
    public bool destinationFound;
    Rigidbody2D rb;
    public Vector2 jumpDistance;
    public Vector2 minJumpVelocity;
    //public float hypotesnuse;
    public Vector2 maxJumpVelocity;
    public Vector2 jumpVelocity;
    public bool walking;
    public Vector2 playerOffset;
    public bool jumping;
    public bool landed;
    public float xOffset;
    public float yOffsetTop;
    public float yOffsetBottom;
    public float xSquareSize;
    public float walkSpeed;
    public float maxJumpDistance;
    public float elapsedTime;
    public float ySquareSize;
    public float jumpTime;
    public bool walkFirst;
   // public Vector2 jumpAngles;
    // Start is called before the first frame update
    void Start()
    {
        //if (findSourceSquare())
        //{
        //    snap("SOURCE");
        //}
        
        elapsedTime = (float)0;
        //jumpMax = 12;
        cc = cursor.GetComponent<CursorCoordinates>();
        nsSet = false;
        psSet = false;
        rb = GetComponent<Rigidbody2D>();
        walking = false;
        landed = true;
        walkFirst = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
      
        if (Input.GetMouseButtonDown(0))
        {
            cc.putNewCoordinates();
           
           if (landed) { 
            if (findSourceSquare() && findDestinationSquare())
            {
               

                if (isContinuous())
                {
                    
                    Debug.Log("CONTINUOUS");
                    destinationFound = true;
                    walking = true;
                       
                       walkFirst = true;
                }
                else
                {
                    nsSet = false;
                        //nextsquare = new vector2();
                        if (isContinuousWithJump())
                        {
                            destinationFound = true;
                            walking = false;
                            jumping = true;
                            walkFirst = true;
                            Debug.Log("CONTINUOUS WITH JUMP");
                           
                        } 
                      
            }
                }
                else
                {
                    Debug.Log("SQUARE NOT FOUND");
                    return;

                }


            } //if jumping
        }
        if (destinationFound)
        {

            if (walkFirst)
            {
                if (walking)
                {
                    Debug.Log("WALKING");
                    if (nextSquare.x >= sourceSquare.x)
                    {

                        if (player.transform.position.x >= nextSquare.x)
                        {
                            rb.velocity = new Vector2(0, 0);
                            walking = false;
                            //   snap();

                        }
                        else
                        {
                           rb.velocity = new Vector2(calculateWalkSpeed(), 0);
                        }
                    }
                    else if (nextSquare.x <= sourceSquare.x)
                    {

                        if (player.transform.position.x <= nextSquare.x)
                        {
                            rb.velocity = new Vector2(0, 0);
                            walking = false;
                            //snap();
                        }
                        else
                        {
                             rb.velocity = new Vector2(calculateWalkSpeed(), 0);
                        }
                    }


                }
                else if (jumping && landed)

                {
                    Debug.Log("JUMPING/LANDED");
                      if (hasClearJumpPathNoCollision())
                    {
                        rb.velocity = jumpVelocity;
                         landed = false;
                    }
                    else
                    {
                        jumping = false;
                        landed = true;
                    }

                }
                else if (jumping)
                {
                    Debug.Log("JUMPING");
                    nsSet = false;
                   
                        if (!compareFloatWithin(player.transform.position.x, destinationSquare.x, (float).1))//!Mathf.Approximately(player.transform.position.x, nextSquare.x))

                        {
                            landed = false;
                         
                        }
                        else
                        {
                            if (compareFloatWithin(rb.velocity.y, 0, .01f))
                            {
                                nsSet = false;
                                landed = true;
                                jumping = false;
                            destinationFound = false;
                            
                        }

                        }
                  
                }
               
            }
           
            
           

        }
        if (!destinationFound)
        {
            if (destinationSquare.x > sourceSquare.x)
            {
                if (player.transform.position.x >= destinationSquare.x)
                {
                    // if (Mathf.Approximately(rb.velocity.x, 0))
                    //  {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                    snap("DESTINATION");
                    Debug.Log("SNAPPED");
                    //  }
                }
            }
            else if (destinationSquare.x < sourceSquare.x)
            {
                if (player.transform.position.x <= destinationSquare.x)
                {
                    // if (Mathf.Approximately(rb.velocity.x, 0))
                    // {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                    snap("DESTINATION");
                    Debug.Log("SNAPPED");
                    //  }
                }
            }

        }


        if (destinationSquare.x > sourceSquare.x)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
        }  else if (destinationSquare.x < sourceSquare.x)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
        }
    }

    void calculateJumpDistance() {
       
        jumpDistance.x = nextSquare.x - sourceSquare.x; 
        jumpDistance.y = nextSquare.y - sourceSquare.y;
    }
    float calculateWalkSpeed()
    { 
        
        float dist = destinationSquare.x - player.transform.position.x;
        if (dist > .5f)
        {
            return 5;
        }

        else if (dist < -.5f)
        {
            return -5;
        }
        else if (dist > 0) {
            return 1;
        }

        else if (dist < 0)
        {
            return -1;
        }
        else if (Mathf.Approximately(dist, 0))
        {
            return 0;
        }


        return 0;
        
    }
    void snap(string place) {
       
        switch (place)
        {
            case "SOURCE":
                player.transform.position = new Vector3(sourceSquare.x, player.transform.position.y, 0);
                break;
            case "DESTINATION":
                player.transform.position = new Vector3(destinationSquare.x, player.transform.position.y, 0);
                break;

        }
        
    }
    bool isContinuousWithJump() {
       if (findNextSquare("JUMPING"))
        {
            while (findNextSquare("JUMPING"))
            {
                Debug.Log("NS: " + nextSquare);
                calculateJumpDistance();
                if (jumpScan(1))
                {
                    //nsSet = false;
                    Debug.Log("Found NS");
                    return true;
                }
            }
        }
       
            Debug.Log("BackTracking");
       // if (findSourceSquare())
       // {
       if (findPreviousSquare("JUMPING"))
        {
            while (findPreviousSquare("JUMPING"))
            {
                calculateJumpDistance();
                Debug.Log("NS: " + nextSquare);
                if (jumpScan(1))
                {
                    //nsSet = false;
                    psSet = false;
                    //destinationSquare = nextSquare;
                    Debug.Log("Found PS");
                    return true;
                }
            }
        }
          
       
       // }
        //else {
        //    Debug.Log("???");
        //    return false;
        //}
      

        return false;
    }

    bool jumpScan(int mode) {
       
        jumpVelocity = new Vector2();
        bool isFound = false;
        float time = jumpTime;
 
        switch (mode)
        {
            case 0:
                break;
            case 1:
                if (destinationSquare.x > sourceSquare.x)
                {
                    if (jumpDistance.x > maxJumpDistance)
                    {
                        Debug.Log("TOO FAR");
                        return false;
                    }
                }
                else if (destinationSquare.x < sourceSquare.x)
                {
                    if (-jumpDistance.x  < -maxJumpDistance)
                    {
                        Debug.Log("TOO FAR");
                        return false;
                    }
                }

                break;
        }
        if (destinationSquare.y >= player.transform.position.y)
        {
            while (!isFound)
            {
                //while () { }
                switch (mode)
                {
                    case 0:
                        if (sourceSquare.y + (jumpVelocity.y * time) - (float)((9.8 * time * time) / 2) > sourceSquare.y + (jumpDistance.y * 2))

                        {
                            isFound = true;
                            Debug.Log("Jump Velocity Y: " + jumpVelocity.y);
                        }
                        else
                        {
                            jumpVelocity.y += (float).02;
                        }
                        break;
                    case 1:
                        if (((jumpVelocity.y  * time)) - (float)((9.8 * time * time) / 2)  > (jumpDistance.y))

                        {
                            //Debug.Log("IN.  NS VALUE:" + nextSquare);
                            isFound = true;
                            Debug.Log("Jump Velocity Y: " + jumpVelocity.y);
                        }
                        else
                        {
                            jumpVelocity.y += (float).02;
                        }
                        break;
                }


            }

            isFound = false;

            while (!isFound)
            {
                //time = 5;
                switch (mode)
                {
                    case 0:
                        if (sourceSquare.x + playerOffset.x + (jumpVelocity.x * time) > sourceSquare.x + jumpDistance.x / 2)
                        {
                            isFound = true;
                            Debug.Log("Jump Velocity X: " + jumpVelocity.x);
                        }
                        else
                        {
                            jumpVelocity.x += (float).02;
                        }
                        break;
                    case 1:
                        if (destinationSquare.x > sourceSquare.x)
                        {
                            if (((jumpVelocity.x * time))  > jumpDistance.x )
                            {
                                isFound = true;
                                Debug.Log("Jump Distance " + jumpDistance.x);
                                Debug.Log("Jump Velocity X: " + jumpVelocity.x);
                            }
                            else
                            {
                                jumpVelocity.x += (float).02;
                            }
                        }
                        else if (destinationSquare.x < sourceSquare.x)
                        {
                            if (((jumpVelocity.x * time))  < jumpDistance.x)
                            {
                                isFound = true;
                                Debug.Log("Jump Distance " + jumpDistance.x);
                                Debug.Log("Jump Velocity X: " + jumpVelocity.x);
                            }
                            else
                            {
                                jumpVelocity.x -= (float).02;
                            }
                        }

                        break;
                }


            }
        }
        else
        {

          

            

            while (!isFound)
            {
               // time = 5;
                switch (mode)
                {
                    case 0:
                        if (sourceSquare.x + playerOffset.x + (jumpVelocity.x * time) > sourceSquare.x + (jumpDistance.x * 2))
                        {
                            isFound = true;
                            Debug.Log("Jump Velocity X: " + jumpVelocity.x);
                        }
                        else
                        {
                            jumpVelocity.x += (float).02;
                        }
                        break;
                    case 1:
                        if (destinationSquare.x > sourceSquare.x)
                        {
                            if (((jumpVelocity.x * time)) > jumpDistance.x)
                            {
                                isFound = true;
                                Debug.Log("Jump Distance " + jumpDistance.x);
                                Debug.Log("Jump Velocity X: " + jumpVelocity.x);
                            }
                            else
                            {
                                jumpVelocity.x += (float).02;
                            }
                        }
                        else if (destinationSquare.x < sourceSquare.x)
                        {
                            if (((jumpVelocity.x * time))  < jumpDistance.x)
                            {
                                isFound = true;
                                Debug.Log("Jump Distance " + jumpDistance.x);
                                Debug.Log("Jump Velocity X: " + jumpVelocity.x);
                            }
                            else
                            {
                                jumpVelocity.x -= (float).02;
                            }
                        }

                        break;
                }


            }

            isFound = false;

            while (!isFound)
            {
                // time = 2;
                //while () { }
                switch (mode)
                {
                    case 0:
                        if (sourceSquare.y + (jumpVelocity.y * time) - (float)((9.8 * time * time) / 2) > sourceSquare.y + jumpDistance.y)

                        {
                            isFound = true;
                            Debug.Log("Jump Velocity Y: " + jumpVelocity.y);
                        }
                        else
                        {
                            jumpVelocity.y += (float).02;
                        }
                        break;
                    case 1:
                        if (((jumpVelocity.y * time)) - (float)((9.8 * time * time) / 2) > (jumpDistance.y))

                        {
                            //Debug.Log("IN.  NS VALUE:" + nextSquare);
                            isFound = true;
                            Debug.Log("Jump Velocity Y: " + jumpVelocity.y);
                        }
                        else
                        {
                            jumpVelocity.y += (float).02;
                        }
                        break;
                }


            }




        }
       
        
        Debug.Log("Scan succeeded");
        return true;
        //Debug.Log("Jump Out of range");
        //return false;
    }

    bool hasClearJumpPathNoCollision() {
        //bool hasFinished = false;
        float time = 1;
        Vector2 timeDistance = new Vector2();


        while (time < jumpTime * 100)
        {
            timeDistance.x = player.transform.position.x + ((jumpVelocity.x * (time / 100)));
            timeDistance.y = player.transform.position.y + ((jumpVelocity.y * (time / 100)))  - ((float)((9.8 * (time/100) * (time/100)) / 2));
            foreach (TileTag tile in tilemap.GetComponentsInChildren<TileTag>())
            {
                if ((timeDistance.x + xOffset > tile.go.transform.position.x - xOffset && timeDistance.x + xOffset < tile.go.transform.position.x + xOffset) ||
                   (timeDistance.x - xOffset > tile.go.transform.position.x - xOffset && timeDistance.x - xOffset < tile.go.transform.position.x + xOffset))
                {
                    if ((timeDistance.y + yOffsetTop > tile.go.transform.position.y - yOffsetTop && timeDistance.y + yOffsetTop < tile.go.transform.position.y + yOffsetTop) ||
                   (timeDistance.y - yOffsetBottom > tile.go.transform.position.y - yOffsetTop && timeDistance.y - yOffsetBottom < tile.go.transform.position.y + yOffsetTop))
                    {
                        Debug.Log("WILL COLLIDE DURING JUMP. Coordinates: " + timeDistance + "Time: " + time + " Tile Coordinates: " + (Vector2)tile.go.transform.position);
                        return false;

                    }
                }

            }
            time += 1;
        }
        return true;
    }
    bool findSourceSquare() {
        //HERE
        //xOffset = .4
        GameObject found = null;
        foreach (TileTag tile in tilemap.GetComponentsInChildren<TileTag>()) {
            if (player.transform.position.x > tile.go.transform.position.x - xOffset && player.transform.position.x < tile.go.transform.position.x + xOffset)
            {
                if (player.transform.position.y > tile.go.transform.position.y) {
                    if (found == null)
                    {
                        found = tile.go;
                    }
                    else {
                        if (tile.go.transform.position.y > found.transform.position.y) {
                            found = tile.go;
                        }
                    }
                }
            }
        }

        if (found != null) {
            sourceSquare = found.transform.position;
            playerOffset = new Vector2();
            playerOffset = (Vector2)player.transform.position - sourceSquare;
            return true;
        }
        Debug.Log("Source Invalid square");
        return false;
        
    }

    bool findDestinationSquare() {
        GameObject found = null;
        foreach (TileTag tile in tilemap.GetComponentsInChildren<TileTag>())
        {
            if (cc.cursorCoordinates.x > tile.go.transform.position.x - xOffset && cc.cursorCoordinates.x < tile.go.transform.position.x + xOffset)
            {
                //if (player.transform.position.y > cc.cursorCoordinates.y)
                // {
                if (found == null)
                {
                    if (tile.go.transform.position.y < cc.cursorCoordinates.y)
                    {
                        found = tile.go;
                    }
                }
                else
                {
                    
                    if (tile.go.transform.position.y < cc.cursorCoordinates.y)
                    {
                        if (tile.go.transform.position.y > found.transform.position.y)
                        {
                            // Debug.Log("Inside2");
                            found = tile.go;
                        }
                        // }
                    }
                }
                //}
            }
        }

        if (found != null)
        {

            destinationSquare = found.transform.position;
            return true;
        }
        Debug.Log("Destination Invalid square");
        return false;

    }

    
    bool isContinuous() {

        while (findNextSquare("WALKING")) {
            //if (findNextSquare()) {
                if (nextSquare == destinationSquare)
                {
                nsSet = false;
                    return true;
                }
                
            }
        
        return false;
    }

    bool findNextSquare(string mode) {
        float oldDist = 0f;
        GameObject found = null;
        bool continuous = true;
        if (!nsSet)
        {
            nextSquare = sourceSquare;
            nsSet = true;
            return true;
        }
        else
        {
            //if (mode == 1) {
            //    if (jumpScan(1))
            //    {
            //        Debug.Log("FOUND JUMPING SQUARE");
            //        return false;
            //    }
            //}

            foreach (TileTag tile in tilemap.GetComponentsInChildren<TileTag>())
            {

                if (destinationSquare.x >= nextSquare.x)
                {
                    //Debug.Log("IN");
                    switch (mode)
                    {
                        case "WALKING":
                            if (Mathf.Approximately(tile.go.transform.position.y, nextSquare.y))
                            {
                                
                                if (Mathf.Approximately(tile.go.transform.position.x, (float)(nextSquare.x + xSquareSize)))
                                { 
                                    found = tile.go;
                                   
                                }
                            }
                            break;
                        case "JUMPING":
                            if (Mathf.Approximately(tile.go.transform.position.y, destinationSquare.y))
                            {
                                if (Mathf.Approximately(tile.go.transform.position.x, destinationSquare.x))
                                { 
                                    
                                    
                                    found = tile.go;
                                   
                                }
                            }

                                break;

                    }
                
            }
                else if (destinationSquare.x <= nextSquare.x)
                {
                    switch (mode)
                    {
                        case "WALKING":
                            if (Mathf.Approximately(tile.go.transform.position.y, nextSquare.y))
                            {
                                if (Mathf.Approximately(tile.go.transform.position.x, (float)(nextSquare.x - xSquareSize)))
                                { 
                                    found = tile.go;
                                    // }
                                }
                            }
                            break;
                        case "JUMPING":
                            if (Mathf.Approximately(tile.go.transform.position.y, destinationSquare.y))
                            {
                                //    Debug.Log("IN Y");
                                //calculateJumpDistance();


                                //if (Mathf.Approximately(tile.go.transform.position.x, (float)(nextSquare.x)))
                                if (Mathf.Approximately(tile.go.transform.position.x, destinationSquare.x))
                                {

                                 
                                    found = tile.go;
                                    // }
                                }
                            }
                            
                            //}
                            break;
                    }
                    
                   
                }


            }

        }

       

        if (found != null) {
         
            nextSquare = found.transform.position;
            Debug.Log(nextSquare);
            return true;
        }
        Debug.Log("Not continuous");
        return false;
    }

    bool findPreviousSquare(string mode) {
        GameObject found = null;
      
            if (Mathf.Approximately(nextSquare.x,sourceSquare.x))
            {
                if (!psSet)
                {
                    nextSquare = destinationSquare;
                psSet = true;
                }
                else
                {
                psSet = false;
                Debug.Log("Retraced to Player: Failed");
                return false;
            }
            }
       
        
        Debug.Log("Starting NS:" + nextSquare);
        foreach (TileTag tile in tilemap.GetComponentsInChildren<TileTag>())
        {
         
             if (nextSquare.x > player.transform.position.x)
            {

                switch (mode)
                {
                    case "WALKING":
                        if (Mathf.Approximately(tile.go.transform.position.y, nextSquare.y))
                        {
                            if (Mathf.Approximately(tile.go.transform.position.x, (float)(nextSquare.x - xSquareSize)))
                            { //.79 && tile.go.transform.position.x < nextSquare.x + .81) {

                                Debug.Log("INNER");
                                
                                found = tile.go;
                                Debug.Log(nextSquare);
                               goto Later;
                                // }
                            }
                        }
                        break;
                    case "JUMPING":
                        if (Mathf.Approximately(destinationSquare.y, tile.go.transform.position.y))
                        {
                            if (Mathf.Approximately(tile.go.transform.position.x, (float)(nextSquare.x - xSquareSize)))
                            { //.79 && tile.go.transform.position.x < nextSquare.x + .81) {

                                Debug.Log("INNER");
                               
                                found = tile.go;
                                Debug.Log(nextSquare);
                                
                                goto Later;
                                // }
                            }
                        }
                        break;
                }

           
            }

            else if (nextSquare.x < player.transform.position.x)
            {
                switch (mode)
                {
                    case "WALKING":
                        if (Mathf.Approximately(tile.go.transform.position.y, nextSquare.y))
                        {
                            //    Debug.Log("IN Y");
                            if (Mathf.Approximately(tile.go.transform.position.x, (float)(nextSquare.x + xSquareSize)))
                            { //.79 && tile.go.transform.position.x < nextSquare.x + .81) {
                                Debug.Log("INNER");
                                
                                found = tile.go;
                                Debug.Log(nextSquare);
                                // break;
                                // }
                                goto Later;
                            }
                        }
                        break;
                    case "JUMPING":
                        if (Mathf.Approximately(destinationSquare.y, nextSquare.y))
                        {
                            if (Mathf.Approximately(tile.go.transform.position.x, (float)(nextSquare.x + xSquareSize)))
                            { //.79 && tile.go.transform.position.x < nextSquare.x + .81) {
                                Debug.Log("INNER");
                                // if (tile.go.transform.position.y > player.GetComponent<BoxCollider2D>().bounds.min.y && tile.go.transform.position.y < player.GetComponent<BoxCollider2D>().bounds.min.y)
                                //{
                                //    continuous = false;
                                //}
                                // Debug.Log("Here");
                                //if ()
                                //{
                                //  Debug.Log("There");
                                found = tile.go;
                                Debug.Log(nextSquare);
                                // break;
                                // }
                                goto Later;
                            }
                        }
                        break;

                }
                
            }
        }
        Later:

        if (found != null)
        {
            
            nextSquare = found.transform.position;
            Debug.Log(nextSquare);
            return true;
        }
        if (found == null) {
                Debug.Log("Tile not found: Retrace Failed!");
                return false;
            }
            
        
        Debug.Log("?");
        return false;
    } 

    bool compareFloatWithin(float main, float compare, float within) {
        if (main + within <= compare || main - within >= compare) {
            return true;
        }
        return false;
    }

   
}
