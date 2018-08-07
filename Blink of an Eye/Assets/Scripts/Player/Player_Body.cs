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

	//Touch input
	TouchButton b_Jump;
	TouchButton b_Left;
	TouchButton b_Right;

	//private dependacies
	float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;
	float bounceHeight;
	Vector3 velocity;
	private TrailRenderer trail;
	float velocityXSmoothing;

	//private checks
	bool _canDoubleJump;
	bool jumpHeldDown = false;
	public	bool controlled;
	bool eyeFlip;
	bool bouncing;
	

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
			
			#if UNITY_STANDALONE_WIN
			Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
			#endif

			#if UNITY_IOS
			int left = b_Left.Pressed? -1 : 0;
			int right = b_Right.Pressed? 1: 0;
			Vector2 input = new Vector2(left + right, 0);
			#endif

			if(bouncing)
			{
				velocity.y = bounceHeight;
				bouncing = false;
				bounceHeight = 0;
			}

			//***********************IOS JUMPING************************* NEEDS TO BE FIXED FOR HELD JUMPS
			#if UNITY_IOS
			//Handles ios jumping
			if(b_Jump.Pressed) //if button is pressed
			{
				if(!jumping) // if character is not already jumping
				{
					if(controller.collisions.below) // if character is grounded
					{
						print("long boi");
						velocity.y = maxJumpVelocity;
						jumping = true;	
					}
					else
					{
						print("2nd jump");
						velocity.y = 1.25f * minJumpVelocity;
						_canDoubleJump = false;	
					}
				}
			}

			//Short ios jump
			if(!b_Jump.Pressed && velocity.y > 0.1f)
			{
				print("smol jump");
				if(velocity.y> minJumpVelocity){
					velocity.y = minJumpVelocity;
				}
				jumping = false;
			}

			//***********************WINDOWS JUMPING*************************
			#elif UNITY_STANDALONE_WIN
			//Handles jumping
			if(Input.GetButtonDown("Vertical") && _canDoubleJump)
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

			//Short jump
			if(Input.GetButtonUp("Vertical"))
			{
				if(velocity.y> minJumpVelocity){
					velocity.y = minJumpVelocity;
				}
			}
			#endif

			//Horizontal Movement
			float targetVelocityX = input.x * moveSpeed;
			velocity.x = Mathf.SmoothDamp(velocity.x,targetVelocityX,ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
			if(velocity.y < 0)
			{
				velocity.y += gravity * Time.deltaTime;
				}
			velocity.y += gravity * Time.deltaTime;
			//controller.Move( velocity  * Time.deltaTime);

			//eye movement
			eyeFlip = (velocity.x > -0.1)? false : true;
			eyes.flipX = eyeFlip;
		}
	}

	private void FixedUpdate() {
		controller.Move(velocity * Time.deltaTime);
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
	public void SetButtons(TouchButton j, TouchButton l, TouchButton r) {
		{
			this.b_Jump = j;
			this.b_Left = l;
			this.b_Right = r;
		}
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
