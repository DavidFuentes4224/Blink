using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour {

	public LayerMask collisionMask;

	const float skinWidth = 0.015f;
	public int horizontalRayCount = 4;
	public int verticalRayCount = 4;

	float horizontalRaySpacing;
	float verticalRaySpacing;

	BoxCollider2D colliderBody;
	RaycastOrigins raycasyOrigins;
	public CollisionInfo collisions;

	private Player player;

	// Use this for initialization
	void Start () {
		colliderBody= GetComponent<BoxCollider2D>();
		CalculateRaySpacing();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	public void Move(Vector3 velocity){
		if(colliderBody != null)
		{
			UpdateRaycastOrigins();
			collisions.Reset();

			if(velocity.x != 0) {
				HorizontalCollisions (ref velocity);
			}
			if(velocity.y != 0) {
				VerticalCollisions(ref velocity);
			}
		}


		transform.Translate(velocity);
	}
	
	//*********************************HORIZONTAL***************************/
	void HorizontalCollisions(ref Vector3 velocity) {
		float directionX = Mathf.Sign(velocity.x);
		float rayLength = Mathf.Abs(velocity.x) + skinWidth;

		for (int i = 0; i < verticalRayCount; i++){
			Vector2 rayOrigin = (directionX == -1)?raycasyOrigins.bottomLeft:raycasyOrigins.bottomRight;
			rayOrigin += Vector2.up * (horizontalRaySpacing * i);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin,Vector2.right * directionX, rayLength, collisionMask);

			Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

			if(hit){
				velocity.x = (hit.distance - skinWidth) * directionX;
				rayLength = hit.distance;

				collisions.left = directionX == -1;
				collisions.right = directionX == 1;

				//handles collision types
				int hitLayer = hit.collider.gameObject.layer;
				switch(hitLayer)
				{
					case 9: //harmful
						velocity.y = 0;
						player.SpawnNewBody();
						return;
					case 10: //victory
						velocity.x = 0;
						player.Win();
						break;
					default: break;
				}
			}
		}
	}

//*********************************VERTICAL***************************/
	void VerticalCollisions(ref Vector3 velocity) {
		float directionY = Mathf.Sign(velocity.y);
		float rayLength = Mathf.Abs(velocity.y) + skinWidth;

		for (int i = 0; i < verticalRayCount; i++){
			Vector2 rayOrigin = (directionY == -1)?raycasyOrigins.bottomLeft:raycasyOrigins.topLeft;
			rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin,Vector2.up * directionY, rayLength, collisionMask);

			Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);
		
			if(hit){
				velocity.y = (hit.distance - skinWidth) * directionY;
				rayLength = hit.distance;

				collisions.below = directionY == -1;
				collisions.above = directionY == 1;

				//handles collision types
				int hitLayer = hit.collider.gameObject.layer;
				switch(hitLayer)
				{
					case 9: //harmful
						velocity.y = 0;
						player.SpawnNewBody();
						return;
					case 10: //victory
						velocity.y = 0;
						player.Win();
						break;
					default: break;
				}
			}
		}
	}


	void UpdateRaycastOrigins() {		
		Bounds bounds = colliderBody.bounds;
		bounds.Expand (skinWidth * -2);

		raycasyOrigins.bottomLeft = new Vector2 (bounds.min.x, bounds.min.y);
		raycasyOrigins.bottomRight = new Vector2 (bounds.max.x, bounds.min.y);
		raycasyOrigins.topLeft = new Vector2 (bounds.min.x, bounds.max.y);
		raycasyOrigins.topRight = new Vector2 (bounds.max.x, bounds.max.y);
	}

	void CalculateRaySpacing() { 
		Bounds bounds = colliderBody.bounds;
		bounds.Expand (skinWidth * -2);

		horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
		verticalRayCount = Mathf.Clamp(verticalRayCount,2,int.MaxValue);

		horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
		verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
	}

	struct RaycastOrigins{
		public Vector2 topLeft, topRight;
		public Vector2 bottomLeft, bottomRight;
	}

	public struct CollisionInfo {
		public bool above, below;
		public bool left, right;

		public void Reset(){
			above = below = false;
			left = right = false;
		}
	}
}
