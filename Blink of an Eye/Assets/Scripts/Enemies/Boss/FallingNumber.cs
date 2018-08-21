using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingNumber : MonoBehaviour {
	public TextMesh displayNumber;
	public bool isSolution = false;


	public void SetDisplay(int num)
	{
		displayNumber.text = num.ToString();
	}

	public void makeSolution()
	{
		isSolution = true;
		gameObject.layer = 11;
	}

	// Use this for initialization
	void Awake () {
		displayNumber = this.GetComponent<TextMesh>();
	}

	private void Start() {
		StartCoroutine("SelfDestruct");
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x,transform.position.y - Time.deltaTime * 3,0);
	}

	IEnumerator SelfDestruct()
	{
		yield return new WaitForSeconds(6.0f);
		Destroy(this.gameObject);
	}
}
