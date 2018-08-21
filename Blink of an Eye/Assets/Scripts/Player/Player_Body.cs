using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Body : MonoBehaviour {

	//Manipulatable values
	public float maxJumpHeight = 4;
	public float minJumpHeight = 1;
	public float timeToApex = .4f;
	public float moveSpeed = 6;
	public float accelerationTimeAirborne = .2f;
	public float accelerationTimeGrounded = .1f;

	//set-ables
	public SpriteRenderer eyes;
	public Sprite eyesClosed;


	//private dependacies
	float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;
	float bounceHeight;
	Vector3 velocity;
	private TrailRenderer trail;
	float velocityXSmoothing;

	//private checks
	bool canDoubleJump;
	bool isDoubleJumping;
	bool isJumping;
	public	bool controlled;
	bool eyeFlip;
	bool bouncing;
	public Vector2 directionalInput;
	
	//IOS DEPENDENT VARIABLES
	#if UNITY_IOS
	public bool jumping = false;
	#endif


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
		bouncing = false;
	}
	
	public void SetDirectionalInput(Vector2 input) {
		directionalInput = input;
	}

	public void OnJumpInputDown() {
		Debug.Log("Should be jumping");
		if(controller.collisions.below)
		{
			Debug.Log("jumping now");
			velocity.y = maxJumpVelocity;
			isDoubleJumping = false;
			isJumping = true;
		}
		if(canDoubleJump && !controller.collisions.below && !isDoubleJumping)
		{
			velocity.y = maxJumpVelocity * 0.75f;
			isDoubleJumping = true;
		}
	}

	public void OnJumpInputUp() {
		if(velocity.y > minJumpVelocity)
		{
			velocity.y = minJumpVelocity;
		}
	}

	// Update is called once per frame
	void Update () {
		if(controlled)
		{
			if((controller.collisions.below || controller.collisions.above) && !isJumping)
			{
				velocity.y = 0;
				canDoubleJump = true;
			}
			

			if(bouncing)
			{
				velocity.y = bounceHeight;
				bouncing = false;
				bounceHeight = 0;
			}

			//Horizontal Movement
			float targetVelocityX = directionalInput.x * moveSpeed;
			velocity.x = Mathf.SmoothDamp(velocity.x,targetVelocityX,ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
			if(velocity.y < 0)
			{
				velocity.y += gravity * Time.deltaTime;
				}
			velocity.y += gravity * Time.deltaTime;

			//eye movement
			eyeFlip = (velocity.x > -0.1)? false : true;
			eyes.flipX = eyeFlip;
			
		}
	}

	private void FixedUpdate() {
		controller.Move(velocity * Time.deltaTime);
		if(isJumping)
			{
				isJumping = false;
			}
		
	}

	 public void Kill()
    {
        this.controlled = false;
		//this.gravity = 0;
        //this.GetComponent<Collider2D>().enabled = false;
        Color color = this.GetComponent<SpriteRenderer>().color;
        color.a -= 0.75f;
        this.GetComponent<SpriteRenderer>().color = color;
		this.gameObject.layer = 13;
		eyes.sprite = eyesClosed;
		StartCoroutine ("SelfDestruct");
    }

	public void Bounce(float height)
	{
		this.bouncing = true;
		this.bounceHeight = height;
	}

	public bool GetControlled(){
		return this.controlled;
	}
	IEnumerator SelfDestruct()
	{
		Debug.Log ("Destory");
		while(Mathf.Abs(velocity.x) > 0.1f)
		{
			if(velocity.y >= 0)
			{
				velocity.y += gravity * Time.deltaTime;
			}
			else
			{
				velocity.y += 2 * gravity * Time.deltaTime;
			}
			velocity.x = Mathf.MoveTowards(velocity.x,0,1 * Time.deltaTime);
			yield return null;
		}
		yield return new WaitForSeconds (10.0f);
		Destroy (this.gameObject);
	}
	
}
