using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	//manips
	public int number;
	//privs
	bool unlocked;
	public Player player;

	// Use this for initialization
	private void Awake() {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	void Start () {
		unlocked = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	bool GetUnlockStatus(){
		return this.unlocked;
	}

	public void Unlock(){
		unlocked = true;
		this.gameObject.layer = 10;
		Color doorColor = this.gameObject.GetComponent<SpriteRenderer>().color;
		doorColor.a = 1.0f;
		this.gameObject.GetComponent<SpriteRenderer>().color = doorColor;
	}

	public void Lock(){
		unlocked = false;
		this.gameObject.layer = 0;
		Color doorColor = this.gameObject.GetComponent<SpriteRenderer>().color;
		doorColor.a = 0.25f;
		this.gameObject.GetComponent<SpriteRenderer>().color = doorColor;
	}
}
