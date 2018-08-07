using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Key {

	
	public bool isPressed;
	public Transform buttonSprite;
	public LayerMask collisionMask;
	float verticalRaySpacing;
	public Door door;
	Collider2D  colliderBody;

	private void Start() {
		colliderBody= GetComponent<BoxCollider2D>();
		CalculateRaySpacing();
	}

	private void Update() {
		if(isPressed)
		{
			buttonSprite.transform.position = this.transform.position;
			door.Unlock();
		}
		else
		{
			buttonSprite.transform.position = this.transform.position + new Vector3(0,0.125f,0);
			door.Lock();
		}
		Vector2 rayOrigin = new Vector2(colliderBody.bounds.min.x,transform.position.y);
		for(int i = 0; i < 4; i++)
		{
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin,Vector2.up, 0.5f, collisionMask);
			rayOrigin.x += verticalRaySpacing;
			Debug.DrawRay(rayOrigin, Vector2.up * 0.5f, Color.red);
			if(hit)
			{
				int hitLayer = hit.collider.gameObject.layer;
				if(hitLayer ==  0 || hitLayer == 13)
				{
					isPressed = true;
					return;
				}
			}
			else
			{
				isPressed = false;
			}
		}
	}

	void CalculateRaySpacing() { 
		Bounds bounds = colliderBody.bounds;
		verticalRaySpacing = bounds.size.x / (4);
	}
}
