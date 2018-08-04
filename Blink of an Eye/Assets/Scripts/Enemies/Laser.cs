using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class Laser : MonoBehaviour {

	public float distance;
	public Transform beam;
	public float offset = 0;
	public LayerMask collisionMask;

	// Use this for initialization
	void Start () {
		this.distance = 0;
		//StartCoroutine
	}
	
	// Update is called once per frame
	void Update () {
		CalcDistance();
		AdjustBeam();
		offset = (offset + Time.deltaTime) % 4;
		beam.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0,offset);
		beam.GetComponent<Renderer>().material.mainTextureScale = new Vector2 (1, distance);

	}

	void CalcDistance(){
		RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up,100,collisionMask);
		Debug.DrawRay(transform.position,-transform.up * 100,Color.green);
		if(hit.collider != null) {
			this.distance = hit.distance;
		}
	}

	void AdjustBeam()
	{
		beam.localScale = new Vector3(0.15f,distance,1);
		beam.transform.localPosition = new Vector3(0,-0.5f * distance + 0.5f,0);
		
	}
}
