using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSlider : MonoBehaviour {
	public Vector2 direction;
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector2(transform.position.x + (direction.x * Time.deltaTime),transform.position.y + (direction.y * Time.deltaTime));
	}

	IEnumerator SelfDestruct()
	{
		yield return new WaitForSeconds(15.0f);
		Destroy(this.gameObject);
	}
}
