using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class Laser : MonoBehaviour {

	public float distance;
	public Transform beam;
	public float offset = 0;
	public LayerMask collisionMask;
	public bool onTimer = false;
	public float waitSeconds;

	//private references
	bool _Activated;
	Renderer beamRender;
	BoxCollider2D beamCollider;
	float beamWidth;
	float targetWidth;

	private void Awake() {
		beamRender = beam.GetComponent<Renderer>();
		beamCollider = beam.GetComponent<BoxCollider2D>();
	}

	// Use this for initialization
	void Start () {
		this.distance = 0;
		this.beamWidth = 0.125f;
		this.targetWidth = 0.125f;
		_Activated = true;

		if(onTimer)
		{
			StartCoroutine("StartTimer");
		}
		//StartCoroutine
	}
	
	// Update is called once per frame
	void Update () {
		CalcDistance();
		AdjustBeam(beamWidth);
		offset = (offset + Time.deltaTime) % 4;
		beamRender.material.mainTextureOffset = new Vector2(0,offset);
		beamRender.material.mainTextureScale = new Vector2 (1, distance);
		beamWidth = Mathf.Lerp(beamWidth,targetWidth, Time.deltaTime * 8);
		if(_Activated)
		{
			beamCollider.enabled = true;
		}
		else
		{
			beamCollider.enabled = false;
		}
	}

	void CalcDistance(){
		RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up,100,collisionMask);
		Debug.DrawRay(transform.position,-transform.up * 100,Color.green);
		if(hit.collider != null) {
			this.distance = hit.distance;
		}
	}

	void AdjustBeam(float Width)
	{
		beam.localScale = new Vector3(Width,distance,1);
		beam.transform.localPosition = new Vector3(0,-0.5f * distance + 0.5f,0);
	}

	IEnumerator StartTimer(){
		while (true)
		{
			_Activated = true;
			targetWidth = 0.5f;
			yield return new WaitForSeconds(waitSeconds);

			targetWidth = 0;
			_Activated = false;
			yield return new WaitForSeconds(1.0f);

			targetWidth = 0.1f;
			yield return new WaitForSeconds(1.0f);


		}
	}
}
