using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
	
	public float speed;
	public float turnSpeed;
	public LayerMask collisionMask;

	private Transform target;
	Launcher launcher;
	
	// Use this for initialization
	private void Start() {
		
	}
	
	// Update is called once per frame
	void Update () {
		target = FindObjectOfType<Player>().GetBodyRef();
		Vector3 dir = target.position - transform.position;
		var angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg - 180;

		transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(0,0,angle),turnSpeed * Time.deltaTime);
		//transform.position = Vector3.MoveTowards(transform.position,target.position,Time.deltaTime * speed);
		Vector3 forward = transform.right * -1;
		transform.position = transform.position + (forward * speed);
		RaycastHit2D hit = Physics2D.Raycast(transform.position,transform.right * -1,0.75f,collisionMask);
		Debug.DrawRay(transform.position, transform.right * -0.75f, Color.black);
		if(hit)
		{
			Explode();
		}
	}

	public void addLauncher(Launcher l)
	{
		this.launcher = l;
		return;
	}

	void Explode()
	{

		if(launcher)
		{
			launcher.DecreaseObjectCount();
		}
		Destroy(this.gameObject);
		return;
	}
}
