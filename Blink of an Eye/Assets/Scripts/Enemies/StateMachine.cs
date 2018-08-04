using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {
	public Transform[] locations;
	public float waitTime;
	private Transform target;
	bool changingState;
	public bool smooth = false;
	public float speed = 2;
	int locationIndex;
	int Dir = 1;

	Vector3 previous;
	Vector3 velocity = new Vector3(0,0,0);

	// Use this for initialization
	void Start () {
		locationIndex = 0;
		StartCoroutine("ChangeLocation");
	}
	
	// Update is called once per frame
	void Update () {
		velocity = (transform.position - previous) /Time.deltaTime;
		previous = transform.position;
	}

	public int GetDirection()
	{
		return this.Dir;
	}

	public Vector3 GetVelocity()
	{
		return this.velocity;
	}

	IEnumerator ChangeLocation()
	{
		while(true) //logic loop
		{
			while(Vector2.Distance(transform.position,locations[locationIndex].position) > 0.05f)
			{
				if(smooth)
				{
					this.transform.position = Vector2.Lerp(transform.position,locations[locationIndex].position, (speed * 0.5f) * Time.deltaTime);
				}
				else{
					this.transform.position = Vector2.MoveTowards(transform.position,locations[locationIndex].position, speed * Time.deltaTime);
				}
				yield return null;
			}
			yield return new WaitForSeconds(waitTime);
			locationIndex = (locationIndex + 1) % locations.Length;
			Dir = (int) Mathf.Sign(this.transform.position.x - locations[locationIndex].position.x);
			if(this.GetComponent<Enemy>())
			{
				//Debug.Log("Direction " +(int) Mathf.Sign(this.transform.position.x - locations[locationIndex].position.x));
				this.GetComponent<Enemy>().ToggleEyes(Dir);

			}
		}

	}
}
