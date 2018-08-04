using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public float health = 100;
	public SpriteRenderer eyes;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(health < 0)
		{
			Destroy(this);
		}
	}

	public void ToggleEyes(float direction)
	{
		//direction = (int) direction;
		//print(direction + " is the current direciton value");
		if(direction == 1)
		{
			eyes.flipX = true;
		}
		else {eyes.flipX = false;}
	}
}
