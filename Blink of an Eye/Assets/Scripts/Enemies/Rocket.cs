using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
	
	public float speed;
	public float turnSpeed;

	private Transform target;
	
	// Use this for initialization
	private void Start() {
		target = FindObjectOfType<Player>().GetBodyRef();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 dir = target.position - transform.position;
		var angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg - 180;

		transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(0,0,angle),turnSpeed * Time.deltaTime);
		//transform.position = Vector3.MoveTowards(transform.position,target.position,Time.deltaTime * speed);
		Vector3 forward = transform.right * -1;
		transform.position = transform.position + (forward * speed);

		
	}
}
