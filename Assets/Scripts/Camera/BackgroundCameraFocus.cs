using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCameraFocus : MonoBehaviour
{

	public GameObject player;
	Vector2 distance;
    // Start is called before the first frame update
    void Start()
    {
        distance = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
        foreach(Transform child in transform)
		{
   			child.gameObject.AddComponent<BackgroundCameraFocus>();
   			child.gameObject.GetComponent<BackgroundCameraFocus>().player = gameObject;
		}
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x - distance.x, player.transform.position.y - distance.y, transform.position.z);
    }
}
