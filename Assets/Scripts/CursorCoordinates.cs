using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorCoordinates : MonoBehaviour
{
    public Vector2 cursorCoordinates;
    // Start is called before the first frame update
    void Start()
    {
       
        cursorCoordinates = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
       // putNewCoordinates();
       // if (Input.GetMouseButtonDown(0))
      //  {
           
     //  }
    }

    public void putNewCoordinates()
    {
        cursorCoordinates = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

    }
}
