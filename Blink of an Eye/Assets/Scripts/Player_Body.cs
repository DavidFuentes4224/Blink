using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Body : MonoBehaviour {

	public float maxJumpHeight = 4;
	public float minJumpHeight = 1;
	public float timeToApex = .4f;
	public float moveSpeed = 6;
	public float accelerationTimeAirborne = .2f;
	public float accelerationTimeGrounded = .1f;
	
	float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;
	bool _canDoubleJump;
	bool controlled;

	Vector3 velocity;
	private TrailRenderer trail;
	float velocityXSmoothing;

	Player_Controller controller;

	// Use this for initialization
	void Start () {
		controller = GetComponent<Player_Controller>();
		gravity = -(2 * maxJumpHeight)/(timeToApex * timeToApex);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToApex;
		minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
		//print("Gravity: " + gravity + " Max Jump vel: " + maxJumpVelocity + " Double Jump Vel: " + (1.25f * minJumpVelocity));
		this.controlled = true;

		//visual stuff
		trail = this.GetComponent<TrailRenderer>();
		Color color = new Color(Random.value, Random.value, Random.value, 1.0f);
        trail.startColor = color;
        trail.endColor = color;
		velocity = new Vector3(0,0,0);
	}
	
	// Update is called once per frame
	void Update () {
		if(controlled)
		{
			if(controller.collisions.below || controller.collisions.above)
			{
				velocity.y = 0;
				if(controller.collisions.below)
				{
					_canDoubleJump = true;
				}
			}

			Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

			if(Input.GetKeyDown(KeyCode.Space) && _canDoubleJump)
			{
				if(controller.collisions.below)
				{
					velocity.y = maxJumpVelocity;	
				}
				else
				{
					velocity.y = 1.25f * minJumpVelocity;
					_canDoubleJump = false;	
				}
			}

			if(Input.GetKeyUp(KeyCode.Space))
			{
				if(velocity.y> minJumpVelocity){
					velocity.y = minJumpVelocity;
				}
			}

			float targetVelocityX = input.x * moveSpeed;
			velocity.x = Mathf.SmoothDamp(velocity.x,targetVelocityX,ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
			if(velocity.y < 0)
			{velocity.y += gravity * Time.deltaTime;}
			velocity.y += gravity * Time.deltaTime;
			//controller.Move( velocity  * Time.deltaTime);
		}
	}
	private void FixedUpdate() {
		controller.Move(velocity * Time.deltaTime);
	}

	 public void Kill()
    {
        velocity = new Vector2(0, 0);
        this.controlled = false;
		this.gravity = 0;
        this.GetComponent<Collider2D>().enabled = false;
        Color color = this.GetComponent<SpriteRenderer>().color;
        color.a -= 0.75f;
        this.GetComponent<SpriteRenderer>().color = color;
		StartCoroutine ("SelfDestruct");
    }

	IEnumerator SelfDestruct()
	{
		Debug.Log ("Destory");
		yield return new WaitForSeconds (10.0f);
		Destroy (this.gameObject);
	}
	
}
