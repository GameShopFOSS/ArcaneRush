using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
	public int hp;
	public int maxhp;
	public int hpregen;
	public int mp;
	public int maxmp;
	public int mpregen;
	public int movementSpeed;
	public int armor;
	public bool airborne;
	public bool stunned;
	public float timerhpregen;
	public float timermpregen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        capStatistics();
    }

    void capStatistics()
    {
    	if (hp > maxhp) 
    	{
    		hp = maxhp;
    	}

    	if (mp > maxmp)
    	{
    		mp = maxmp;
    	}
    }
}
