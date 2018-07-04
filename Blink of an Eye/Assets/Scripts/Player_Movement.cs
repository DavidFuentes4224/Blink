﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerPhysics))]
public class Player_Movement : MonoBehaviour {

    public float speed;
    public float acceleration;
    public float jumpHeight;
    public LayerMask groundLayer;
    public float offset;

    private float currentSpeed;
    private float targetSpeed;
    private Vector2 amountToMove;
    private bool jumped;
    private bool controlled;

    private Animator Eyes;
    private PlayerPhysics playerPhysics;
    private TrailRenderer trail;
    private Rigidbody2D body;

	// Use this for initialization
	void Start () {
        playerPhysics = this.GetComponent<PlayerPhysics>();
        trail = this.GetComponent<TrailRenderer>();
        body = this.GetComponent<Rigidbody2D>();

        //Eyes = GameObject.FindGameObjectWithTag("Eye").GetComponent<Animator>();
        //Eyes.SetBool("_isOpen", true);
        Color color = new Color(Random.value, Random.value, Random.value, 1.0f);
        trail.startColor = color;
        trail.endColor = color;
        controlled = true;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (controlled)
        {
            if (Input.GetButtonDown("Vertical") && IsGrounded())
            {
                this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 100 * jumpHeight));

            }
        }
        
    }

	void FixedUpdate() {
        if (controlled)
        {
            targetSpeed = Input.GetAxisRaw("Horizontal") * speed;
            currentSpeed = IncrementTowards(currentSpeed, targetSpeed, acceleration);

            amountToMove = new Vector2(currentSpeed, 0);



            playerPhysics.Move(amountToMove * Time.deltaTime);
        }
    }

    private float IncrementTowards(float n, float target, float accl)
    {
        if(n == target)
        {
            return n;
        }
        else
        {
            float dir = Mathf.Sign(target - n);
            n += accl * Time.deltaTime * dir;
            return (dir == Mathf.Sign(target - n))? n : target;
        }
    }

    public bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 0.6f;

        Debug.DrawRay(position, direction * distance, Color.green, 5.0f);
        Debug.DrawRay(position + Vector2.left * offset, direction * distance, Color.green, 5.0f);
        Debug.DrawRay(position + Vector2.right * offset, direction * distance, Color.green, 5.0f);

        Debug.Log("Drawing ray");
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        RaycastHit2D hitL = Physics2D.Raycast(position + Vector2.left * offset, direction, distance, groundLayer);
        RaycastHit2D hitR = Physics2D.Raycast(position + Vector2.right * offset, direction, distance, groundLayer);
        if (hit.collider != null || hitL.collider != null || hitR.collider != null)
        {
            return true;
        }

        return false;
    }

    public void Kill()
    {
        body.velocity = new Vector2(0, 0);
        body.constraints = RigidbodyConstraints2D.FreezeAll;
        this.controlled = false;
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
